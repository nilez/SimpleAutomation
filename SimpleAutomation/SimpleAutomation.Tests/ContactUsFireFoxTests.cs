using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using SimpleAutomation.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;

namespace SimpleAutomation.Tests
{
    [TestClass]    
    public class ContactUsFireFoxTests
    {
        string contactUsUrl;
        IWebDriver driver;
        ContactUsTestTemplate contactUsTestTemplate;
        [TestInitialize]
        public void Setup() {
            AppSettingsReader reader = new AppSettingsReader();
            contactUsUrl = (string)reader.GetValue("ContactUsUrl", typeof(string));
            driver = new FirefoxDriver();
            driver.Url = contactUsUrl;                        
            contactUsTestTemplate = new ContactUsTestTemplate(driver);            
        }

        [TestMethod]
        [TestCategory("FireFox - ContactUs")]
        public void OnLoading_ContactUs_ItShouldLoad()
        {
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
            contactUsTestTemplate.NavidateToContactUs();
        }

        [TestMethod]
        [TestCategory("FireFox - ContactUs")]
        public void OnSubmitting_ContactUs_ShouldSubmitWithCorrectValues()
        {
            ContactUsViewModel toSend = new ContactUsViewModel() { Name = "j.Bloggs 6", Email = "j.Bloggs@qaworks.com", Message = "please contact me I want to find out more" };
            contactUsTestTemplate.SubmitValidDetails(toSend);
        }

        [TestMethod]
        [TestCategory("FireFox - ContactUs")]
        public void OnSubmitting_ContactUs_ShouldFailForInvalidEmailValue() {
            ContactUsViewModel toSend = new ContactUsViewModel() { Name = "J Name", Email = "j.Bloggsqaworks.com", Message = "a message" };
            contactUsTestTemplate.SubmitInvalidDetails(toSend);
        }

        [TestCleanup]
        public void CleanUp() {
            contactUsTestTemplate.CleanUp();
        }        
    }
}
