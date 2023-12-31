using System.Collections.Generic;

namespace Talabat.Helper
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse():base(400)
        {
            
        }
    }
}
