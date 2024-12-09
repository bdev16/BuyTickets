using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.models;
using Xunit.Abstractions;

namespace BuyTicketsTest.views
{
    public class EnterpriseControllerViewTest : IClassFixture<FixtureControllersAndViews>
    {
        private readonly FixtureControllersAndViews _fixture;
        private readonly ITestOutputHelper _output;

        public EnterpriseControllerViewTest(FixtureControllersAndViews fixture, ITestOutputHelper output)
        {
            _output = output;
            _fixture = fixture;
        }
    }
}