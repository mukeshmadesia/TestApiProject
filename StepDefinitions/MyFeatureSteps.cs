using TechTalk.SpecFlow;
using NUnit.Framework;
using TestApiProject.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestApiProject.StepDefinitions
{
    [Binding]
    public class MyFeatureSteps

    {
        private readonly ScenarioContext _scenarioContext;

        public MyFeatureSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //[Given(@"I have entered (.*) into the calculator")]
        //public void GivenIHaveEnteredIntoTheCalculator(int number)
        //{
        //    if (!_scenarioContext.ContainsKey("FirstNumber"))
        //        _scenarioContext["FirstNumber"] = number;
        //    else
        //        _scenarioContext["SecondNumber"] = number;
        //}

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            int firstNumber = _scenarioContext.Get<int>("FirstNumber");
            int secondNumber = _scenarioContext.Get<int>("SecondNumber");
            _scenarioContext["Result"] = firstNumber + secondNumber;
            ExtentReportManager.LogInfo($"Adding the Numers");
        }

        //[Then(@"the result should be (.*) on the screen")]
        //public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        //{
        //    int result = _scenarioContext.Get<int>("Result");
        //    Assert.AreEqual(expectedResult, result);
        //}
    }
}

