using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutomationAssignment.Pages
{
    public class BasePage
    {
        public readonly IWebDriver _driver;
        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void WriteTextToTextBox(By by, string txt)
        {
            if (_driver.FindElement(by).Displayed)
            {
                _driver.FindElement(by).Clear();
                _driver.FindElement(by).SendKeys(txt);
                WaitForReady();
            }
            else
            {
                throw new Exception("Text Box with Locator: " + by + "not found");
            }
        }
        public void ClickButton(By by)
        {
            if (_driver.FindElement(by).Enabled)
            {
                _driver.FindElement(by).Click();
                //Thread.Sleep(4000);

            }
            else
            {
                throw new Exception("btn with Locator: " + by + "not found");
            }
        }
        public void SendKeyTextBox(By by, string key)
        {
            if (_driver.FindElement(by).Displayed)
            {
                _driver.FindElement(by).SendKeys(key);
                WaitForReady();
            }
            else
            {
                throw new Exception("Text Box with Locator: " + by + "not found");
            }
        }
        public IWebElement GetElement(By by)
        {
            return _driver.FindElement(by);
        }
        public void SelectDropDownValue(string id, string Value)
        {
            _driver.FindElement(By.XPath("//select[@id='" + id + "']/option[contains(.,'" + Value + "')]")).Click();
        }
        public bool IsElementPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void WaitForReady(int timeout = 120)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeout));
            wait.Until(driver =>
            {
                bool isAjaxFinished = (bool)((IJavaScriptExecutor)driver).
                    ExecuteScript("return jQuery.active == 0");
                //bool isLoaderHidden = (bool)((IJavaScriptExecutor)driver).
                //    ExecuteScript("return $('.divloader').is(':visible') == false");
                //return isAjaxFinished & isLoaderHidden;
                return isAjaxFinished;
            });
        }
    }
}
