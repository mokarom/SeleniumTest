using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace SeleniumTestProject
{
    [TestClass]
    public class MySeleniumTest
    {
        private TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;

        public MySeleniumTest()
        {

        }

        [TestInitialize()]
        public void SetupTest()
        {
            appURL = "http://www.google.com/";
            string browser = "Chrome";           

        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void TheGoogleSearchTest()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            using (driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                driver.Navigate().GoToUrl(appURL + "");
                //driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                var element = driver.FindElement(By.Name("q"));
                element.SendKeys("Cheese");
                element.Submit();
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Title.StartsWith("cheese", StringComparison.OrdinalIgnoreCase));
                Assert.IsTrue(driver.Title.StartsWith("cheese", StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }



        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}
