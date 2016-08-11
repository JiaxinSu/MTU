using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace MTU
{
    [TestFixture]
    public class MTU
    {
        // class variables
        static IWebDriver driver; 
        string label = "Jiaxin-Testing";
        
        [SetUp]
        public void login()
        {
            // will be called every time the test starts
        }

        [TearDown]
        public void Cleanup()
        {
            // will be called every time the test ends
        }

        [Test]
        public void a_login()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084");
            Task.Delay(10000).Wait();  // wait for 10 seconds for the user to enter username & password
        }

        [Test]
        public void A1_addNewTemplate()
        {
            try
            {
                
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

                Task.Delay(1000).Wait(); // Wait 5 seconds
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
        public void A2_UpdateTemplate()
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084/input_promos?utf8=%E2%9C%93&search=&active=1&type=T&sort=country&dir=asc&period=A&commit=Search");
                Task.Delay(1000).Wait();  
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
        public void A3_addedNewProm() 
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084/input_promos?utf8=%E2%9C%93&search=&active=1&type=T&sort=country&dir=asc&period=A&commit=Search");
                Task.Delay(1000).Wait();  
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
        public void A4_check_promo_subtab()
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

        // -----------------------------------------------------------------------//

    //    IWebDriver testdriver;
        [Test]
        public void B1_setup()
        {
            try
            {
             //   testdriver = new ChromeDriver();
                  driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084/");
                // System.Threading.Thread.Sleep(10000);
                // go to masks & dnis tab because other tabs cannot be access from the home page via XPath 
                driver.FindElement(By.XPath("//div[contains(@class, 'links')]/a[1]")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }

        }

        [Test]
        public void B2_a1_MasksandDNIS_phone_number_masks()
        {
            try
            {

                //  --* Update & Save *--// 
                //click update for first field
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();

                // input text to update Mask
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='mask']")).Clear();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='mask']")).SendKeys("Automatation test for mask");

                //save changes 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

                // --* Clear Field *--// 
                // click clear
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();

                // window handler for chrome pop up -- accept alert 
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();

                // put the text back (so we can do it again)
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='mask']")).SendKeys("Automatation test for mask");
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
      
        }

        [Test]
        public void B3_a2_MasksandDNIS_DNIS_overrides()
        {
            try
            {
                //-- * update & save * -- //
                // go to tab
                driver.FindElement(By.XPath("//div[@id='sub-header']/a")).Click();

                //Update
                driver.FindElement(By.XPath("//tr[contains(@class, 'odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();

                //select UTA
                SelectElement list = new SelectElement(driver.FindElement(By.XPath("//select[@id='dnis_override_channel']")));
                list.SelectByText("UTA");

                // save
                driver.FindElement(By.XPath("//div[contains(@class, 'buttons')]/input")).Click();

                // check save
                string s = driver.FindElement(By.XPath("/html/body/div[@class='flash notice']")).Text;
                Assert.IsTrue(s.Contains("successfully updated"));

                // -- * New * --// 
                driver.FindElement(By.XPath("//a[contains(@class, 'new button')]/div[contains(@class, 'caption1')]")).Click();
                // select 
                SelectElement list2 = new SelectElement(driver.FindElement(By.XPath("//select[@id='dnis_override_channel']")));
                list2.SelectByText("Boss Singapore");

                // put in DNIS info 
                driver.FindElement(By.XPath("//input[@id='dnis_override_dnis']")).SendKeys("Automation Test");
                driver.FindElement(By.XPath("//div[contains(@class, 'buttons')]/input")).Click();
                //check save
                string s2 = driver.FindElement(By.XPath("/html/body/div[@class='flash notice']")).Text;
                Assert.IsTrue(s2.Contains("successfully added"));


                // -- * Delete * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'delete button')]/div[contains(@class, 'caption1')]")).Click();
                // window handler for chrome pop up -- accept alert 
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                //check delete 
                string s3 = driver.FindElement(By.XPath("/html/body/div[@class='flash notice']")).Text;
                Assert.IsTrue(s3.Contains("successfully deleted"));
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }

        }

        [Test]
        public void B4_b_MTU()
        {
            try
            {
                // go to MTU tab
                driver.FindElement(By.XPath("//div[contains(@class, 'links')]/a[1]")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void B5_b1_MTUTranslations_a_Countries()
        {
            try
            {
                // automatically on countries tab 
                //1 -- * update * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).Clear();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).SendKeys("Auto Test MTU Countries");
                Console.WriteLine("update");

                //3 --* check save *--// 
                Console.WriteLine("countries check save");
                String s = driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).ToString();
                Console.Write("s is: ", s);



                //2 --* save * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("save");

                System.Threading.Thread.Sleep(2000);

                //4 -- * clear * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();

                // 5 -- *confirm *--/ 
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();

                //6 --* check clear *--// 
                String s5 = driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).ToString();
                Console.WriteLine("s5 is: ", s5);


                //7 --* fill in for when field is originally blank *--/ 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).SendKeys("Auto Test MTU Countries");
                //8 --* save* --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

                /*
                //9 -- * check  * --/ 
                String s6 = testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Text;
                Assert.IsTrue(s6.Contains("Automation Test"));
                */
            } 
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void B6_b2_MTUTranslations_b_Carriers()
        {
            try
            {
                // go to carriers tab
                driver.FindElement(By.XPath("//div[@id='sub-header']/a[1]")).Click();
                Console.WriteLine("clicked on the carriers tab");

                //1 -- * update * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).Clear();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).SendKeys("Auto Test MTU Carriers");
                Console.WriteLine("update");

                //2 --* save * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("save");

                System.Threading.Thread.Sleep(3000);

                //4 -- * clear * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("clear");
                // 5 -- *confirm *--/ 
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Console.WriteLine("alert");

                //7 --* fill in for when field is originally blank *--/ 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).SendKeys("Auto Test MTU Carriers");
                Console.WriteLine("fill in");
                //8 --* save* --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("find element");

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void B7_b3_MTUTranslations_c_Products()
        {
            try
            {

                // go to products tab 
                driver.FindElement(By.XPath("//div[@id='sub-header']/a[2]")).Click();

                //1 -- * update * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).Clear();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).SendKeys("Auto Test MTU Products");

                //2 --* save * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

                System.Threading.Thread.Sleep(4000);

                //4 -- * clear * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();

                // 5 -- *confirm *--/ 
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();

                //7 --* fill in for when field is originally blank *--/ 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).SendKeys("Auto Test MTU Products");
                //8 --* save* --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void B8_b4_MTUTranslations_d_DenomSuffix()
        {
            try
            {
                // go to denom tab
                driver.FindElement(By.XPath("//div[@id='sub-header']/a[3]")).Click();

                //1 -- * update * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).Clear();
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).SendKeys("Auto Test MTU Denom");
                Console.WriteLine("update");


                //2 --* save * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("save");

                System.Threading.Thread.Sleep(4000);

                //4 -- * clear * --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("clear");

                // 5 -- *confirm *--/ 
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Console.WriteLine("confirm");

                //7 --* fill in for when field is originally blank *--/ 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).SendKeys("Auto Test MTU Denom");
                //8 --* save* --// 
                driver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void c_IVR_Files()
        {
            // not currently working // 
            // -- * new * -- // 
            // -- update & save --// 
            // -- delete --//

        } 

    } 
}