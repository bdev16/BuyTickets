using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;
using Xunit;

namespace BuyTicketsTest
{
    public class EnterpriseControllerTest
    {
        [Fact]
        public void Check_CreateMethod_Add_EnterpriseObjectToList()
        {
            //Arrange
            
            EnterpriseController enterpriseController = new EnterpriseController();
            Enterprise enterprise = new Enterprise("LATAM");

            //Act

            enterpriseController.Create(enterprise);

            var enterpriseResult = enterpriseController.SearchById(enterprise.Id);
            
            //Assert

            Assert.Equal(enterprise, enterpriseResult);
        }
    }
}