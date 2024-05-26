using CsvParser.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CsvParser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvParseController : ControllerBase
    {
        private readonly ICsvTransactionParsingService _csvTransactionParsingService;
        public CsvParseController(ICsvTransactionParsingService csvTransactionParsingService)
        {
            _csvTransactionParsingService = csvTransactionParsingService;
        }

        /// <summary>
        /// Uploads csv with a collection of transactions 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("UploadCsv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                var success = await _csvTransactionParsingService.UploadCsvAsync(file);

                if (success)
                {
                    return Ok("File uploaded and processed successfully.");
                }
                else
                {
                    return StatusCode(500, "Error processing the file.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets total valid transactions
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetTotalTransactions")]
        public async Task<IActionResult> GetTransactions(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and page size must be positive integers.");
            }

            var paginatedResult = await _csvTransactionParsingService.GetPaginatedTransactionsAsync(page, pageSize);

            return Ok(paginatedResult);
        }

        /// <summary>
        /// Deletes a single transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTransaction/{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var success = await _csvTransactionParsingService.DeleteTransactionAsync(id);

            if (!success)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }

            return Ok($"Transaction with ID {id} deleted successfully.");
        }
        /// <summary>
        /// Gets single transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetTransaction/{id}")]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var transaction = await _csvTransactionParsingService.GetTransactionAsync(id);

            if (transaction == null)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }

            return Ok(transaction);
        }
    }
}
