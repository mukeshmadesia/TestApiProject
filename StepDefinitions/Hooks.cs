using TechTalk.SpecFlow;
using TestApiProject.Utilities;

namespace TestApiProject.StepDefinitions
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReportManager.InitReport();
        }


        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            ExtentReportManager.CreateTest(scenarioContext.ScenarioInfo.Title);
            ExtentReportManager.LogInfo("Starting scenario: " + scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                ExtentReportManager.LogFail(scenarioContext.TestError.Message);
            }
            else
            {
                ExtentReportManager.LogPass("Step passed: " + scenarioContext.StepContext.StepInfo.Text);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ExtentReportManager.LogInfo("Ending scenario");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportManager.FlushReport();
        }
    }
}
