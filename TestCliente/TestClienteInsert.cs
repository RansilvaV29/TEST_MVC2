using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V130.Browser;
using OpenQA.Selenium.Support.UI;

namespace TestCliente
{
    public class TestClienteInsert : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public TestClienteInsert()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");

            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Create_Cliente_returnData()
        {
            driver.Navigate().GoToUrl("http://localhost:5192");

            // Esperar y hacer clic en "Clientes"
            wait.Until(d => d.FindElement(By.Id("BotonClientes"))).Click();
            wait.Until(d => d.FindElement(By.Id("agregarBoton"))).Click();

            // Llenar formulario
            driver.FindElement(By.Name("Cedula")).SendKeys("1720911112");
            driver.FindElement(By.Name("Apellidos")).SendKeys("Silva");
            driver.FindElement(By.Name("Nombres")).SendKeys("Raul");
            driver.FindElement(By.Name("FechaNacimiento")).SendKeys("12/09/2001");
            driver.FindElement(By.Name("Mail")).SendKeys("raul29247@gmail.com");
            driver.FindElement(By.Name("Telefono")).SendKeys("0978967634");
            driver.FindElement(By.Name("Direccion")).SendKeys("la Mena");

            // Seleccionar estado
            var selectElement = new SelectElement(driver.FindElement(By.Name("Estado")));
            selectElement.SelectByValue("True");

            // Enviar formulario
            driver.FindElement(By.Id("botonSubmit")).Click();

            // Validar URL de redirección
            Assert.Contains("/Cliente", driver.Url);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
