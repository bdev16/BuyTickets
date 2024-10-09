using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.models;


namespace BuyTickets.views
{
    public class MenuView
    {
        private readonly FlightControllerView _flightControllerView;
        private readonly EnterpriseControllerView _enterpriseControllerView;

        public MenuView(FlightControllerView flightControllerView, EnterpriseControllerView enterpriseControllerView)
        {
            _flightControllerView = flightControllerView;
            _enterpriseControllerView = enterpriseControllerView;
        }

    }
}