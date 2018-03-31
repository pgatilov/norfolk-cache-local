using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NorfolkCache.Services.CacheLog
{
    public class CacheServiceTraceLog : CacheServiceDecorator
    {
        public CacheServiceTraceLog(ICacheService cacheService)
            : base(cacheService)
        {
        }

        public override void Clear()
        {
            Trace.TraceInformation("CacheService.Clear() enter");
            try
            {
                base.Clear();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }
            Trace.TraceInformation("CacheService.Clear() exit");
        }

        public override CacheServiceInfo GetInfo()
        {
            Trace.TraceInformation("CacheService.GetInfo() enter");

            CacheServiceInfo result;
            try
            {
                result = base.GetInfo();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }

            Trace.TraceInformation("CacheService.GetInfo() exit");
            return result;
        }

        public override IList<string> GetNamespaces()
        {
            Trace.TraceInformation("CacheService.GetNamespaces()");

            IList<string> result;
            try
            {
                result = base.GetNamespaces();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }

            Trace.TraceInformation("CacheService.GetNamespaces() exit");
            return result;
        }

        public override void RemoveKey(string @namespace, string key)
        {
            Trace.TraceInformation("CacheService.RemoveKey() enter");

            try
            {
                base.RemoveKey(@namespace, key);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }

            Trace.TraceInformation("CacheService.RemoveKey() exit");
        }

        public override void RemoveNamespace(string @namespace)
        {
            Trace.TraceInformation("CacheService.RemoveNamespace() enter");

            try
            {
                base.RemoveNamespace(@namespace);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }

            Trace.TraceInformation("CacheService.RemoveNamespace() exit");
        }

        public override void Set(string @namespace, string key, string value)
        {
            Trace.TraceInformation("CacheService.Set() enter");

            try
            {
                base.Set(@namespace, key, value);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }

            Trace.TraceInformation("CacheService.Set() exit");
        }

        public override bool TryGet(string @namespace, string key, out string value)
        {
            Trace.TraceInformation("CacheService.TryGet() enter");

            bool result;
            try
            {
                result = base.TryGet(@namespace, key, out value);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }

            Trace.TraceInformation("CacheService.TryGet() exit");
            return result;
        }

        public override bool TryGetNamespaceKeys(string @namespace, out IList<string> keys)
        {
            Trace.TraceInformation("CacheService.TryGetNamespaceKeys() enter");

            bool result;
            try
            {
                result = base.TryGetNamespaceKeys(@namespace, out keys);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }

            Trace.TraceInformation("CacheService.TryGetNamespaceKeys() exit");
            return result;
        }
    }
}
