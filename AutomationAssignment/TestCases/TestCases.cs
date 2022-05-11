using AutomationAssignment.Pages;
using NUnit.Framework;
using OfficeOpenXml;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutomationAssignment.TestCases
{
    [TestFixture]
    public class TestCases:BaseTest
    {
        public static LoginPage loginPage = new LoginPage(_driver);
        public static HomePage homePage = new HomePage(_driver);
        public static ProductPage productPage = new ProductPage(_driver);        
        public static FileInfo UsersDetail = new FileInfo(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\Users.xlsx");
        [Test]
        public void LoginTest()
        {
            try
            {
                loginPage.Login("test1478@gmail.com", "Test@123");                
                if (loginPage.IsElementPresent(loginPage.SignInBtn))
                {
                    test.Error("Login Failed");
                    Assert.Fail("Login Failed");
                }
                else
                {
                    test.Pass("Logged in Successfully");
                    Assert.Pass("Logged in Successfully");
                }

            }
            catch (Exception ex)
            {
                test.Error("Login Failed Due to " + ex.Message);
            }

        }
        [Test]
        public void AddProduct()
        {
            try
            {
                LoginTest();
                homePage.SearchProduct("wall decor");
                homePage.SendKeyTextBox(homePage.txtSearch, Keys.Enter);
                string productName = "ASTRONAUT STATUE - ASTRONAUT SCULPTURE, WALL DECOR, WALL";
                homePage.ClickProduct(productName);
                homePage.WaitForReady();                
                productPage.AddProduct(productName, "White / Siver", "47cm x 36cm");
                if (productPage.VerifyProductInCart(productName))
                {
                    test.Pass("Product Added Successfully");
                    Assert.Pass("Product Added Successfully");
                }
                //productPage.DeleteProduct(productName);
            }
            catch (Exception ex)
            {

            }

        }
        [Test]
        public void VerifyUsers()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage Users = new ExcelPackage(UsersDetail);
            ExcelWorksheet usersSheet = Users.Workbook.Worksheets[0];
            var start = usersSheet.Dimension.Start;
            var end = usersSheet.Dimension.End;
            string userName = "";
            string password = "";
            //loginPage.ClickButton(loginPage.SignInBtn);
            for (int row = start.Row +1; row <= end.Row; row++)
            {
                userName = usersSheet.Cells[row, 1].Text.ToString();
                password = usersSheet.Cells[row, 2].Text.ToString();
                loginPage.Login(userName, password);
                if (loginPage.IsElementPresent(loginPage.SignInBtn))
                {
                    continue;
                }
                else
                {
                    loginPage.Logout();                    
                }
            }
            
        }
        [Test]
        public void LoginWithGoogle()
        {
            try
            {
                loginPage.LoginWithGoogleAccount("test@gmail.com");
                if (loginPage.IsElementPresent(loginPage.SignInBtn))
                {
                    test.Error("Login Failed");
                    Assert.Fail("Login Failed");
                }
                else
                {
                    test.Pass("Logged in Successfully");
                    Assert.Pass("Logged in Successfully");
                }

            }
            catch (Exception ex)
            {
                test.Error("Login Failed Due to " + ex.Message);
            }

        }
        [Test]
        public void DeleteProduct()
        {
            try
            {
                LoginTest();
                string productName = "ASTRONAUT STATUE - ASTRONAUT SCULPTURE, WALL DECOR, WALL";
                homePage.OpenBasket();
                if (productPage.VerifyProductInCart(productName))
                {
                    productPage.DeleteProduct(productName);
                    test.Pass("Product Deleted Successfully");
                    Assert.Pass("Product Deleted Successfully");
                }
                
            }
            catch (Exception ex)
            {

            }

        }
        [Test]
        public void EditProduct()
        {
            try
            {
                LoginTest();
                string productName = "ASTRONAUT STATUE - ASTRONAUT SCULPTURE, WALL DECOR, WALL";
                //var btnBasket = _driver.FindElements(By.XPath("//nav/ul/li"))[3];
                //btnBasket.Click();
                homePage.OpenBasket();
                homePage.WaitForReady();
                if (productPage.VerifyProductInCart(productName))
                {
                    productPage.EditProduct(productName, "Black / Silver", "18,5'' x 14,1''");
                    test.Pass("Product Updatad Successfully");
                    Assert.Pass("Product Updated Successfully");
                }
            }
            catch (Exception ex)
            {

            }

        }
        [Test]
        public void RegisterUser()
        {
            try
            {
                string text = loginPage.RegisterAccount("Test85845@gmail.com", "Test", "Test@123456");
                if (text == "")
                {
                    test.Pass("User Added Successfully");
                    Assert.Pass("User Added Successfully");
                }
                else
                {
                    test.Fail(text);
                    Assert.Fail(text);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
