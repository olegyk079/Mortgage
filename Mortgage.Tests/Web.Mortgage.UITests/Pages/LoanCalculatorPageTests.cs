using NUnit.Framework;
using OpenQA.Selenium;

namespace Web.Mortgage.UITests
{
    public class LoanCalculatorPageTests : BaseTest
    {
        public LoanCalculatorPageTests() : base("http://127.0.0.1:5500/pages/LoanCalculatorPage.html?")
        {

        }

        [Test]
        public void EmptyInputTest()
        {
            //Act

            Driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            //Assert

            WaitForAlertAndValidateText("Poproszę wprowadzić wartość kredytu");
        }

        [TestCase("abc", "7", "5")]
        [TestCase("500000%", "7", "5")]
        [TestCase("500000c", "7", "5")]
        public void InvalidAmountInputTypeTest(string principal, string annualInterestRate, string years)
        {
            //Act 

            var amountField = Driver.FindElement(By.Id("amount"));
            amountField.SendKeys(principal);

            //Assert 

            WaitForAlertAndValidateText("Proszę wprowadzić tylko cyfry");
        }

        [TestCase("500000", "abc", "5")]
        [TestCase("500000", "7%", "5")]
        [TestCase("500000", "7a", "5")]
        public void InvalidAnnualInterestInputTypeTest(string principal, string annualInterestRate, string years)
        {
            //Act 

            var amountField = Driver.FindElement(By.Id("amount"));
            amountField.SendKeys(principal);

            var aprField = Driver.FindElement(By.Id("apr"));
            aprField.SendKeys(annualInterestRate);

            //Assert 

            WaitForAlertAndValidateText("Proszę wprowadzić tylko cyfry");
        }

        [TestCase("500000", "7", "abc")]
        [TestCase("500000", "7", "5%")]
        [TestCase("500000", "7", "5c")]
        public void InvalidYearsInputTypeTest(string principal, string annualInterestRate, string years)
        {
            //Act 

            var amountField = Driver.FindElement(By.Id("amount"));
            amountField.SendKeys(principal);

            var aprField = Driver.FindElement(By.Id("apr"));
            aprField.SendKeys(annualInterestRate);

            var yearsField = Driver.FindElement(By.Id("years"));
            yearsField.SendKeys(years);

            //Assert 

            WaitForAlertAndValidateText("Proszę wprowadzić tylko cyfry");
        }

        [Test]
        public void LoanCalculationResultTest()
        {
            //Arrange

            double principal = 500000;
            double annualInterestRate = 7;
            int years = 5;

            //Act

            var amountField = Driver.FindElement(By.Id("amount"));
            amountField.SendKeys(principal.ToString());

            var aprField = Driver.FindElement(By.Id("apr"));
            aprField.SendKeys(annualInterestRate.ToString());

            var yearsField = Driver.FindElement(By.Id("years"));
            yearsField.SendKeys(years.ToString());

            Driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            WaitForElement("payment");
            WaitForElement("total");
            WaitForElement("totalinterest");

            var paymentText = Driver.FindElement(By.Id("payment")).Text;
            var totalText = Driver.FindElement(By.Id("total")).Text;
            var totalinterestText = Driver.FindElement(By.Id("totalinterest")).Text;

            //Assert

            double monthlyPayment = 9900.60;
            double totalPayment = 594035.96;
            double totalInterest = 94035.96;

            Assert.AreEqual(monthlyPayment.ToString("F2"), paymentText);
            Assert.AreEqual(totalPayment.ToString("F2"), totalText);
            Assert.AreEqual(totalInterest.ToString("F2"), totalinterestText);
        }

        private void WaitForElement(string id)
        {
            Wait.Until(Driver => Driver.FindElement(By.Id(id)).Displayed);
        }

        private void WaitForAlertAndValidateText(string expectedText)
        {
            try
            {
                IAlert alert = Wait.Until(Driver => Driver.SwitchTo().Alert());
                Assert.AreEqual(expectedText, alert.Text);
                alert.Accept();
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Alert is expected");
            }
        }
    }
}
