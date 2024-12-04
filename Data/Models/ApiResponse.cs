using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ApiResponse<T, TEnum>
    {
        public bool Status { get; set; }

        public string Message { get; set; }
        public TEnum EnumMessage { get; set; }

        public T Data { get; set; }

        public Exception Exception { get; set; }


        public ApiResponse()
        {
            Status = true;
        }
    }
}
