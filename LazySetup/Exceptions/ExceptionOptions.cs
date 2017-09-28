using System;
using System.Collections.Generic;
using System.Text;

namespace LazySetup.Exceptions
{
    public class ExceptionOptions
    {
        public Action OnError { get; set; }
        public ExceptionType LogWhen { get; set; }
    }

    public enum ExceptionType
    {
        StatusCode300OrHigher = 300,
        StatusCode400OrHigher = 400,
        StatusCode500OrHigher = 500
    }
}
