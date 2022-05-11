using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutomationAssignment.Pages
{
    public class HomePage:BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {

        }
        //public By ItemCategory = By.XPath("//p[contains(text(),'wall decor')]//parent::a");
        public By txtSearch = By.Id("global-enhancements-search-query");
        public By lstItems = By.XPath("//ul[@class = 'wt-grid wt-grid--block wt-pl-xs-0 tab-reorder-container']");
        public By btnBasket = By.XPath("//nav/ul/li[4]");
        public void ClickProduct(string product)
        {
            try
            {
                var element = GetElement(lstItems).FindElements(By.TagName("li"));
                var text = element.First(x => x.Text.ToUpper().StartsWith(product));
                text.Click();
                _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            }
            catch (Exception ex)
            {

            }
        }
        public void SearchProduct(string productName)
        {
            try
            {
                WriteTextToTextBox(txtSearch, "wall decor");
                SendKeyTextBox(txtSearch, Keys.Enter);
            }
            catch (Exception ex)
            {

            }
        }
        public void OpenBasket()
        {
            //var btnBasket = _driver.FindElements(By.XPath("//nav/ul/li"))[3];
            ClickButton(btnBasket);
            WaitForReady();
            Thread.Sleep(3000);
        }
    }
}
