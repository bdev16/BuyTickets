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
            
            List<Enterprise> enterprises = new List<Enterprise>();
            EnterpriseController enterpriseController = new EnterpriseController(enterprises);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");

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

            List<Enterprise> enterprises = new List<Enterprise>();
            EnterpriseController enterpriseController = new EnterpriseController(enterprises);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");

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

            List<Enterprise> enterprises = new List<Enterprise>();
            EnterpriseController enterpriseController = new EnterpriseController(enterprises);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");
            Enterprise enterprise1 = new Enterprise("GOL", "golairlines@gmail.com", "gol123", "30207900000790");

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

            List<Enterprise> enterprises = new List<Enterprise>();
            EnterpriseController enterpriseController = new EnterpriseController(enterprises);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");
            Enterprise enterpriseCopy = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");

            //Act

            enterpriseController.Create(enterprise);

            enterprise.FullName = "LATAM AIRLINES";

            enterpriseController.Update(enterprise);

            var enterpriseResultAfterUpdate = enterpriseController.SearchById(enterprise.Id);

            //Assert

            Assert.NotEqual(enterpriseResultAfterUpdate.FullName, enterpriseCopy.FullName);
            Assert.Equal(enterpriseResultAfterUpdate.Id, enterprise.Id);
        }

        [Fact]
        public void Check_MethodDelete_RemoveInformedEnterpriseFromEnterpriseList()
        {
            //Arrange

            List<Enterprise> enterprises = new List<Enterprise>();
            EnterpriseController enterpriseController = new EnterpriseController(enterprises);
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");


            //Act

            enterpriseController.Create(enterprise);

            var enterpriseResult = enterpriseController.SearchById(enterprise.Id);

            enterpriseController.Delete(enterpriseResult.Id);

            var enterpriseResultList = enterpriseController.SearchAll();

            //Assert

            Assert.Null(enterpriseResultList);
        }
    }
}