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

        public void NavidateToContactUs() {
            browserSpecificDriver.Navigate();
        }
        public void SubmitDetails(ContactUsViewModel toSend) {

            #region For a form with lot of elements we can dynamically chcek the elements
            //List<string> names = new List<string>(new string[] { "ctl00_MainContent_NameBox", "ctl00_MainContent_EmailBox", "ctl00_MainContent_MessageBox", "ContactForm" });

            //var elements = names.Select(n => driver.FindElement(By.Id(n)));

            //if (elements.All(e => e != null))
            //{

            //}                
            //else {
            //    var missingElements = names.Except(elements.Where(x => x != null).Select(x => x.TagName)).Aggregate((i, j) => i + ", " + j);
            //    throw new NoSuchElementException(string.Format("One or more elements of the contact form are missing : {0}", missingElements));
            //}
            #endregion

            IWebElement eleForm = browserSpecificDriver.FindElement(By.Id("ContactForm"));
            if (eleForm == null)
            {
                Assert.Fail(string.Format("Unable to locate Contact Us form: {0}", "ContactForm"));
            }
            IWebElement eleName = browserSpecificDriver.FindElement(By.Id("ctl00_MainContent_NameBox"));
            IWebElement eleMessage = browserSpecificDriver.FindElement(By.Id("ctl00_MainContent_MessageBox"));
            IWebElement eleEmail = browserSpecificDriver.FindElement(By.Id("ctl00_MainContent_EmailBox"));
            IWebElement eleSend = browserSpecificDriver.FindElement(By.Id("ctl00_MainContent_SendButton"));
            if (eleName != null && eleEmail != null & eleMessage != null && eleSend != null)
            {
                eleName.SendKeys(toSend.Name);
                eleEmail.SendKeys(toSend.Email);
                eleMessage.SendKeys(toSend.Message);

                // Added wait for JS validation to kick in as, the page submits despite invalid email address.
                Thread.Sleep(500);

                IWebElement eleInvalidEmailSpan = browserSpecificDriver.FindElement(By.Id("ctl00_MainContent_revEmailAddress"));
                string styleValue = eleInvalidEmailSpan.GetAttribute("style");
                if (!styleValue.ToLower().Contains("visibility: visible"))
                {
                    Assert.Fail("Javascript Validation for invalid email failed.");
                }

                eleSend.Click();                
            }
            else
            {
                Assert.Fail(string.Format("One or more elements missing from the form: {0}", "ContactForm"));
            }
        }

        public void CleanUp() { }
    }
}
