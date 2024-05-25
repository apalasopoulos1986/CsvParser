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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                var transactions = await _csvTransactionParsingService.ParseCsvAsync(file);
                               
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
