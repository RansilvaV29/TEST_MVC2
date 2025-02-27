using Reqnroll;
using ReqnrollTest.Reportes;

namespace ReqnrollTestProject1.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtendReportManager.InitReport();
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            ExtendReportManager.StartTest(scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public static void AfterStep(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo.Text;
            bool isSuccess = scenarioContext.TestError == null;

            ExtendReportManager.LogStep(isSuccess, isSuccess ? $"Paso exitoso: {stepInfo}"
                : $"Error: {scenarioContext.TestError?.Message}");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtendReportManager.FlushReport();
        }
    }
}
