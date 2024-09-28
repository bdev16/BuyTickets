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

        [Fact]
        public void Check_SearchAllMethod_ReturnsAEnterpriseList()
        {
            //Arrange

            EnterpriseController enterpriseController = new EnterpriseController();
            Enterprise enterprise = new Enterprise("LATAM");

            //Act

            enterpriseController.Create(enterprise);

            var enterpriseListResult = enterpriseController.SearchAll();

            //Assert

            Assert.NotEmpty(enterpriseListResult);
        }

        [Fact]
        public void Check_MethodSearchById_ReturnTheEnterpriseInformed()
        {
            //Arrange

            EnterpriseController enterpriseController = new EnterpriseController();
            Enterprise enterprise = new Enterprise("LATAM");
            Enterprise enterprise1 = new Enterprise("GOL");

            //Act

            enterpriseController.Create(enterprise);
            enterpriseController.Create(enterprise1);

            var enterpriseResult = enterpriseController.SearchById(enterprise.Id);

            //Assert

            Assert.Equal(enterpriseResult, enterprise);
        }

        [Fact]
        public void Check_MethodUpdate_ModifieTheFlightRegisteredByTheFlightInformed()
        {
            //Arrange

            EnterpriseController enterpriseController = new EnterpriseController();
            Enterprise enterprise = new Enterprise("LATAM");
            Enterprise enterpriseCopy = new Enterprise("LATAM");

            //Act

            enterpriseController.Create(enterprise);

            enterprise.Name = "LATAM AIRLINES";

            enterpriseController.Update(enterprise);

            var enterpriseResultAfterUpdate = enterpriseController.SearchById(enterprise.Id);

            //Assert

            Assert.NotEqual(enterpriseResultAfterUpdate.Name, enterpriseCopy.Name);
            Assert.Equal(enterpriseResultAfterUpdate.Id, enterprise.Id);
        }
    }
}