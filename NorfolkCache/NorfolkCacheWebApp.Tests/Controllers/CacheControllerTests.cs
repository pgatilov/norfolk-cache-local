using System;
using System.Linq;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NorfolkCache.Services;
using NorfolkCacheWebApp.Controllers;

namespace NorfolkCacheWebApp.Tests.Controllers
{
    [TestClass]
    public class CacheControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetNamespaces_ExceptionThrown_InternalServerError()
        {
            var mock = new Mock<ICacheService>();
            mock.Setup(s => s.GetNamespaces()).Throws<Exception>();
            var controller = new CacheController(mock.Object);

            controller.GetNamespaces();
        }

        [TestMethod]
        public void GetNamespaces_EmptyCollection_EmptyCollectionReturned()
        {
            var mock = new Mock<ICacheService>();
            mock.Setup(s => s.GetNamespaces()).Returns(new string[] { });
            var controller = new CacheController(mock.Object);

            var result = controller.GetNamespaces();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetNamespaces_ThreeElements_CollectionReturned()
        {
            var mock = new Mock<ICacheService>();
            mock.Setup(s => s.GetNamespaces()).Returns(new string[] { "namespace1", "namespace2", "namespace3" });
            var controller = new CacheController(mock.Object);

            var result = controller.GetNamespaces();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("namespace1", result.ElementAt(0).Namespace);
            Assert.AreEqual("namespace2", result.ElementAt(1).Namespace);
            Assert.AreEqual("namespace3", result.ElementAt(2).Namespace);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void RemoveNamespaces_ExceptionThrown_InternalServerError()
        {
            var mock = new Mock<ICacheService>();
            mock.Setup(s => s.Clear()).Throws<Exception>();
            var controller = new CacheController(mock.Object);

            controller.RemoveNamespaces();
        }

        [TestMethod]
        public void RemoveNamespaces_NamespacesRemoved_NoContent()
        {
            var mock = new Mock<ICacheService>();
            mock.Setup(s => s.Clear());
            var controller = new CacheController(mock.Object);

            controller.RemoveNamespaces();
        }
    }
}
