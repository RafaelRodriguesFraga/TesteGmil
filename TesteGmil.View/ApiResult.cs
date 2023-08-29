using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteGmil.View
{
    public class ApiResult<TResult>
    {
        public ApiResult(bool success, int code, TResult data, string error)
        {
            Success = success;
            Code = code;
            Data = data;
            Error = error;
        }

        public bool Success { get; set; }
        public int Code { get; set; }
        public TResult Data { get; set; }
        public string Error { get; set; }
    }
}
