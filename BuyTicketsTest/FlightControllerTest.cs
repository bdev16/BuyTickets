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
    public class FlightControllerTest
    {
        [Fact]
        public void Check_CreateMethod_Add_FlightObjectToList()
        {
            //Arrange
            
            List<Flight> flights = new List<Flight>();
            FlightController flightController = new FlightController(flights);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123");
            Flight flight = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise);

            //Act

            flightController.Create(flight);

            var flightResult = flightController.SearchById(flight.Id);
            
            //Assert

            Assert.Equal(flight, flightResult);
        }

        [Fact]
        public void Check_SearchAllMethod_ReturnsAFlightList()
        {
            //Arrange

            List<Flight> flights = new List<Flight>();
            FlightController flightController = new FlightController(flights);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123");
            Flight flight = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise);

            //Act

            flightController.Create(flight);

            var flightResult = flightController.SearchAll();
            
            //Assert

            Assert.NotEmpty(flightResult);
        }

        [Fact] 
        public void Check_MethodSearchById_ReturnTheFlightInformed()
        {
            //Arrange

                List<Flight> flights = new List<Flight>();
                FlightController flightController = new FlightController(flights);
                Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123");
                Flight flight = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise); 

            //Act

                flightController.Create(flight);

                var flightResult = flightController.SearchById(flight.Id);

            //Assert

                Assert.Equal(flightResult, flight);
        }

        [Fact]
        public void Check_MethodSearchByEnterprise_ReturnTheFlightListToEnterpriseInformed()
        {
            // Given

            List<Flight> flights = new List<Flight>();
            FlightController flightController = new FlightController(flights);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123");
            Flight flight = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise); 
            Flight flight2 = new Flight("RECIFE", "SAO PAULO", "10/10/2024", "09:00", "11:00", enterprise); 
            Flight flight3 = new Flight("RECIFE", "RIO DE JANEIRO", "10/10/2024", "09:00", "11:00", enterprise); 

            // When

            flightController.Create(flight);
            flightController.Create(flight2);
            flightController.Create(flight3);

            var resultFlightToEnterprise = flightController.SearchByEnterprise(enterprise);
        
            // Then

             Assert.NotEmpty(resultFlightToEnterprise);
        }

        [Fact]
        public void Check_MethodUpdate_ModifieTheFlightRegisteredByTheFlightInformed()
        {
            //Arrange

            List<Flight> flights = new List<Flight>();
            FlightController flightController = new FlightController(flights);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123");
            Flight flight = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise);
            Flight flightCopy = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise);

            //Act

            flightController.Create(flight);

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
            List<Flight> flights = new List<Flight>();
            FlightController flightController = new FlightController(flights);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123");
            Flight flight = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise);

            //Act

            flightController.Create(flight);

            var flightResult = flightController.SearchById(flight.Id);

            flightController.Delete(flightResult.Id);

            var flightResultList = flightController.SearchAll();

            //Assert

            Assert.Null(flightResultList);
        }
    }
}