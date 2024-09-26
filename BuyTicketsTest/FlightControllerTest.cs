using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BuyTickets.models;
using BuyTickets.Controllers;

namespace BuyTicketsTest
{
    public class FlightControllerTest
    {
        [Fact]
        public void Check_CreateMethod_Add_FlightObjectToList()
        {
            //Arrange
            
            FlightController flightController = new FlightController();
            Enterprise enterprise = new Enterprise("LATAM");
            Flight flight = new Flight("RECIFE", "SAO PAULO", "27/09/2024", "08:00", "10:00", enterprise);

            //Act

            flightController.Create(flight);

            var flightResult = flightController.SearchById(flight.Id);
            
            //Assert

            Assert.Equal(flight, flightResult);
        }
    }
}