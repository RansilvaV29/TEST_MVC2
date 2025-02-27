using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace TestCliente
{
    public class UnitTest1
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;


        public UnitTest1()
        {
            var options = new ChromeOptions();
            // Evitar que Chrome detecte el WebDriver
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--start-maximized");



            _driver = new ChromeDriver(@"C:\chromedriver", options);

            _driver.Manage().Window.Maximize();

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        }

        public bool EsMailValido(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        [Theory]
        [InlineData("usuario@gmail.com", true)]
        [InlineData("test@empresa.com", true)]
        [InlineData("correo_invalido.com", false)]
        [InlineData("sinformatocorrecto", false)]

        public void ValidarEmail_DeberiaDerectarCorreosValidos(string email, bool esperado)
        {
            bool resultado = EsMailValido(email);
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void Test_NavegadorGoogle()
        {
            try
            {
                _driver.Navigate().GoToUrl("http://www.google.com");

                var buscarTexto = _driver.FindElement(By.Name("q"));

                buscarTexto.SendKeys("Selenium");

                buscarTexto.SendKeys(Keys.Enter);

                var resultados = _wait.Until(d => d.FindElements(By.CssSelector("h3")));

                Assert.True(resultados.Count > 0, "No se encontraron resultados de la busqueda");

                Thread.Sleep(20000);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }


    }
}