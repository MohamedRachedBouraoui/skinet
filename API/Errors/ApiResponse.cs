using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMsgForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMsgForStatusCode(int statusCode)
        {
            return statusCode switch
            {

                400 => "You have made a Bad Request.",
                401 => "You are Not Authorized.",
                404 => "Resource Not Found.",
                500 => "An Server Error occured ! Please contact the Support Team.",

                _ => null
            };
        }
    }
}