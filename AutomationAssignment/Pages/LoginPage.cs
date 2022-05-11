using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutomationAssignment.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {

        }
        public By SignInBtn = By.XPath("//button[contains(text(),'Sign in')]");
        public By UserName = By.Id("join_neu_email_field");
        public By Password = By.Id("join_neu_password_field");
        public By LoginBtn = By.XPath("//button[(@class = 'wt-btn wt-btn--primary wt-width-full')]");
        public By SignOutbtn = By.XPath("//*[@id='gnav - header - inner']/div[4]/nav/ul/li[3]/div/button");
        public By lblOutbtn = By.XPath("//*[@id='gnav - header - inner']/div[4]/nav/ul/li[3]/div/div/ul/li[7]/a/div[2]");
        public By btnContinueWithGoogl = By.XPath("//span[contains(text(),'Continue with Google')]");
        public By btnRegisterAccount = By.XPath("//button[contains(text(),'Register')]");
        public By txtEmailAddress = By.Id("join_neu_email_field");
        public By txtFirstName = By.Id("join_neu_first_name_field");
        public By txtPassword = By.Id("join_neu_password_field");

        public void Logout()
        {
            try
            {
                var signOutMenu = _driver.FindElements(By.XPath("//nav/ul/li"))[2];
                signOutMenu.FindElement(By.TagName("button")).Click();
                signOutMenu.FindElement(By.TagName("ul")).FindElements(By.TagName("li"))[6].Click();

            }
            catch (Exception ed)
            {

            }
        }
        public void Login(string userName, string password)
        {
            if (IsElementPresent(By.Id("join-neu-overlay")))
            {
                if (_driver.FindElement(By.Id("join-neu-overlay")).Displayed == false)
                {
                    ClickButton(SignInBtn);
                }
            }
            else
                ClickButton(SignInBtn);
            WriteTextToTextBox(UserName, userName);
            WriteTextToTextBox(Password, password);
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            ClickButton(LoginBtn);
            WaitForReady();
            Thread.Sleep(3000);
        }
        public void LoginWithGoogleAccount(string GoogleAccount)
        {
            ClickButton(SignInBtn);
            WaitForReady();
            Thread.Sleep(3000);
            _driver.FindElement(By.XPath("//button[contains(.,'Continue with Google')]")).Click();
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            _driver.FindElement(By.Id("identifierId")).SendKeys(GoogleAccount);
            _driver.FindElement(By.XPath("//span[contains(text(),'Next')]")).FindElement(By.XPath("..")).Click();
        }
        public string RegisterAccount(string email,string firstName,string password)
        {
            ClickButton(SignInBtn);
            WaitForReady();
            Thread.Sleep(3000);
            ClickButton(btnRegisterAccount);
            Thread.Sleep(3000);
            WriteTextToTextBox(txtEmailAddress, email);
            WriteTextToTextBox(txtFirstName, firstName);
            WriteTextToTextBox(txtPassword, password);
            ClickButton(btnRegisterAccount);
            Thread.Sleep(3000);
            if (IsElementPresent(By.Id("aria-join_neu_email_field-error")))
            {
                return GetElement(By.Id("aria-join_neu_email_field-error")).Text;
            }
            else
                return "";
        }
    }
}
