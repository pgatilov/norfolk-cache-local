using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NorfolkCache.Services;
using NorfolkCacheWebApp.Controllers;

namespace NorfolkCacheWebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var mock = new Mock<ICacheService>();
            mock.Setup(s => s.GetInfo()).Returns(new CacheServiceInfo { });

            // Arrange
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Norfolk Cache", result.ViewBag.Title);
        }
    }
}
