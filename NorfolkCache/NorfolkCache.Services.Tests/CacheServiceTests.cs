using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NorfolkCache.Services.Tests
{
    [TestClass]
    public class CacheServiceTests
    {
        [TestMethod]
        public void GetNamespaces_EmptyCache_EmptyCollectionReturned()
        {
            var cache = new CacheService();

            var namespaces = cache.GetNamespaces();

            Assert.IsNotNull(namespaces);
            Assert.AreEqual(0, namespaces.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetNamespaceKeys_NullNamespace_ExceptionThrown()
        {
            var cache = new CacheService();

            IList<string> keys;
            cache.TryGetNamespaceKeys(null, out keys);
        }

        [TestMethod]
        public void TryGetNamespaceKeys_NamespaceNotExists_ExceptionThrown()
        {
            var cache = new CacheService();

            IList<string> keys;
            var result = cache.TryGetNamespaceKeys("namespace", out keys);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGet_NamespaceIsNull_ExceptionThrown()
        {
            var cache = new CacheService();

            string value;
            cache.TryGet(null, "key", out value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGet_KeyIsNull_ExceptionThrown()
        {
            var cache = new CacheService();

            string value;
            cache.TryGet("namespace", null, out value);
        }

        [TestMethod]
        public void Get_KeyValueIsNotExist_ExceptionThrown()
        {
            var cache = new CacheService();

            string value;
            var exists = cache.TryGet("namespace", "key", out value);

            Assert.AreEqual(false, exists);
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void TryGet_NamespaceAndKeyAreCorrect_ValueReturned()
        {
            var cache = new CacheService();
            cache.Set("namespace", "key", "value");

            string value;
            var exists = cache.TryGet("namespace", "key", out value);

            Assert.AreEqual(true, exists);
            Assert.AreEqual("value", value);
        }

        [TestMethod]
        public void TryGet_KeyValueIsReplaced_NewValueReturned()
        {
            var cache = new CacheService();
            cache.Set("namespace", "key", "value1");

            cache.Set("namespace", "key", "value2");
            string value;
            var exists = cache.TryGet("namespace", "key", out value);

            Assert.AreEqual(true, exists);
            Assert.AreEqual("value2", value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveNamespace_NullNamespace_ExceptionThrown()
        {
            var cache = new CacheService();

            cache.RemoveNamespace(null);
        }

        [TestMethod]
        public void RemoveNamespace_NamespaceExists_NamespaceRemoved()
        {
            var cache = new CacheService();
            cache.Set("namespace", "key", "value");

            cache.RemoveNamespace("namespace");

            IList<string> keys;
            var result = cache.TryGetNamespaceKeys("namespace", out keys);
            Assert.AreEqual(false, result);
            Assert.AreEqual(null, keys);
        }

        [TestMethod]
        public void RemoveNamespace_NamespaceNotExist_Nothing()
        {
            var cache = new CacheService();

            cache.RemoveNamespace("namespace");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveKey_NullNamespace_ExceptionThrown()
        {
            var cache = new CacheService();

            cache.RemoveKey(null, "key");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveKey_NullKey_ExceptionThrown()
        {
            var cache = new CacheService();

            cache.RemoveKey("namespace", null);
        }

        [TestMethod]
        public void RemoveKey_NamespaceNotExist_Nothing()
        {
            var cache = new CacheService();

            cache.RemoveKey("namespace", "key");
        }

        [TestMethod]
        public void RemoveKey_NamespaceExists_Nothing()
        {
            var cache = new CacheService();
            cache.Set("namespace", "key", "value");

            cache.RemoveKey("namespace", "key");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Set_NamespaceIsNull_ExceptionThrown()
        {
            var cache = new CacheService();

            cache.Set(null, "key", "value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Set_KeyIsNull_ExceptionThrown()
        {
            var cache = new CacheService();

            cache.Set("namespace", null, "value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Set_ValueIsNull_ExceptionThrown()
        {
            var cache = new CacheService();

            cache.Set("namespace", "key", null);
        }

        [TestMethod]
        public void Set_NamespaceNotExist_KeyValueAdded()
        {
            var cache = new CacheService();

            cache.Set("namespace", "key", "value");

            IList<string> keys;
            var result = cache.TryGetNamespaceKeys("namespace", out keys);
            Assert.AreEqual(true, result);
            Assert.AreEqual(1, keys.Count);
            Assert.AreEqual("key", keys[0]);
        }

        [TestMethod]
        public void Set_KeyValueIsAddedTwice_KeyValueAdded()
        {
            var cache = new CacheService();

            cache.Set("namespace", "key", "value");
            cache.Set("namespace", "key", "value");

            IList<string> keys;
            var result = cache.TryGetNamespaceKeys("namespace", out keys);
            Assert.AreEqual(true, result);
            Assert.AreEqual(1, keys.Count);
            Assert.AreEqual("key", keys[0]);
        }

        [TestMethod]
        public void Set_NamespaceExists_KeyValueAdded()
        {
            var cache = new CacheService();
            cache.Set("namespace", "key1", "value1");

            cache.Set("namespace", "key2", "value2");

            string value;
            var result = cache.TryGet("namespace", "key2", out value);
            Assert.AreEqual(true, result);
            Assert.AreEqual("value2", value);
        }
    }
}
