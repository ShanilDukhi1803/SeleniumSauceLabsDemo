using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ConsoleApp2
{
    public class GitHubTest
    {
        [Test]
        //static void Main  //(string[] args)
        public void CheckGitHubSearch()
        {
            {
                new DriverManager().SetUpDriver(new ChromeConfig());

                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl("https://github.com");
                string searchPhrase = "selenium";

                IWebElement searchBox = driver.FindElement(By.CssSelector(".search-input"));
                searchBox.Click();

                IWebElement searchInput = driver.FindElement(By.CssSelector("#query-builder-test"));

                searchInput.SendKeys("Selenium");
                searchInput.SendKeys(Keys.Enter);

                IList<string> actualItems = driver.FindElements(By.CssSelector("[data-testid='results-list'] > div")).Select(item => item.Text.ToLower()).ToList();

                IList<string> expectedItems = actualItems.Where(item => item.Contains(searchPhrase)).ToList();

                Assert.That(expectedItems, Is.EqualTo(actualItems));

                driver.Quit();
            }

        }
    }
}
