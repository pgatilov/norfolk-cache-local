using System;

namespace NorfolkCache.SpecialServices
{
    public class MyService : IMyService
    {
        private readonly string _name;

        public MyService(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string GetServiceName()
        {
            return _name;
        }
    }
}
