using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ReqnrollTestProject.Utilities
{
    public static class WebDriverManager
    {
        public static IWebDriver GetDriver(string browser)
        {
            new DriverManager().SetUpDriver(new ChromeConfig()); // Descarga automáticamente la versión correcta
            return browser.ToLower() switch
            {
                "chrome" => new ChromeDriver(),
                _ => throw new ArgumentException("Navegador no soportado")
            };
        }
    }

}
