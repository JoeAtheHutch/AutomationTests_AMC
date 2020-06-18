// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace AutomationTests_AMC
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestChrome()
        {
            TestValues values = new TestValues();

            IWebDriver driver = new ChromeDriver
            {
                Url = values.siteURL
            };
            driver.Manage().Window.Maximize();
            //Open News links and access all News
            
            IWebElement newsLink = driver.FindElement(By.XPath(values.newsXPath));
            newsLink.Click();
            IWebElement allNewsLink = driver.FindElement(By.XPath(values.allNewsXPath));
            allNewsLink.Click();

            //get the individual articles into a List then Assert that it contains "James Mac Pherson"
            ReadOnlyCollection<IWebElement> newsList = driver.FindElements(By.XPath(values.newsListXPath));
            bool hasMatch = false;
            foreach (IWebElement article in newsList)
            {
                if (article.Text.Contains(values.newsMatchText))
                {
                    hasMatch = true;
                }
            }
            Assert.IsTrue(hasMatch);
            

            //Switch to the search for Loan Officers
            IWebElement findLoanOfficerButton = driver.FindElement(By.XPath(values.lOButtonXPath));
            findLoanOfficerButton.Click();
            IWebElement zipCodeField = driver.FindElement(By.XPath(values.zipCodeFieldXPath));
            var selectRadius = new SelectElement(driver.FindElement(By.XPath(values.searchRadiusXPath)));
            IWebElement searchButton = driver.FindElement(By.XPath(values.searchButtonXPath));

            //Enter in Zip Code and search radius
            zipCodeField.SendKeys(values.zipCode);
            selectRadius.SelectByValue(values.searchRadius);
            searchButton.Click();

            //Grab the list of agents
            ReadOnlyCollection<IWebElement> agentList = driver.FindElements(By.XPath(values.agentListXPath));

            //I'm sure there's a better implementation than this. This is just the easiest method to debug I could think of.
            //Set a false boolean. Look at the list of agents If one is Matt Keyes with the correct NMLS number, set boolean to true, break loop and Assert.
            hasMatch = false;
            foreach (IWebElement agent in agentList)
            {
                if (agent.Text.Contains(values.agentName) && agent.Text.Contains(values.NMLSNum))
                {
                    hasMatch = true;
                    break;
                }
            }
            Assert.IsTrue(hasMatch);

            driver.Quit();
        }

        [Test]
        public void TestFireFox()
        {
            TestValues values = new TestValues();

            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(values.siteURL);
            driver.Manage().Window.Maximize();
            //Open News links and access all News
            
            IWebElement newsLink = driver.FindElement(By.XPath(values.newsXPath));
            newsLink.Click();
            IWebElement allNewsLink = driver.FindElement(By.XPath(values.allNewsXPath));
            allNewsLink.Click();

            //get the individual articles into a List then Assert that it contains "James Mac Pherson"
            ReadOnlyCollection<IWebElement> newsList = driver.FindElements(By.XPath(values.newsListXPath));
            bool hasMatch = false;
            foreach (IWebElement article in newsList)
            {
                if (article.Text.Contains(values.newsMatchText))
                {
                    hasMatch = true;
                }
            }
            Assert.IsTrue(hasMatch);


            //Switch to the search for Loan Officers
            IWebElement findLoanOfficerButton = driver.FindElement(By.XPath(values.lOButtonXPath));
            findLoanOfficerButton.Click();
            IWebElement zipCodeField = driver.FindElement(By.XPath(values.zipCodeFieldXPath));
            var selectRadius = new SelectElement(driver.FindElement(By.XPath(values.searchRadiusXPath)));
            IWebElement searchButton = driver.FindElement(By.XPath(values.searchButtonXPath));

            //Enter in Zip Code and search radius
            zipCodeField.SendKeys(values.zipCode);
            selectRadius.SelectByValue(values.searchRadius);
            searchButton.Click();

            //Grab the list of agents
            ReadOnlyCollection<IWebElement> agentList = driver.FindElements(By.XPath(values.agentListXPath));

            //I'm sure there's a better implementation than this. This is just the easiest method to debug I could think of.
            //Set a false boolean. Look at the list of agents If one is Matt Keyes with the correct NMLS number, set boolean to true, break loop and Assert.
            hasMatch = false;
            Debug.WriteLine("Made it to just before the loop");
            foreach (IWebElement agent in agentList)
            {
                if (agent.Text.Contains(values.agentName) && agent.Text.Contains(values.NMLSNum))
                {
                    hasMatch = true;
                    break;
                }
            }
            Assert.IsTrue(hasMatch);

            driver.Quit();
        }
    }

    public class TestValues
    {
        public string siteURL = "https://academymortgage.com";
        public string newsXPath = "//*[@href='/news']";
        public string allNewsXPath = ".//*[@href='/news/all-news']";
        public string newsListXPath = ".//*[@id='Main_C005_Col00']//h3";
        public string newsMatchText = "James Mac Pherson";
        public string lOButtonXPath = "//*[@href='/find-a-loan-officer']/span";
        public string zipCodeFieldXPath = "//*[@id='Main_C007_Col00']/div[1]/div/div[1]/div[3]/form/input";
        public string searchRadiusXPath = "//*[@id='Main_C007_Col00']/div[1]/div/div[1]/div[3]/form/select";
        public string searchButtonXPath = "//*[@id='Main_C007_Col00']/div[1]/div/div[1]/div[3]/form/button";
        public string zipCode = "84005";
        public string searchRadius = "25";
        public string agentListXPath = "//*[@class='academy-custom-list']/li";
        public string agentName = "Matt Keyes";
        public string NMLSNum = "NMLS #398768";
    }
}
