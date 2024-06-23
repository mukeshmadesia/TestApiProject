using TechTalk.SpecFlow;
using NUnit.Framework;

namespace TestApiProject.StepDefinitions
{
    [Binding]
    public class SubstractionSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public SubstractionSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"I press sub")]
        public void WhenIPressSub()
        {
            int firstNumber = _scenarioContext.Get<int>("FirstNumber");
            int secondNumber = _scenarioContext.Get<int>("SecondNumber");
            _scenarioContext["Result"] = firstNumber - secondNumber;
        }

    }
}

