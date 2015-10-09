using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using SimpleAutomation.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAutomation.Tests
{
    [TestClass]    
    public class ContactUsChromeTests
    {
        string contactUsUrl;
        IWebDriver driver;
        ContactUsTestTemplate contactUsTestTemplate;
        [TestInitialize]
        public void Setup() {
            AppSettingsReader reader = new AppSettingsReader();
            contactUsUrl = (string)reader.GetValue("ContactUsUrl", typeof(string));
            driver = new ChromeDriver();
            driver.Url = contactUsUrl;                        
            contactUsTestTemplate = new ContactUsTestTemplate(driver);            
        }

        [TestMethod]
        [TestCategory("Chrome - ContactUs")]
        public void OnLoading_ContactUs_ItShouldLoad()
        {
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
            contactUsTestTemplate.NavidateToContactUs();
        }

        [TestMethod]
        [TestCategory("Chrome - ContactUs")]
        public void OnSubmitting_ContactUs_ShouldSubmitWithCorrectValues()
        {
            ContactUsViewModel toSend = new ContactUsViewModel() { Name = "j.Bloggs 6", Email = "j.Bloggs@qaworks.com", Message = "please contact me I want to find out more" };
            contactUsTestTemplate.SubmitDetails(toSend);
        }

        [TestMethod]
        [TestCategory("Chrome - ContactUs")]
        public void OnSubmitting_ContactUs_ShouldFaileForInvalidEmailValue() {
            ContactUsViewModel toSend = new ContactUsViewModel() { Name = "J Name", Email = "j.Bloggsqaworks.com", Message = "a message" };
            contactUsTestTemplate.SubmitDetails(toSend);
        }

        [TestCleanup]
        public void CleanUp()
        {
            contactUsTestTemplate.CleanUp();
        }
    }
}
