using NUnit.Framework;
using OpenQA.Selenium;

namespace Web.Mortgage.UITests
{
    internal class MainPageTests : BaseTest
    {
        public MainPageTests() : base("http://127.0.0.1:5500/pages/MainPage.html?") { }

        [Test]
        public void IsCorrectHeaderTest()
        {
            //Arrange

            var expectedHeader = "Bank \"ABC\" - najlepszy bank w Europie";

            //Act

            var actualHeader = Driver.FindElement(By.Id("login-header")).Text;

            //Assert

            Assert.AreEqual(actualHeader, expectedHeader);
        }
    }
}