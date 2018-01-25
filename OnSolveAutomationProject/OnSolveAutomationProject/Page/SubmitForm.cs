using OnSolveAutomationProject.Model;
using OnSolveAutomationProject.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSolveAutomationProject.Page
{
    public class SubmitForm
    {
        IWebDriver _driver;
        
        By _txtFirstName = By.Id("RESULT_TextField-1");
        By _txtLastName = By.Id("RESULT_TextField-2");
        By _txtStreetAddress = By.Id("RESULT_TextField-3");
        By _txtAddressLine2 = By.Id("RESULT_TextField-4");
        By _txtCity = By.Id("RESULT_TextField-5");        
        By _cmbState = By.Id("RESULT_RadioButton-6");
        By _txtZipCode = By.Id("RESULT_TextField-7");
        By _txtPhoneNumber = By.Id("RESULT_TextField-8");        
        By _txtEmailAddress = By.Id("RESULT_TextField-9");
        By _txtDate = By.Id("RESULT_TextField-10");
        By _btnSubmit = By.Id("FSsubmit");

        public SubmitForm(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void SetFirstName(string firstName)
        {
            _driver.FindElement(_txtFirstName).SendKeys(firstName);
        }

        public void SetLastName(string lastName)
        {
            _driver.FindElement(_txtLastName).SendKeys(lastName);
        }

        public void SetStreetAddress(string streetAddress)
        {
            _driver.FindElement(_txtStreetAddress).SendKeys(streetAddress);
        }

        public void SetAddressLine2(string addressLine2)
        {
            _driver.FindElement(_txtAddressLine2).SendKeys(addressLine2);
        }

        public void SetCity(string city)
        {
            _driver.FindElement(_txtCity).SendKeys(city);
        }

        public void SetState(string state)
        {
            var s = _driver.FindElement(_cmbState);
            //create select element object 
            var selectElement = new SelectElement(s);
            //select by value
            selectElement.SelectByText(state);            
        }

        public void SetZipCode(string zipCode)
        {
            _driver.FindElement(_txtZipCode).SendKeys(zipCode);
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            _driver.FindElement(_txtPhoneNumber).SendKeys(phoneNumber);
        }

        public void SetEmailAddress(string emailAddress)
        {
            _driver.FindElement(_txtEmailAddress).SendKeys(emailAddress);
        }

        public void SetDate(string date)
        {
            _driver.FindElement(_txtDate).SendKeys(date);
        }

        public void ClickSubmitButton()
        {
            _driver.FindElement(_btnSubmit).Click();
        }


        public bool NavigateDone()
        {
            IWebElement lblObj;
            if (WebActions.WaitUntilElementVisible(_driver, _btnSubmit, out lblObj))
            {
                return true;
            }
            return false;
        }

        public void SubmitData(FormData formData)
        {
            NavigateDone();
            SetFirstName(formData.FirstName);
            SetLastName(formData.LastName);
            SetStreetAddress(formData.StreetAddress);
            SetAddressLine2(formData.AddressLine2);
            SetCity(formData.City);
            SetState(formData.State);
            SetZipCode(formData.ZipCode);
            SetPhoneNumber(formData.PhoneNumber);
            SetEmailAddress(formData.EmailAddress);
            SetDate(formData.Date);
            ClickSubmitButton();
        }
    }
}
