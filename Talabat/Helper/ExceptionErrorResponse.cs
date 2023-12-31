using System.Net.NetworkInformation;

namespace Talabat.Helper
{
    public class ExceptionErrorResponse:ApiResponse
    {
        public string Details { get; set; }
        public ExceptionErrorResponse(int status, string message=null ,string detailes =null):base( status, message)
        {
            Details = detailes;
        }
    }
}
