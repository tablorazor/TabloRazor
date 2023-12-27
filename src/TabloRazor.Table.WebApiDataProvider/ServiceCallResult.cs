using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloRazor.Table.WebApiDataProvider
{
    public class ServiceCallResult<T>
    {
        public T Result { get; set; }
        public ExceptionHandlingResultModel Exception { get; set; }
        public bool Status { get; set; }
    }
}
