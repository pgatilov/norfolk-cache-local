using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using NorfolkCache.Services;
using NorfolkCacheWebApp.Models;
using Swashbuckle.Swagger.Annotations;

namespace NorfolkCacheWebApp.Controllers
{
    /// <summary>
    /// Represents a cache controller.
    /// </summary>
    public class CacheController : ApiController
    {
        private readonly ICacheService _cacheService;
        private readonly IExceptionLog _exceptionLog = new DumpExceptionLog();

        /// <summary>
        /// Initialize a new instance of the <see cref="CacheController"/> class.
        /// </summary>
        /// <param name="cacheService"></param>
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        /// <summary>
        /// Gets a list of all cache namespaces.
        /// </summary>
        /// <returns>A list of all cache namespaces.</returns>
        [HttpGet]
        [Route("api/cache/namespaces")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public IEnumerable<BriefNamespaceModel> GetNamespaces()
        {
            try
            {
                var namespaces = _cacheService.GetNamespaces();

                return namespaces.Select(ns => new BriefNamespaceModel
                {
                    Namespace = ns
                }).ToArray();
            }
            catch (Exception e)
            {
                _exceptionLog.Log(e);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Remove all namespaces.
        /// </summary>
        [HttpDelete]
        [Route("api/cache/namespaces")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Namespace is removed successfully")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public void RemoveNamespaces()
        {
            try
            {
                _cacheService.Clear();
            }
            catch (Exception e)
            {
                _exceptionLog.Log(e);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets full namespace information.
        /// </summary>
        /// <param name="namespace">A namespace name.</param>
        /// <returns>An <see cref="FullNamespaceModel"/>.</returns>
        [HttpGet]
        [Route("api/cache/namespaces/{namespace}")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Namespace is not found")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public FullNamespaceModel GetNamespace(string @namespace)
        {
            try
            {
                var namespaces = _cacheService.GetNamespaces();

                var result = namespaces.Where(ns => ns == @namespace).Select(ns => new FullNamespaceModel
                {
                    Namespace = ns
                }).FirstOrDefault();

                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception e)
            {
                _exceptionLog.Log(e);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Sets key-value pair for specified namespace.
        /// </summary>
        /// <param name="namespace">A namespace name.</param>
        /// <param name="key">A key.</param>
        /// <param name="value">A value.</param>
        [HttpPost]
        [Route("api/cache/namespaces/{namespace}/{key}/{value}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Key-value pair is set successfully")]
        public void SetKeyValue(string @namespace, string key, string value /*, [FromBody] string bodyValue */)
        {
            try
            {
                _cacheService.Set(@namespace, key, value);
            }
            catch (Exception e)
            {
                _exceptionLog.Log(e);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Removes a specified namespace with all key-value pairs.
        /// </summary>
        /// <param name="namespace">A namespace name.</param>
        [HttpDelete]
        [Route("api/cache/namespaces/{namespace}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotImplemented, Description = "Method is not implemented")]
        public void RemoveNamespace(string @namespace)
        {
            throw new HttpResponseException(HttpStatusCode.NotImplemented);
        }

        /// <summary>
        /// Gets all namespace keys.
        /// </summary>
        /// <param name="namespace">A namespace name.</param>
        /// <returns>A <see cref="NamespaceKeysModel"/>.</returns>
        [HttpGet]
        [Route("api/cache/keys/{namespace}")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "A namespace is not found")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public NamespaceKeysModel GetNamespaceKeys(string @namespace)
        {
            try
            {
                IList<string> keys;
                if (_cacheService.TryGetNamespaceKeys(@namespace, out keys))
                {
                    return new NamespaceKeysModel
                    {
                        Namespace = @namespace,
                        KeyCount = keys.Count,
                        Keys = keys
                    };
                }
            }
            catch (Exception e)
            {
                _exceptionLog.Log(e);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Gets a key-value pair for specified namespace.
        /// </summary>
        /// <param name="namespace">A namespace name.</param>
        /// <param name="key">A key.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cache/keys/{namespace}/{key}")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "A namespace or a key is not found")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public KeyValueModel GetKeyValue(string @namespace, string key)
        {
            try
            {
                string value;
                if (_cacheService.TryGet(@namespace, key, out value))
                {
                    return new KeyValueModel
                    {
                        Namespace = @namespace,
                        Key = key,
                        Values = new[] { value }
                    };
                }
            }
            catch (Exception e)
            {
                _exceptionLog.Log(e);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Removes a key with all associated values in the specified namespace.
        /// </summary>
        /// <param name="namespace">A namespace.</param>
        /// <param name="key">A key.</param>
        [HttpDelete]
        [Route("api/cache/keys/{namespace}/{key}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Key is removed successfully")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public void RemoveKey(string @namespace, string key)
        {
            try
            {
                _cacheService.RemoveKey(@namespace, key);
            }
            catch (Exception e)
            {
                _exceptionLog.Log(e);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
