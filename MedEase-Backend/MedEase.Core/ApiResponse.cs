namespace MedEase.Core
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, bool success, dynamic data = null, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultResponseMessage(statusCode);
            Data = data;
            Success = success;
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
        public dynamic? Data { get; set; }

        private string? GetDefaultResponseMessage(int statusCode)
        {
            string code;

            switch (statusCode)
            {
                case 200:
                    return "Ok";
                case 201:
                    return "Created";
                case 400:
                    return "Bad Request";        
                case 401:
                    return "Not Authorized";                
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
