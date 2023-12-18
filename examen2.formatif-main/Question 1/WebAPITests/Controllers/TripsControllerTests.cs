using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebAPI.Services;
using WebAPI.Models;

namespace WebAPI.Controllers.Tests
{
    [TestClass()]
    public class TripsControllerTests
    {
        [TestMethod()]
        public void GetTrip_Ok()
        {
            Mock<TripsService> serviceMock = new Mock<TripsService>();
            Mock<TripsController> controllerMock = new Mock<TripsController>(serviceMock.Object) { CallBase = true };

            List<Trip> publicTrips = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    Title = "Test",
                    IsPublic = true,
                    Users = new List<DemoUser>()
                }
            };
            List<Trip> privateTrips = new List<Trip>
            {
                new Trip
                {
                    Id = 2,
                    Title = "Test",
                    IsPublic = false,
                    Users = new List<DemoUser>()
                }
            };

            string test = null;

            serviceMock.Setup(p => p.GetPublicTrips()).ReturnsAsync(publicTrips);

            serviceMock.Setup(p => p.GetUserTrips(It.IsAny<string>())).ReturnsAsync(privateTrips);

            controllerMock.Setup(t => t.UserId).Returns(test);

            var actionResult = controllerMock.Object.GetTrips();

            var result = actionResult.Result;

            GetTripsDTO tripResult = (GetTripsDTO)result.Value;

            Assert.AreEqual(publicTrips, tripResult.PublicTrips);

            Assert.AreEqual(0, tripResult.UserTrips.Count);


        }
    }
}