using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V130.Target;
using OpenQA.Selenium.Support.UI;

namespace TestCliente
{
    public class Prueba : IDisposable
    {
        private readonly IWebDriver driver;

        public Prueba()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        public bool EsMailValido(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        [Fact]
        public void Login_Con_Datos_Vacios()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/login");

            driver.FindElement(By.Name("email")).SendKeys("");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("password")).SendKeys("");
            Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("button[data-qa='login-button']")).Click();
            Thread.Sleep(1000);

            bool isEmailFieldRequired = driver.FindElement(By.Name("email")).GetAttribute("required") == "true";
            Assert.True(isEmailFieldRequired, "El campo de correo no es requerido.");

            bool isPasswordFieldRequired = driver.FindElement(By.Name("password")).GetAttribute("required") == "true";
            Assert.True(isPasswordFieldRequired, "El campo de contraseña no es requerido.");
        }


        [Fact]
        public void Login_Con_Correo_Invalido()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/login");

            driver.FindElement(By.Name("email")).SendKeys("qwrewr@GMAIL");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("password")).SendKeys("qweqweqweqwe");
            Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("button[data-qa='login-button']")).Click();
            Thread.Sleep(1000);

            var errorMessage = driver.FindElement(By.XPath("//p[contains(text(),'Your email or password is incorrect!')]"));

            Assert.NotNull(errorMessage);
            Assert.Equal("Your email or password is incorrect!", errorMessage.Text);
        }

        [Fact]
        public void login_con_correo_invalido_y_contraseña_incorrecta()
        {
            driver.Navigate().GoToUrl("https://automationexercise.com/login");

            driver.FindElement(By.Name("email")).SendKeys("raul29247@gmail.com");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("password")).SendKeys("qweqweqew");
            Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("button[data-qa='login-button']")).Click();
            Thread.Sleep(1000);

            IWebElement errormessage = driver.FindElement(By.XPath("//p[contains(text(),'Your email or password is incorrect!')]"));

            Assert.True(errormessage.Displayed, "El mensaje de error no se mostró.");
        }


        [Fact]
        public void Login_Con_Datos_Validos()
        {
            string email = "raul29247@gmail.com";
            string password = "peluchitaQ1";

            // Validar si el email es válido
            if (!EsMailValido(email))
            {
                throw new ArgumentException("El correo electrónico no es válido.");
            }

            driver.Navigate().GoToUrl("https://automationexercise.com/login");

            driver.FindElement(By.Name("email")).SendKeys(email);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("password")).SendKeys(password);
            Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("button[data-qa='login-button']")).Click();
            Thread.Sleep(1000);

            IWebElement logoutLink = driver.FindElement(By.XPath("//a[@href='/logout'][contains(text(),'Logout')]"));

            Assert.True(logoutLink.Displayed, "El mensaje de logout no se mostró.");
        }


        public void Dispose()
        {
            driver.Quit();
        }
    }
}