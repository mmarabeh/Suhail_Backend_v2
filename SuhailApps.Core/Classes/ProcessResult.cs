using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SuhailApps.Core.Classes
{
    public class ProcessResult<T>
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public T ResultObj { get; set; }

        public string ErrorCode { get; set; }

        public HttpStatusCode StatusCode { get; set; }

    }
}
