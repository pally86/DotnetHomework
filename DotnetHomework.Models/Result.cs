using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHomework.Models
{
    public class Result
    {
        public object Data { get; set; }
        public string ContentType { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public static implicit operator Result(void v)
        {
            throw new NotImplementedException();
        }
    }
}
