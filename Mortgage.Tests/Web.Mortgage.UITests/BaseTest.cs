using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Web.Mortgage.UITests
{
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        private string Url;

        public BaseTest(string url)
        {
            Url = url;
            SetUp();
        }

        [SetUp]
        protected void SetUp()
        {
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl(Url);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
