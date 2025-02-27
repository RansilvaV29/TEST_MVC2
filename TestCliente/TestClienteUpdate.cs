using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace TestCliente
{
    public class TestClienteUpdate : IDisposable
    {
        private readonly IWebDriver driver;

        public TestClienteUpdate()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");

            driver = new ChromeDriver(options);
        }

        [Fact]
        public void Update_Cliente_returnSuccess()
        {
            // Navegar a la página principal
            driver.Navigate().GoToUrl("http://localhost:5192");

            // Esperar y hacer clic en "Clientes"
            driver.FindElement(By.Id("BotonClientes")).Click();

            // Buscar el cliente por correo electrónico
            var clienteRow = driver.FindElement(By.XPath("//td[contains(text(), 'raul29247@gmail.com')]"));

            // Hacer clic en el botón de editar del cliente (última columna)
            var editButton = clienteRow.FindElement(By.XPath(".//following-sibling::td//a[contains(@href, '/Cliente/Edit/')]"));
            editButton.Click();

            // Esperar que se cargue el formulario de edición (usar Thread.Sleep)
            System.Threading.Thread.Sleep(2000);

            // Modificar los datos del cliente
            driver.FindElement(By.Id("Cedula")).Clear();
            driver.FindElement(By.Id("Cedula")).SendKeys("0506070810");

            driver.FindElement(By.Id("Apellidos")).Clear();
            driver.FindElement(By.Id("Apellidos")).SendKeys("Lopez");

            driver.FindElement(By.Id("Nombres")).Clear();
            driver.FindElement(By.Id("Nombres")).SendKeys("David Nuevo");

            driver.FindElement(By.Name("FechaNacimiento")).SendKeys("12/09/2001");

            driver.FindElement(By.Id("Mail")).Clear();
            driver.FindElement(By.Id("Mail")).SendKeys("david.nuevo@email.com");

            driver.FindElement(By.Id("Telefono")).Clear();
            driver.FindElement(By.Id("Telefono")).SendKeys("0998765432");

            driver.FindElement(By.Id("Direccion")).Clear();
            driver.FindElement(By.Id("Direccion")).SendKeys("Nuevo Centro Histórico");

            var estadoSelect = driver.FindElement(By.Id("Estado"));
            var option = estadoSelect.FindElement(By.XPath("//option[@value='True']"));
            option.Click();

            driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();

            System.Threading.Thread.Sleep(2000);

            var clientes = driver.FindElements(By.XPath("//td[contains(text(), 'david.nuevo@email.com')]"));
            Assert.NotEmpty(clientes);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
