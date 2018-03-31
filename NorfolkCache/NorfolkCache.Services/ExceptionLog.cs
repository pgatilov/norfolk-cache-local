using System;
using System.Diagnostics;

namespace NorfolkCache.Services
{
    public class ExceptionLog : IExceptionLog
    {
        public void Log(Exception e)
        {
            Trace.TraceError(e.Message);
        }
    }
}
