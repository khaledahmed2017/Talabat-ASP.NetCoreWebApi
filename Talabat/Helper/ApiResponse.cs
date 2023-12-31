using Microsoft.AspNetCore.Diagnostics;

namespace Talabat.Helper
{
    public class ApiResponse
    {
        public string message { get; set; }
        public int Status { get; set; }
        public ApiResponse(int status, string message = null)
        {
            this.message = message??GetDefaultMessageForStatus(status);
            this.Status = status;
        }
        public string GetDefaultMessageForStatus(int status)
        {
            return status switch
            {
                400 => "Abad Request,you have made",
                401 => "Authorized,you are not",
                404 => "Resource found , it was not",
                500 => "Errors are the path to ............",
                _ => null
            };
        }
    }
}
