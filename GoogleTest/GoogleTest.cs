using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Reflection;

namespace GoogleTest
{
    [TestClass]
    public class GoogleTest
    {
        private IWebDriver _driver;
        public GoogleTest()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _driver = new ChromeDriver(outPutDirectory);
            _driver.Navigate().GoToUrl("https://www.google.com/");            
        }

        [TestMethod]
        public void SearchDoorInGoogle()
        {
            var searchButtonLocator = By.XPath("//div[@class='FPdoLc lJ9FBc']");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(searchButtonLocator));

            var searchField = _driver.FindElement(By.Name("q"));
            var searchButton = _driver.FindElement(searchButtonLocator);
          
            searchField.SendKeys("Door Wikipedia");
            searchButton.Submit();

            var searchResult = _driver.FindElement(By.XPath("//h3[.='Door - Wikipedia']")).Text.ToString();

            Assert.AreEqual(searchResult,"Door - Wikipedia");
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Quit();
        }
    }
}
