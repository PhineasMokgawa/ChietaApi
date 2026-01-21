using System.Threading.Tasks;
using CHIETAMIS.Models.TokenAuth;
using CHIETAMIS.Web.Controllers;
using Shouldly;
using Xunit;

namespace CHIETAMIS.Web.Tests.Controllers
{
    public class HomeController_Tests: CHIETAMISWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}