using System;

namespace NorfolkCache.Services
{
    public interface IExceptionLog
    {
        void Log(Exception e);
    }
}
