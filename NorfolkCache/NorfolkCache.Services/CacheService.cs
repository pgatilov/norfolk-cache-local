using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NorfolkCache.Services
{
    public class CacheService : ICacheService, ICacheServiceInfoProvider
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private readonly Dictionary<string, NamespaceRecord> _cache = new Dictionary<string, NamespaceRecord>();
        private volatile int _totalGetRequests;
        private volatile int _totalSetRequests;
        private volatile int _totalNamespaces;
        private volatile int _totalKeys;

        public IList<string> GetNamespaces()
        {
            _cacheLock.EnterReadLock();
            var keys = _cache.Keys.ToArray();
            _cacheLock.ExitReadLock();

            return keys;
        }

        public bool TryGetNamespaceKeys(string @namespace, out IList<string> keys)
        {
            if (string.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            NamespaceRecord ns;

            _cacheLock.EnterReadLock();
            var nsExists = _cache.TryGetValue(@namespace, out ns);
            _cacheLock.ExitReadLock();

            if (nsExists == false)
            {
                keys = null;
                return false;
            }

            lock (ns.SyncObject)
            {
                keys = ns.KeyValues.Keys.ToArray();
                return true;
            }
        }

        public bool TryGet(string @namespace, string key, out string value)
        {
            if (string.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            NamespaceRecord ns;

            _cacheLock.EnterReadLock();
            bool nsExists = _cache.TryGetValue(@namespace, out ns);
            _cacheLock.ExitReadLock();

            if (nsExists == false)
            {
                value = null;
                return false;
            }

            KeyValueRecord kv;

            lock (ns.SyncObject)
            {
                if (ns.KeyValues.TryGetValue(key, out kv) == false)
                {
                    value = null;
                    return false;
                }
            }

            lock (kv.SyncObject)
            {
                if (kv.Values.Count > 0)
                {
                    value = kv.Values[0];
                    Interlocked.Increment(ref _totalGetRequests);
                    return true;
                }
            }

            value = null;
            return false;
        }

        public void RemoveNamespace(string @namespace)
        {
            if (string.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            NamespaceRecord ns;
            bool removed;

            _cacheLock.EnterWriteLock();
            if (removed = _cache.TryGetValue(@namespace, out ns))
            {
                _cache.Remove(@namespace);
            }

            _cacheLock.ExitWriteLock();

            if (removed)
            {
                Interlocked.Decrement(ref _totalNamespaces);

                lock (ns.SyncObject)
                {
                    Interlocked.Add(ref _totalKeys, -ns.KeyValues.Count);
                }
            }
        }

        public void RemoveKey(string @namespace, string key)
        {
            if (string.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            NamespaceRecord ns;

            _cacheLock.EnterReadLock();
            bool nsExists = _cache.TryGetValue(@namespace, out ns);
            _cacheLock.ExitReadLock();

            if (nsExists)
            {
                lock (ns.SyncObject)
                {
                    if (ns.KeyValues.ContainsKey(key))
                    {
                        ns.KeyValues.Remove(key);
                        Interlocked.Decrement(ref _totalKeys);
                    }
                }
            }
        }

        public void Set(string @namespace, string key, string value)
        {
            if (string.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            NamespaceRecord ns;

            _cacheLock.EnterReadLock();
            bool nsExists = _cache.TryGetValue(@namespace, out ns);
            _cacheLock.ExitReadLock();

            if (nsExists)
            {
                KeyValueRecord kv;
                bool kvExists;

                lock (ns.SyncObject)
                {
                    kvExists = ns.KeyValues.TryGetValue(key, out kv);

                    if (kvExists == false)
                    {
                        ns.Add(key, value);
                        Interlocked.Increment(ref _totalKeys);
                    }
                }

                if (kvExists)
                {
                    lock (kv.SyncObject)
                    {
                        kv.Values.Clear();
                        kv.Values.Add(value);
                    }
                }
            }
            else
            {
                ns = new NamespaceRecord(@namespace, key, value);

                _cacheLock.EnterWriteLock();
                _cache.Add(@namespace, ns);
                _cacheLock.ExitWriteLock();

                Interlocked.Increment(ref _totalNamespaces);
                Interlocked.Increment(ref _totalKeys);
            }

            Interlocked.Increment(ref _totalSetRequests);
        }

        public void Clear()
        {
            _cacheLock.EnterWriteLock();
            _cache.Clear();
            _cacheLock.ExitWriteLock();

            Interlocked.Exchange(ref _totalNamespaces, 0);
            Interlocked.Exchange(ref _totalKeys, 0);
        }

        public CacheServiceInfo GetInfo()
        {
            return new CacheServiceInfo
            {
                TotalGetRequests = _totalGetRequests,
                TotalSetRequests = _totalSetRequests,
                TotalNamespaces = _totalNamespaces,
                TotalKeys = _totalKeys
            };
        }
    }
}
