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
using OnSolveAutomationProject.Model;
using System.Xml;
using System.Globalization;
using System.Collections.Generic;

namespace OnSolveAutomationProject
{
    [TestClass]
    public class FormSiteTestcases
    {
        IWebDriver _driver;
        LoginForm _objLogin;
        SubmitForm _objHomePage;
        ThankyouFormSite _successSite;
        string _loginSiteUrl = "https://fs28.formsite.com/ecnvietnam/form1/index.html";
        string _apiDataUrl = "https://fs28.formsite.com/api/users/ecnvietnam/forms/form1/";
        
        private TestContext context;

        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        [TestInitialize]
        public void TestInitialized()
        {
            ChromeOptions chrOptions = new ChromeOptions();
            chrOptions.AddArguments("start-maximized");
            chrOptions.AddArguments("disable-infobars");
            _driver = new ChromeDriver("../../driver", chrOptions);

            //FirefoxOptions ffOptions = new FirefoxOptions();            
            //ffOptions.AddArguments("--start-maximized");
            //ffOptions.AddArguments("--disable-infobars");
            //_driver = new FirefoxDriver("../../driver", ffOptions);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
        }

        [TestMethod()]        
        public void SubmitData_Correct()
        {
            FormData fData = new FormData();
            fData.FirstName = "Hien Su";
            fData.LastName = "Su Duy";
            fData.StreetAddress = "qwe";
            fData.AddressLine2 = "zxc";
            fData.City = "asd";
            fData.State = "Nevada";
            fData.ZipCode = "3242";
            fData.PhoneNumber = "123456789456";
            fData.EmailAddress = "abc@bdc.com";
            fData.Date = "01/08/2018";

            try
            {
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime beforeSubmitDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, centralZone).AddMinutes(-1);

                _objLogin = new LoginForm(_driver, _loginSiteUrl);
                _objLogin.Login("secret");

                _objHomePage = new SubmitForm(_driver);
                _objHomePage.SubmitData(fData);

                _successSite = new ThankyouFormSite(_driver);
                bool resSite = _successSite.NavigateDone();
                if (resSite)
                    TestContext.WriteLine("'Thank you' Site was displayed after submit data.");
                else
                    Assert.Fail("'Thank you' Site was not displayed after submit data.");

                bool resVal = ValidateDataByAPIEndPoint(fData, beforeSubmitDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                if(resVal)
                    TestContext.WriteLine("The validating data by API EndPoint is successful.");
                else
                    Assert.Fail("The validating data by API EndPoint is unsuccessful.");
                _driver.Close();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _driver.Close();
            }
        }

        [TestMethod()]
        [DataSource("System.Data.OleDB", @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=TestData\FormSite.xlsx; Extended Properties='Excel 12.0;HDR=yes';", "SubmitData$", DataAccessMethod.Sequential)]
        public void SubmitDataByExcel_Correct()
        {
            FormData fData = new FormData();
            fData.FirstName = context.DataRow["FirstName"].ToString();
            fData.LastName = context.DataRow["LastName"].ToString();
            fData.StreetAddress = context.DataRow["StreetAddress"].ToString();
            fData.AddressLine2 = context.DataRow["AddressLine2"].ToString();
            fData.City = context.DataRow["City"].ToString();
            fData.State = context.DataRow["State"].ToString();
            fData.ZipCode = context.DataRow["ZipCode"].ToString();
            fData.PhoneNumber = context.DataRow["PhoneNumber"].ToString();
            fData.EmailAddress = context.DataRow["Email"].ToString();
            fData.Date = context.DataRow["Date"].ToString();

            try
            {
                TimeZoneInfo centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime beforeSubmitDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, centralZone).AddMinutes(-1);

                _objLogin = new LoginForm(_driver, _loginSiteUrl);
                _objLogin.Login("secret");
                
                _objHomePage = new SubmitForm(_driver);
                _objHomePage.SubmitData(fData);

                _successSite = new ThankyouFormSite(_driver);
                bool resSite = _successSite.NavigateDone();
                if (resSite)
                    TestContext.WriteLine("'Thank you' Site was displayed after submit data.");
                else
                    Assert.Fail("'Thank you' Site was not displayed after submit data.");

                bool resVal = ValidateDataByAPIEndPoint(fData, beforeSubmitDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                if (resVal)
                    TestContext.WriteLine("The validating data by API EndPoint is successful.");
                else
                    Assert.Fail("The validating data by API EndPoint is unsuccessful. Min datetime: " + beforeSubmitDateTime.ToString());
            }
            catch (Exception ex)
            {
                Assert.Fail("Error happened : " + ex.Message);
            }
            finally
            {
                _driver.Close();
            }
        }

        private bool ValidateDataByAPIEndPoint(FormData expectedFData, string dateTimeMin)
        {
            bool bRes = false;
            FormSiteAPI site = new FormSiteAPI(_apiDataUrl, "Qm8nO3h6auh7");
            List<FormData> fDatas = site.GetResultDataByMinDateTime(dateTimeMin);
            foreach (FormData data in fDatas)
            {
                if (data.Compare(expectedFData))
                {
                    bRes = true;
                    break;
                }
            }

            return bRes;
        }
    }
}
