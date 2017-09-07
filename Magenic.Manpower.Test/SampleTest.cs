using System;
using AutoMapper;
using Magenic.Manpower.WebApi.ServiceLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Magenic.Manpower.Test
{
    [TestClass]
    public class SampleTest
    {
        [TestMethod]
        public void TestMethodPassing()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestMethodFailing()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void Test1()
        {
            //Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();
            var mockMapper = new Mock<IMapper>();

            //Act
            var lookupSvc = new LookupService(mockServiceProvider.Object, mockMapper.Object);


            //Assert
            Assert.IsNotNull(lookupSvc);
        }

        [TestMethod]
        public void Test2()
        {
            //Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();
            var mockMapper = new Mock<IMapper>();

            //Act
            var lookupSvc = new LookupService(mockServiceProvider.Object, mockMapper.Object);


            //Assert
            mockServiceProvider.Verify(x => x.GetService(It.IsAny<Type>()), Times.Once);
        }

        [TestMethod]
        public void Test3()
        {
            //new creation


        }
    }
}
