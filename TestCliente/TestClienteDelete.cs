using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using System.Threading;

namespace TestCliente
{
    public class TestClienteDelete : IDisposable
    {
        private readonly IWebDriver driver;

        public TestClienteDelete()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");

            driver = new ChromeDriver(options);
        }

        [Fact]
        public void Delete_Cliente_returnSuccess()
        {
            driver.Navigate().GoToUrl("http://localhost:5192");

            Thread.Sleep(2000);

            driver.FindElement(By.Id("BotonClientes")).Click();

            Thread.Sleep(2000);

            var clienteRow = driver.FindElement(By.XPath("//td[contains(text(), 'raul29247@gmail.com')]"));

            var deleteButton = clienteRow.FindElement(By.XPath(".//following-sibling::td//a[contains(@href, '/Cliente/Delete/')]"));
            deleteButton.Click();

            Thread.Sleep(2000);

            var header = driver.FindElement(By.TagName("h1")).Text;
            Assert.Contains("Eliminar Cliente", header);

            var clienteCedula = driver.FindElement(By.XPath("//dt[contains(text(), 'Cédula:')]//following-sibling::dd")).Text;
            Assert.Equal("1720911112", clienteCedula);

            driver.FindElement(By.CssSelector("button.btn.btn-danger")).Click();

            Thread.Sleep(2000);

            // Validar que el cliente ya no esté en la lista
            var clientes = driver.FindElements(By.XPath("//td[contains(text(), 'raul29247@gmail.com')]"));
            Assert.Empty(clientes);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
