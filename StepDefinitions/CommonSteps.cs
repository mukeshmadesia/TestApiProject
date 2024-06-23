using System;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestApiProject.Utilities;

namespace TestApiProject.StepDefinitions
{
    [Binding]
    public class CommonSteps
	{
        private readonly ScenarioContext _scenarioContext;

        public CommonSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            if (!_scenarioContext.ContainsKey("FirstNumber"))
                _scenarioContext["FirstNumber"] = number;
            else
                _scenarioContext["SecondNumber"] = number;

            Console.WriteLine($"Extent - Before Repot Manager");
            ExtentReportManager.LogInfo($"Entered number: {number}");

        }


        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            int result = _scenarioContext.Get<int>("Result");
            Assert.AreEqual(expectedResult, result);
            ExtentReportManager.LogInfo($"Expected result: {expectedResult}, Actual result: {result}");
        }
    }

    
}

