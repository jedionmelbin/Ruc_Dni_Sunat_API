using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunat.WebApi.Models
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            this.Response = true;
        }

        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool Response { get; set; }
        public object Data { get; set; }
    }
}
