using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSolveAutomationProject.Utilities
{
    public class WebActions
    {
        //s == "iexplore" || s == "iexplorer" || s == "chrome" || s == "firefox"
        public static void CloseBrowser(string browserName)
        {
            foreach (var process in Process.GetProcessesByName(browserName))
            {
                process.Kill();
            }
        }

        public static bool WaitUntilElementVisible(IWebDriver driver, By elementLocator, out IWebElement element,int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                element = wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
                return true;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                element = null;
                return false;
            }
        }
        public static IWebElement WaitUntilElementClickable(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }
    }
}
