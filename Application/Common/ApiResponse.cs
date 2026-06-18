using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class ApiResponse<TData>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public TData? Data { get; set; }
        public List<string>? Errors { get; set; }

        private ApiResponse(TData data, string message, int statusCode)
        {
            IsSuccess = true;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            Errors = null; 
        }

        private ApiResponse(string message, int statusCode, List<string>? errors)
        {
            IsSuccess = false;
            Message = message;
            StatusCode = statusCode;
            Errors = errors ?? new List<string>(); 
            Data = default; 
        }

        public static ApiResponse<TData> Success(TData data, string message = "Operation completed successfully", int statusCode = 200)
        {
            return new ApiResponse<TData>(data, message, statusCode);
        }

        public static ApiResponse<TData> Failure(string message, int statusCode = 400, List<string>? errors = null)
        {
            return new ApiResponse<TData>(message, statusCode, errors);
        }
    }
}