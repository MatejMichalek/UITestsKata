using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class SeleniumTestsExample
    {
        IWebDriver? driver;

        [SetUp]
        public void startBrowser()
        {
            // TODO: Place here path to you chrome driver
            driver = new ChromeDriver("C:\\Users\\matej\\Desktop");
        }

        [Test]
        public void OrderProcessTest()
        {
            driver.Url = "https://www.saucedemo.com";

            driver.FindElement(By.Name("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Name("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Name("login-button")).Submit();

            //Add first product to cart
            driver.FindElement(By.Name("add-to-cart-sauce-labs-backpack")).Click();
            Assert.AreEqual("1", driver.FindElement(By.ClassName("shopping_cart_badge")).Text);

            driver.FindElement(By.Name("add-to-cart-sauce-labs-bike-light")).Click();
            Assert.AreEqual("2", driver.FindElement(By.ClassName("shopping_cart_badge")).Text);

            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            var cart = driver.FindElement(By.ClassName("cart_list"));
            var cartItems = driver.FindElements(By.ClassName("cart_item"));
            Assert.AreEqual(2, cartItems.Count);

            var firstItem = cartItems[0];
            Assert.AreEqual("Sauce Labs Backpack", firstItem.FindElement(By.ClassName("inventory_item_name")).Text);
            Assert.AreEqual("1", firstItem.FindElement(By.ClassName("cart_quantity")).Text);
            
            var secondItem = cartItems[0];
            Assert.AreEqual("Sauce Labs Backpack", secondItem.FindElement(By.ClassName("inventory_item_name")).Text);
            Assert.AreEqual("1", secondItem.FindElement(By.ClassName("cart_quantity")).Text);
            
            driver.FindElement(By.Name("checkout")).Click();
            
            driver.FindElement(By.Name("firstName")).SendKeys("Matej");
            driver.FindElement(By.Name("lastName")).SendKeys("Michalek");
            driver.FindElement(By.Name("postalCode")).SendKeys("01701");
            driver.FindElement(By.Name("continue")).Submit();
            
            Assert.AreEqual("FREE PONY EXPRESS DELIVERY!", driver.FindElement(By.XPath("//*[@id='checkout_summary_container']/div/div[2]/div[4]")).Text);
            Assert.AreEqual("Total: $43.18", driver.FindElement(By.ClassName("summary_total_label")).Text);
            Assert.AreEqual("SauceCard #31337", driver.FindElement(By.XPath("//*[@id=\"checkout_summary_container\"]/div/div[2]/div[2]")).Text);
            driver.FindElement(By.Name("finish")).Click();


            Assert.AreEqual("CHECKOUT: COMPLETE!", driver.FindElement(By.XPath("//*[@id=\"header_container\"]/div[2]/span")).Text);
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }

}