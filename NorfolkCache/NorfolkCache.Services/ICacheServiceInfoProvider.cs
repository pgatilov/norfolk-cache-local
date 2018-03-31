namespace NorfolkCache.Services
{
    public interface ICacheServiceInfoProvider
    {
        CacheServiceInfo GetInfo();    
    }

    public class CacheServiceInfo
    {
        public int TotalGetRequests { get; set; }

        public int TotalSetRequests { get; set; }

        public int TotalNamespaces { get; set; }

        public int TotalKeys { get; set; }
    }
}
