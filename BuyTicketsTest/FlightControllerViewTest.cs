using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BuyTickets.models;
using BuyTickets.Controllers;
using System.ComponentModel.Design;
using BuyTickets.views;
using BuyTickets;
using BuyTicketsTest;
using Xunit.Abstractions;
using System.Text;

namespace BuyTicketsTest
{
    public class FlightControllerViewTest
    {  
        private readonly FlightControllerViewFixture _fixture;
        private readonly ITestOutputHelper _output;

        public FlightControllerViewTest(FlightControllerViewFixture fixture, ITestOutputHelper output)
        {
            _output = output;
            _fixture = fixture;
        }
    }
}