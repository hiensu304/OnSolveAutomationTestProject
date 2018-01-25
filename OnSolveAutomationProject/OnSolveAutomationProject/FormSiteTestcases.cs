using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OnSolveAutomationProject.Page;
using OpenQA.Selenium.Firefox;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace OnSolveAutomationProject
{
    [TestClass]
    public class FormSiteTestcases
    {
        IWebDriver _driver;
        LoginForm _objLogin;
        SubmitForm _objHomePage;
        string _loginSiteUrl = "https://fs28.formsite.com/ecnvietnam/form1/index.html";
        string _apiDataResultUrl = "https://fs28.formsite.com/api/users/ecnvietnam/forms/form1/results?fs_api_key=Qm8nO3h6auh7";
        
        private TestContext context;

        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        [TestInitialize]
        public void TestInitialized()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("disable-infobars");
            options.AddArgument("--start-maximized");
            
            _driver = new ChromeDriver(options);
            //_driver = new FirefoxDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        [TestMethod()]
        [DataSource("System.Data.OleDB", @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=TestData\FormSite.xlsx; Extended Properties='Excel 12.0;HDR=yes';", "SubmitData$", DataAccessMethod.Sequential)]
        public void SubmitData_Correct()
        {            
            string password = context.DataRow["Password"].ToString();
            string firstName = context.DataRow["FirstName"].ToString();
            string lastName = context.DataRow["LastName"].ToString();
            string streetAddress = context.DataRow["StreetAddress"].ToString();
            string addressLine2 = context.DataRow["AddressLine2"].ToString();
            string city = context.DataRow["City"].ToString();
            string state = context.DataRow["State"].ToString();
            string zipCode = context.DataRow["ZipCode"].ToString();
            string phoneNumber = context.DataRow["PhoneNumber"].ToString();
            string emailAddress = context.DataRow["Email"].ToString();
            string date = context.DataRow["Date"].ToString();

            _objLogin = new LoginForm(_driver, _loginSiteUrl);
            _objLogin.Login("secret");
            
            _objHomePage = new SubmitForm(_driver);
            _objHomePage.SubmitData(firstName, lastName, streetAddress, addressLine2, city, state, zipCode, phoneNumber, emailAddress, date);
            
            ValidateDataByAPIEndPoint();
            _driver.Close();
        }

        private bool ValidateDataByAPIEndPoint()
        {
            bool bRes = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_apiDataResultUrl);
            request.Method = "GET";            
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string content = string.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }

            
            

            return bRes;
        }
    }
}
