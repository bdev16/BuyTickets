using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BuyTickets.models;
using BuyTickets.Controllers;
using System.Data.Common;

namespace BuyTicketsTest
{
    public class FlightControllerTest : IClassFixture<FixtureControllersAndViews>
    {

        private readonly FixtureControllersAndViews _fixture;

        public FlightControllerTest(FixtureControllersAndViews fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Check_CreateMethod_Add_FlightObjectToList()
        {
            //Arrange
        
            FlightController flightController = _fixture.FlightController;
            Enterprise enterprise = _fixture.Enterprise;
            Flight flight = new Flight("manaus", "belem", "27/09/2024", "08:00", "10:00", enterprise);

            //Act

            flightController.Create(flight);

            var flightResult = flightController.SearchById(flight.Id);
            
            //Assert

            Assert.Equal(flight, flightResult);
        }

        [Fact]
        public void Check_CreateMethod_ReturnNullIfReceiveNullValue()
        {
            //Arrange
        
            FlightController flightController = _fixture.FlightController;

            //Act

            var outputResult = flightController.Create(null);

            //Assert

            Assert.Equal(null, outputResult);
        }

        [Fact]
        public void Check_SearchAllMethod_ReturnsAFlightList()
        {
            //Arrange

            FlightController flightController = _fixture.FlightController;

            //Act

            var flightResult = flightController.SearchAll();
            
            //Assert

            Assert.NotEmpty(flightResult);
        }

        [Fact] 
        public void Check_MethodSearchById_ReturnTheFlightInformed()
        {
            //Arrange

            FlightController flightController = _fixture.FlightController;
            Enterprise enterprise = _fixture.Enterprise;
            Flight flight = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise);

            //Act

            flightController.Create(flight);

            var flightResult = flightController.SearchById(flight.Id);

            //Assert

            Assert.Equal(flightResult, flight);
        }

        [Fact] 
        public void Check_MethodSearchById_ReturnNullIfReceiveIdNonexistent()
        {
            //Arrange

            FlightController flightController = _fixture.FlightController;
            Enterprise enterprise = _fixture.Enterprise;

            //Act

            Guid guid = Guid.Parse("d2c5f3e2-a5f9-4e87-9b22-b3f3d76458a1");
            var flightResult = flightController.SearchById(guid);

            //Assert

            Assert.Equal(null, flightResult);
        }

        [Fact]
        public void Check_MethodUpdate_ModifieTheFlightRegisteredByTheFlightInformed()
        {
            //Arrange

            FlightController flightController = _fixture.FlightController;
            Enterprise enterprise = _fixture.Enterprise;
            Flight flightCopy = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise);
            var flight = _fixture.Flights[0];

            //Act

            flight.Destiny = "RIO DE JANEIRO";

            flightController.Update(flight);

            var flightResultAfterUpdate = flightController.SearchById(flight.Id);

            //Assert

            Assert.NotEqual(flightResultAfterUpdate.Destiny, flightCopy.Destiny);   
            Assert.Equal(flightResultAfterUpdate.Id, flight.Id);
        }

        [Fact]
        public void Check_MethodDelete_RemoveInformedFlightFromFlightList()
        {
            //Arrange

            FlightController flightController = _fixture.FlightController;
            Enterprise enterprise = _fixture.Enterprise;
            var flight = _fixture.Flights[0];

            //Act

            var flightResult = flightController.SearchById(flight.Id);

            flightController.Delete(flightResult.Id);

            var flightResultList = flightController.SearchAll();

            //Assert

            Assert.Null(flightResultList);
        }
    }
}