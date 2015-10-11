using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAutomation.Library
{
    public class ContactUsTestTemplate
    {
        IWebDriver browserSpecificDriver;
        public ContactUsTestTemplate(IWebDriver browserSpecificDriver)
        {
            this.browserSpecificDriver = browserSpecificDriver;
        }

        public void Setup()
        {

        }

        public void NavidateToContactUs()
        {
            browserSpecificDriver.Navigate();
        }
        public void SubmitInvalidDetails(ContactUsViewModel toSend)
        {
            SubmitDetails(false, toSend);
        }

        public void SubmitValidDetails(ContactUsViewModel toSend)
        {
            SubmitDetails(true, toSend);
        }

        private void SubmitDetails(bool valid, ContactUsViewModel toSend)
        {
            IWebElement eleForm = browserSpecificDriver.FindElement(By.Id("aspnetForm"));
            if (eleForm == null || (eleForm != null && eleForm.TagName.ToLower() != "form"))
            {
                Assert.Fail(string.Format("Unable to locate Contact Us form: {0}", "aspnetForm"));
            }
            
            IWebElement eleName = eleForm.FindElement(By.Id("ctl00_MainContent_NameBox"));
            IWebElement eleMessage = eleForm.FindElement(By.Id("ctl00_MainContent_MessageBox"));
            IWebElement eleEmail = eleForm.FindElement(By.Id("ctl00_MainContent_EmailBox"));
            IWebElement eleSend = eleForm.FindElement(By.Id("ctl00_MainContent_SendButton"));
            if (eleName != null && eleEmail != null & eleMessage != null && eleSend != null)
            {
                eleName.SendKeys(toSend.Name);
                eleEmail.SendKeys(toSend.Email);
                eleMessage.SendKeys(toSend.Message);

                // Added wait for JS validation to kick in as, the page submits despite invalid email address.
                Thread.Sleep(500);

                IWebElement eleInvalidEmailSpan = eleForm.FindElement(By.Id("ctl00_MainContent_revEmailAddress"));
                string styleValue = eleInvalidEmailSpan.GetAttribute("style");
                if ( valid && styleValue.ToLower().Contains("visibility: visible"))
                {
                    Assert.Fail("Javascript Validation for invalid email failed.");
                }

                eleSend.Click();
            }
            else
            {
                Assert.Fail(string.Format("One or more elements missing from the form: {0}", "aspnetForm"));
            }
        }

        public void CleanUp()
        {
            browserSpecificDriver.Close();
        }
    }
}
