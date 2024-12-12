using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyTickets.Controllers;
using BuyTickets.models;
using Xunit;

namespace BuyTicketsTest
{
    public class EnterpriseControllerTest : IClassFixture<FixtureControllersAndViews>
    {

        private readonly FixtureControllersAndViews _fixture;

        public EnterpriseControllerTest(FixtureControllersAndViews fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void Check_CreateMethod_Add_EnterpriseObjectToList()
        {
            //Arrange
            
            var enterpriseController = _fixture.EnterpriseController;
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");

            //Act

            enterpriseController.Create(enterprise);

            var enterpriseResult = enterpriseController.SearchById(enterprise.Id);
            
            //Assert

            Assert.Equal(enterprise, enterpriseResult);
        }

        [Fact]
        public void Check_CreateMethod_ReturnNullIfReceiveNullValue()
        {
            //Arrange
        
            var enterpriseController = _fixture.EnterpriseController;

            //Act

            var outputResult = enterpriseController.Create(null);

            //Assert

            Assert.Equal(null, outputResult);
        }

        [Fact]
        public void Check_SearchAllMethod_ReturnsAEnterpriseList()
        {
            //Arrange

            var enterpriseController = _fixture.EnterpriseController;
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

            var enterpriseController = _fixture.EnterpriseController;
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
        public void Check_MethodSearchById_ReturnNullIfReceiveIdNonexistent()
        {
            //Arrange

            var enterpriseController = _fixture.EnterpriseController;

            //Act

            Guid guid = Guid.Parse("d2c5f3e2-a5f9-4e87-9b22-b3f3d76458a1");
            var flightResult = enterpriseController.SearchById(guid);

            //Assert

            Assert.Equal(null, flightResult);
        }

        [Fact]
        public void Check_MethodUpdate_ModifieTheFlightRegisteredByTheFlightInformed()
        {
            //Arrange

            var enterpriseController = _fixture.EnterpriseController;
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

            var enterpriseController = _fixture.EnterpriseController;
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");

            //Act

            enterpriseController.Create(enterprise);

            var enterpriseResult = enterpriseController.SearchById(enterprise.Id);

            enterpriseController.Delete(enterpriseResult.Id);

            var enterpriseResultList = enterpriseController.SearchAll();

            //Assert

            Assert.Null(enterpriseResultList);
        }

        [Fact]
        public void Check_MethodLogin_ReturnsCustomerCaseLoginSuccess()
        {
            // Arrange

            var enterpriseController = _fixture.EnterpriseController;
            Enterprise enterprise = new Enterprise("LATAM", "latamairlines@gmail.com", "latam123", "50405900000592");

            // Act

            enterpriseController.Create(enterprise);

            var loginResult = enterpriseController.Login(enterprise.Email, enterprise.Password);

            // Assert

            Assert.NotNull(loginResult);
        }
    }
}