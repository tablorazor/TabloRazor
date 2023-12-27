using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloRazor.Table.WebApiDataProvider
{
    public class ExceptionHandlingResultModel
    {
        public  string Message { get; set; }
        public int ErrorCode { get; set; }
        public List<ExceptionOrValidationError> Errors { get; set; }
    }
}
