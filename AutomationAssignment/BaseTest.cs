using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebDriverManager.DriverConfigs.Impl;

namespace AutomationAssignment
{
    
    public class BaseTest
    {
        public static IWebDriver _driver;
        public ExtentReports extent;
        public ExtentHtmlReporter htmlReporter;
        public ExtentTest test = null;
        [OneTimeSetUp]
        public void ExtentStart()
        {
            try
            {

                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;                
                htmlReporter = new ExtentHtmlReporter(Directory.GetCurrentDirectory() + "\\ExtentReport.html");
                htmlReporter.Config.Theme = Theme.Standard;
                htmlReporter.Config.DocumentTitle = "Automation Assessment";
                htmlReporter.Config.ReportName = "Automation Assessment";
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
                test = extent.CreateTest("Setup Driver and Extent report");
                ChromeOptions option = new ChromeOptions();
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                option.PlatformName = "windows";
                option.AddUserProfilePreference("download.default_directory", @"c:\Downloads");                
                _driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), option, TimeSpan.FromMinutes(4));
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(4);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                _driver.Manage().Window.Maximize();
                _driver.Url = "https://www.etsy.com/";
            }
            catch (Exception ex)
            {
                test.Log(Status.Error, "Oops Somthing Went Wrong Unable to setup");
            }
        }
        [OneTimeTearDown]
        public void flush()
        {
            _driver.Quit();
            extent.Flush();            
        }
    }
}
