﻿namespace CsvParser.Common.Responses
{
    public class GenericResponse
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public GenericResponse() { }

        public GenericResponse(bool isValid, List<string> errors)
        {
            IsValid = isValid;
            Errors = errors ?? new List<string>();
        }
    }
}
