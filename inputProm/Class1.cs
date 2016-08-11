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
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [Test]
        public void A1_addNewTemplate()
        {
            try
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084");
                Task.Delay(60000).Wait();  // wait for 10 seconds for the user to enter username & password
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
                Console.WriteLine(added_msg);
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
                Assert.IsTrue(updated_msg.Text.Contains("successfully updated"));
                Console.WriteLine("Updated New Template Successfully");

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.ReadLine();
            }
        }

        [Test]
        public void WDeleteTemplate()
        {
            driver = new ChromeDriver();
            //driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084/input_promos?type=T");
            Task.Delay(30000).Wait();  // wait for 10 seconds for the user to enter username & password
            Console.WriteLine(label);
            Console.ReadLine();
            driver.FindElement(By.XPath("//input[@id='search']")).SendKeys(label);
            // driver.FindElement(By.XPath("//select")).SendKeys("Active");
            driver.FindElement(By.ClassName("search_button")).Click();
            //driver.FindElement(By.XPath("//input[contains(@class, 'search_button')]")).Click();
            Task.Delay(10000).Wait();
            Console.WriteLine("User please click 'OK'");
            Task.Delay(10000).Wait();  // wait for 10 seconds

        }

        // -----------------------------------------------------------------------//

        IWebDriver testdriver;
        [Test]
        public void B1_a_login()
        {
            try
            {
                testdriver = new ChromeDriver();
                testdriver.Navigate().GoToUrl("http://lbossqa.corp.idt.net:9084/");
                System.Threading.Thread.Sleep(10000);
                // go to masks & dnis tab because other tabs cannot be access from the home page via XPath 
                testdriver.FindElement(By.XPath("//div[contains(@class, 'links')]/a[1]")).Click();
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
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();

                // input text to update Mask
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='mask']")).Clear();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='mask']")).SendKeys("Automatation test for mask");

                //save changes 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

                // --* Clear Field *--// 
                // click clear
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();

                // window handler for chrome pop up -- accept alert 
                IAlert alert = testdriver.SwitchTo().Alert();
                alert.Accept();

                // put the text back (so we can do it again)
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='mask']")).SendKeys("Automatation test for mask");
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
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
                testdriver.FindElement(By.XPath("//div[@id='sub-header']/a")).Click();

                //Update
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();

                //select UTA
                SelectElement list = new SelectElement(testdriver.FindElement(By.XPath("//select[@id='dnis_override_channel']")));
                list.SelectByText("UTA");

                // save
                testdriver.FindElement(By.XPath("//div[contains(@class, 'buttons')]/input")).Click();

                // check save
                string s = testdriver.FindElement(By.XPath("/html/body/div[@class='flash notice']")).Text;
                Assert.IsTrue(s.Contains("successfully updated"));

                // -- * New * --// 
                testdriver.FindElement(By.XPath("//a[contains(@class, 'new button')]/div[contains(@class, 'caption1')]")).Click();
                // select 
                SelectElement list2 = new SelectElement(testdriver.FindElement(By.XPath("//select[@id='dnis_override_channel']")));
                list2.SelectByText("Boss Singapore");

                // put in DNIS info 
                testdriver.FindElement(By.XPath("//input[@id='dnis_override_dnis']")).SendKeys("Automation Test");
                testdriver.FindElement(By.XPath("//div[contains(@class, 'buttons')]/input")).Click();
                //check save
                string s2 = testdriver.FindElement(By.XPath("/html/body/div[@class='flash notice']")).Text;
                Assert.IsTrue(s2.Contains("successfully added"));


                // -- * Delete * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'delete button')]/div[contains(@class, 'caption1')]")).Click();
                // window handler for chrome pop up -- accept alert 
                IAlert alert = testdriver.SwitchTo().Alert();
                alert.Accept();
                //check delete 
                string s3 = testdriver.FindElement(By.XPath("/html/body/div[@class='flash notice']")).Text;
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
                testdriver.FindElement(By.XPath("//div[contains(@class, 'links')]/a[1]")).Click();
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
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).Clear();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).SendKeys("Auto Test MTU Countries");
                Console.WriteLine("update");

                //3 --* check save *--// 
                Console.WriteLine("countries check save");
                String s = testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).ToString();
                Console.Write("s is: ", s);



                //2 --* save * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("save");

                System.Threading.Thread.Sleep(2000);

                //4 -- * clear * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();

                // 5 -- *confirm *--/ 
                IAlert alert = testdriver.SwitchTo().Alert();
                alert.Accept();

                //6 --* check clear *--// 
                String s5 = testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).ToString();
                Console.WriteLine("s5 is: ", s5);


                //7 --* fill in for when field is originally blank *--/ 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).SendKeys("Auto Test MTU Countries");
                //8 --* save* --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

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
                testdriver.FindElement(By.XPath("//div[@id='sub-header']/a[1]")).Click();
                Console.WriteLine("clicked on the carriers tab");

                //1 -- * update * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).Clear();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).SendKeys("Auto Test MTU Carriers");
                Console.WriteLine("update");

                //2 --* save * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("save");

                System.Threading.Thread.Sleep(3000);

                //4 -- * clear * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("clear");
                // 5 -- *confirm *--/ 
                IAlert alert = testdriver.SwitchTo().Alert();
                alert.Accept();
                Console.WriteLine("alert");

                //7 --* fill in for when field is originally blank *--/ 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][2]/input[@id='suffix']")).SendKeys("Auto Test MTU Carriers");
                Console.WriteLine("fill in");
                //8 --* save* --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
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
                testdriver.FindElement(By.XPath("//div[@id='sub-header']/a[2]")).Click();

                //1 -- * update * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).Clear();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).SendKeys("Auto Test MTU Products");

                //2 --* save * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

                System.Threading.Thread.Sleep(4000);

                //4 -- * clear * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();

                // 5 -- *confirm *--/ 
                IAlert alert = testdriver.SwitchTo().Alert();
                alert.Accept();

                //7 --* fill in for when field is originally blank *--/ 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).SendKeys("Auto Test MTU Products");
                //8 --* save* --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

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
                testdriver.FindElement(By.XPath("//div[@id='sub-header']/a[3]")).Click();

                //1 -- * update * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'edit button')]/div[contains(@class, 'caption1')]")).Click();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).Clear();
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).SendKeys("Auto Test MTU Denom");
                Console.WriteLine("update");


                //2 --* save * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("save");

                System.Threading.Thread.Sleep(4000);

                //4 -- * clear * --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'clear button')]/div[contains(@class, 'caption1')]")).Click();
                Console.WriteLine("clear");

                // 5 -- *confirm *--/ 
                IAlert alert = testdriver.SwitchTo().Alert();
                alert.Accept();
                Console.WriteLine("confirm");

                //7 --* fill in for when field is originally blank *--/ 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'l')][5]/input[@id='suffix']")).SendKeys("Auto Test MTU Denom");
                //8 --* save* --// 
                testdriver.FindElement(By.XPath("//tr[contains(@class, 'data odd')]/td[contains(@class, 'r actions')]/a[contains(@class, 'save button')]/div[contains(@class, 'caption1')]")).Click();

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