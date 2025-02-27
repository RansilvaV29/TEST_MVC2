using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;

namespace ReqnrollTest.Reportes
{
    public static class ExtendReportManager
    {
        private static ExtentReports _extent;
        private static ExtentTest _test;

        public static void InitReport()
        {
            // Ruta donde se guardará el reporte
            string reportPath = $"{AppDomain.CurrentDomain.BaseDirectory}ExtentReport.html";

            var sparkReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);

            _extent.AddSystemInfo("Proyecto", "Reqnroll Test Automation");
            _extent.AddSystemInfo("Plataforma", Environment.OSVersion.ToString());
            _extent.AddSystemInfo("Ejecutado por", Environment.UserName);
        }

        public static void StartTest(string testName)
        {
            _test = _extent.CreateTest(testName);
        }

        public static void LogStep(bool isSuccess, string message)
        {
            if (_test == null) return;

            if (isSuccess)
            {
                _test.Log(Status.Pass, message);
            }
            else
            {
                _test.Log(Status.Fail, message);
            }
        }

        public static void FlushReport()
        {
            _extent.Flush();
        }
    }
}
