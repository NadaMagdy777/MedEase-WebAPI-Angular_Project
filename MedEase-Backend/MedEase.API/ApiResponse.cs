using Microsoft.AspNetCore.Http;

namespace MedEase.API
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultResponseMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultResponseMessage(int statusCode)
        {
            string code;

            switch (statusCode)
            {
                case 400:
                    return "Bad Request";        
                case 401:
                    return "You are not Authorized";                
                case 404:
                    return "Not Found";
                case 500:
                    return "Server Error";
                default:
                    return null;
            }
        }
    }
}
