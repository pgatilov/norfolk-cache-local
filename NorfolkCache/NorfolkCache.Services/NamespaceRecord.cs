using System;
using System.Collections.Generic;

namespace NorfolkCache.Services
{
    public struct NamespaceRecord
    {
        public string Name;

        public Dictionary<string, KeyValueRecord> KeyValues;

        public object SyncObject;

        public NamespaceRecord(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            KeyValues = new Dictionary<string, KeyValueRecord>();
            SyncObject = new object();
        }

        public NamespaceRecord(string name, string key, string value)
            : this(name)
        {
            Add(key, value);
        }

        public void Add(string key, string value)
        {
            KeyValues.Add(key, new KeyValueRecord(key, value));
        }
    }
}
