using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace inputProm
{
    [TestFixture]
    public class inputProm
    {
        // class variables
        static IWebDriver driver;
        string label =  "Jiaxin-Testing";


        [SetUp]
        public void login() {            
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [Test]
        public void a_addNewTemplate()
        {
            try
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084");
                Task.Delay(30000).Wait();  // wait for 30 seconds for the user to enter username & password
                // wait for the page to load into MTU site 
                driver.FindElement(By.XPath("//a[4]")).Click();
                driver.FindElement(By.XPath("//a[contains(@class, 'new button')]/div[contains(@class, 'caption1')]")).Click();

                // 1st row
                driver.FindElement(By.XPath("//select[@id='input_promo_country']")).SendKeys("Ghana");
                driver.FindElement(By.XPath("//select[@id='input_promo_carrier']")).SendKeys("AIR-TEL");
                driver.FindElement(By.XPath("//select[@id='input_promo_promo_type']")).SendKeys("10X");
                driver.FindElement(By.XPath("//input[@id='input_promo_label']")).SendKeys(label);

                // 2nd row
                driver.FindElement(By.XPath("//input[@id='input_promo_min_denom']")).SendKeys("5");
                driver.FindElement(By.XPath("//input[@id='input_promo_max_denom']")).SendKeys("10");
                driver.FindElement(By.XPath("//input[@id='input_promo_currency_code']")).SendKeys("US Dollar");
                driver.FindElement(By.XPath("//input[@id='input_promo_restrictions']")).SendKeys("Only For Ghana");
                driver.FindElement(By.XPath("//input[@id='input_promo_is_hard_card']")).Click();
                driver.FindElement(By.XPath("//input[@id='input_promo_is_electronic']")).Click();

                // the rest
                driver.FindElement(By.XPath("//textarea[@id='input_promo_terms']")).SendKeys("Jiaxin is Testing");
                // IVR File is not done yet: driver.FindElement(By.XPath("//select[@id='input_promo_ivr_file_id']")).SendKeys("It's a file");
                driver.FindElement(By.XPath("//textarea[@id='input_promo_comments']")).SendKeys("Hope you have a good day");

                // close the new template
                driver.FindElement(By.XPath("//form[@id='new_input_promo']/div[contains(@class, 'buttons')]/input")).Click();

                Task.Delay(5000).Wait(); // Wait 5 seconds
                string added_msg = driver.FindElement(By.XPath("//p")).Text;
                Assert.IsTrue(added_msg.Contains("Template"));
                Assert.IsTrue(added_msg.Contains("successfully added"));
                Console.WriteLine("Added New Template Successfully");

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void b_updateTemplate()
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084/input_promos?utf8=%E2%9C%93&search=&active=1&type=T&sort=country&dir=asc&period=A&commit=Search");
                Task.Delay(10000).Wait();  // wait for 10 seconds for the user to enter username & password
                // search by label
                driver.FindElement(By.XPath("//input[@id='search']")).SendKeys(label);
                driver.FindElement(By.XPath("//select")).SendKeys("Active");
                driver.FindElement(By.XPath("//input[contains(@class, 'search_button')]")).Click();

                // What if there are more than one record has the same label??
                driver.FindElement(By.XPath("//a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                driver.FindElement(By.XPath("//textarea[@id='input_promo_terms']")).SendKeys("Trying to update");
                driver.FindElement(By.XPath("//div[contains(@class, 'buttons')]/input")).Click();
                IWebElement updated_msg = driver.FindElement(By.XPath("//p"));
                Assert.IsTrue(updated_msg.Text.Contains("Template"));
                Assert.IsTrue(updated_msg.Text.Contains("successfully updated"));
                Console.WriteLine("Updated Added Template Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void c_addedNewProm() 
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084/input_promos?utf8=%E2%9C%93&search=&active=1&type=T&sort=country&dir=asc&period=A&commit=Search");
                Task.Delay(10000).Wait();  // wait for 10 seconds for the user to enter username & password
                // search by label
                driver.FindElement(By.XPath("//input[@id='search']")).SendKeys(label);
                driver.FindElement(By.XPath("//select")).SendKeys("Active");
                driver.FindElement(By.XPath("//input[contains(@class, 'search_button')]")).Click();
                
                // click new prom
                driver.FindElement(By.XPath("//a[contains(@class, 'new_promo button')]/div[contains(@class, 'caption2')]")).Click();
                driver.FindElement(By.XPath("//input[@id='input_promo_promo_dates_attributes_0_ends_at']")).Click(); 
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                IWebElement cal = driver.FindElement(By.XPath("//div[@id='ui-datepicker-div']"));
                driver.SwitchTo().ActiveElement();
                driver.FindElement(By.CssSelector("span.ui-button-icon-primary.ui-icon.ui-icon-plus")).Click();
                driver.FindElement(By.CssSelector("span.ui-button-icon-primary.ui-icon.ui-icon-plus")).Click();
                driver.FindElement(By.CssSelector("button.ui-datepicker-close.ui-state-default.ui-priority-primary.ui-corner-all")).Click();
                driver.FindElement(By.XPath("//div[contains(@class, 'buttons')]/input")).Click();

                IWebElement prom_msg = driver.FindElement(By.XPath("//p"));
                Assert.IsTrue(prom_msg.Text.Contains("Promotion"));
                Assert.IsTrue(prom_msg.Text.Contains("successfully added"));
                Console.WriteLine("Added New Promotion Successfully");
                
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void d_check_promo_subtab()
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.FindElement(By.XPath("//select")).SendKeys("Ended Any Time");
                driver.FindElement(By.XPath("//input[contains(@class, 'search_button')]")).Click();

                // What if there are more than one record has the same label??
                driver.FindElement(By.XPath("//a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                driver.FindElement(By.XPath("//input[@id='input_promo_promo_dates_attributes_0_ends_at']")).Click();

                driver.SwitchTo().Window(driver.WindowHandles.Last());
                IWebElement cal = driver.FindElement(By.XPath("//div[@id='ui-datepicker-div']"));
                driver.SwitchTo().ActiveElement();
                driver.FindElement(By.CssSelector("span.ui-button-icon-primary.ui-icon.ui-icon-plus")).Click();
                driver.FindElement(By.CssSelector("span.ui-button-icon-primary.ui-icon.ui-icon-plus")).Click();
                driver.FindElement(By.CssSelector("button.ui-datepicker-close.ui-state-default.ui-priority-primary.ui-corner-all")).Click();
                driver.FindElement(By.XPath("//div[contains(@class, 'buttons')]/input")).Click();

                IWebElement prom_msg = driver.FindElement(By.XPath("//p"));
                Assert.IsTrue(prom_msg.Text.Contains("Promotion"));
                Assert.IsTrue(prom_msg.Text.Contains("successfully updated"));
                Console.WriteLine("Updated Added Promotion from Promotion Subtab Successfully");

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

     /*   [Test]
        public void WDeleteTemplate()
        {
            driver = new ChromeDriver();
            //driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084/input_promos?type=T");
            Task.Delay(30000).Wait();  // wait for 30 seconds for the user to enter username & password
            Console.WriteLine(label);
            Console.ReadLine();
            driver.FindElement(By.XPath("//input[@id='search']")).SendKeys(label);
           // driver.FindElement(By.XPath("//select")).SendKeys("Active");
            Task.Delay(1000).Wait();  // wait for 1 seconds for the user to enter username & password
            driver.FindElement(By.ClassName("search_button")).Click();
            //driver.FindElement(By.XPath("//input[contains(@class, 'search_button')]")).Click();
            Task.Delay(10000).Wait();
            Console.WriteLine("User please click 'OK'");
            Task.Delay(10000).Wait();  // wait for 10 seconds
            
        } */

    }
}