using System;
using System.Collections.Generic;

namespace NorfolkCache.Services
{
    public struct KeyValueRecord
    {
        public string Key;

        public List<string> Values;

        public object SyncObject;

        public KeyValueRecord(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            Key = key;
            Values = new List<string>();
            SyncObject = new object();
        }

        public KeyValueRecord(string key, string value)
            : this(key)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            Values.Add(value);
        }
    }
}
