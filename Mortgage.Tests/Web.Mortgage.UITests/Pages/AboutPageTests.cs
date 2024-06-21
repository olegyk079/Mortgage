using NUnit.Framework;
using OpenQA.Selenium;

namespace Web.Mortgage.UITests
{
    internal class AboutPageTests : BaseTest
    {
        public AboutPageTests() : base("http://127.0.0.1:5500/pages/AboutPage.html?") { }

        [TestCase("+48 45 591 70 41", "ClientPhoneNumber")]
        [TestCase("+48 87 125 65 98", "CompanyPhoneNumber")]

        public void TestPhoneNumbers(string expectedNumber, string elementId)
        {
            //Act

            var actualPhoneNumber = Driver.FindElement(By.Id(elementId)).Text;

            //Assert

            Assert.AreEqual(actualPhoneNumber, expectedNumber);
        }

    }
}
