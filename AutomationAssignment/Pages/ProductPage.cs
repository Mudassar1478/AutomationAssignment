using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutomationAssignment.Pages
{
    public class ProductPage:BasePage
    {
        public ProductPage(IWebDriver driver) : base(driver)
        {

        }
        public By ddPrimaryColor = By.Id("variation-selector-0");
        public By ddDimension = By.Id("variation-selector-1");
        public By btnAddToBasket = By.XPath("//button[contains(text(),'Add to basket')]");
        
        public void SelectPrimaryColor(string color)
        {
            try
            {

                SelectDropDownValue("variation-selector-0", color);
            }
            catch (Exception ex)
            {

            }
        }
        public void SelectDimension(string dimension)
        {
            try
            {
                SelectDropDownValue("variation-selector-1", dimension);
            }
            catch (Exception ex)
            {

            }
        }
        public void AddProduct(string productName,string primaryColor, string dimension)
        {
            try
            {
                SelectPrimaryColor("White / Siver");
                SelectDimension("47cm x 36cm");
                ClickButton(btnAddToBasket);
                Thread.Sleep(3000);
                WaitForReady();
            }
            catch (Exception ex)
            {

            }
        }
        public bool VerifyProductInCart(string ProductName)
        {
            try
            {
                var lstItems = _driver.FindElements(By.XPath("//ul[@class='cart-list-items wt-grid__item-xs-12 wt-grid__item-sm-12 wt-grid__item-md-7 wt-grid__item-lg-8 wt-p-xs-0 wt-pr-md-3 wt-height-full wt-list-unstyled wt-pt-xs-2 wt-bt-xs']/li"));
                foreach (var item in lstItems)
                {
                    var itemName = item.FindElement(By.XPath(".//a[@class='wt-text-link-no-underline wt-text-body-01 wt-line-height-tight wt-break-word']")).Text;
                    if (itemName.ToUpper().Contains(ProductName))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteProduct(string ProductName)
        {
            try
            {
                var lstItems = _driver.FindElements(By.XPath("//ul[@class='cart-list-items wt-grid__item-xs-12 wt-grid__item-sm-12 wt-grid__item-md-7 wt-grid__item-lg-8 wt-p-xs-0 wt-pr-md-3 wt-height-full wt-list-unstyled wt-pt-xs-2 wt-bt-xs']/li"));
                foreach (var lstitem in lstItems)
                {
                    var item = lstitem.FindElement(By.XPath(".//a[@class='wt-text-link-no-underline wt-text-body-01 wt-line-height-tight wt-break-word']"));                    
                    var itemName = item.Text;
                    if (itemName.ToUpper().Contains(ProductName))
                    {
                        lstitem.FindElement(By.XPath(".//span[contains(text(),'Remove')]")).FindElement(By.XPath("..")).Click();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditProduct(string ProductName,string primaryColor, string dimension)
        {
            try
            {
                var lstItems = _driver.FindElements(By.XPath("//ul[@class='cart-list-items wt-grid__item-xs-12 wt-grid__item-sm-12 wt-grid__item-md-7 wt-grid__item-lg-8 wt-p-xs-0 wt-pr-md-3 wt-height-full wt-list-unstyled wt-pt-xs-2 wt-bt-xs']/li"));
                foreach (var lstitem in lstItems)
                {
                    var item = lstitem.FindElement(By.XPath(".//a[@class='wt-text-link-no-underline wt-text-body-01 wt-line-height-tight wt-break-word']"));
                    var itemName = item.Text;
                    if (itemName.ToUpper().Contains(ProductName))
                    {
                        lstitem.FindElement(By.XPath(".//a[contains(text(),'Edit')]")).Click();
                        SelectDropDownValue("wt-cart-select-", primaryColor);                        
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
