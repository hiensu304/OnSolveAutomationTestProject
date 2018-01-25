using OnSolveAutomationProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSolveAutomationProject.Page
{
    public class ThankyouFormSite
    {
        IWebDriver _driver;

        By _lblSuccess = By.CssSelector("h1.success-title");

        public ThankyouFormSite(IWebDriver driver)
        {
            this._driver = driver;
        }
        public bool NavigateDone()
        {
            IWebElement lblObj;
            if (WebActions.WaitUntilElementVisible(_driver, _lblSuccess, out lblObj))
            {
                return true;
            }
            return false;
        }

    }
}
