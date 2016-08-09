using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace nunitDemo
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void sampletest()
        {
            int a = 200;
            int b = 100;
            Assert.AreEqual(a, b);
        }

        [Test]
        public void equalOrNot()
        {
            int a = 200;
            int b = 200;
            Assert.AreEqual(a, b);
        }

        [Test]
        public void openGoogle()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.google.com");
            Console.WriteLine(driver.Title);

            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("selenium training in london");
            query.Submit();
            Console.WriteLine(driver.Title);

            driver.Quit();
        }
    }
}