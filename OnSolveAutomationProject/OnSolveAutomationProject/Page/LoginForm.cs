﻿using OnSolveAutomationProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSolveAutomationProject.Page
{
    public class LoginForm
    {
        IWebDriver _driver;
        string _loginUrl = "https://fs28.formsite.com/ecnvietnam/form1/index.html";
        By _labelTitle = By.CssSelector("div.segment_header");
        By _txtPassword = By.Id("Password");
        By _btnSubmit = By.Name("Submit");

        public LoginForm(IWebDriver driver)
        {
            this._driver = driver;
            _driver.Url = _loginUrl;            
        }

        public void SetPassword(string password)
        {                        
            _driver.FindElement(_txtPassword).SendKeys(password); 
        }

        public void ClickSubmitButton()
        {
            _driver.FindElement(_btnSubmit).Click();
        }

        public bool NavigateDone()
        {
            IWebElement lblObj;
            if (WebActions.WaitUntilElementVisible(_driver, _labelTitle, out lblObj, 20))
            {
                return true;
            }
            return false;
        }

        public void Login(String strPasword)
        {
            NavigateDone();

            this.SetPassword(strPasword);

            //Click Login button
            ClickSubmitButton();
        }
    }
}
