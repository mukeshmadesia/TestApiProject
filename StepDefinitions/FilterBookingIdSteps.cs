using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

#pragma warning disable CS8602, CS8604, CS8618, CS8601

namespace TestApiProject.StepDefinitions
{
    [Binding]
    public class RetrieveBookingSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private List<BookingIdResponse> _bookingIds;

        [Given(@"I have a booking request with query parameters (.*), (.*), (.*), (.*)")]
        public void GivenIHaveABookingRequestWithQueryParameters(string firstName, string lastName, string checkInDate, string checkOutDate)
        {
            //_client = new RestClient("https://restful-booker.herokuapp.com");

            _client = new RestClient(Config.ApiBaseUrl);

            _request = new RestRequest("/booking", Method.Get);
            _request.AddHeader("Accept", "application/json");

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                _request.AddQueryParameter("firstname", firstName);
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                _request.AddQueryParameter("lastname", lastName);
            }
            if (!string.IsNullOrWhiteSpace(checkInDate))
            {
                _request.AddQueryParameter("checkin", checkInDate);
            }
            if (!string.IsNullOrWhiteSpace(checkOutDate))
            {
                _request.AddQueryParameter("checkout", checkOutDate);
            }
        }

        [When(@"I send a GET request to retrieve the booking IDs")]
        public void WhenISendAGETRequestToRetrieveTheBookingIDs()
        {
            _response = _client.Execute(_request);
        }

        [Then(@"the filter response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode, $"Expected status code {expectedStatusCode}, but got {(int)_response.StatusCode}");
        }

        [Then(@"the response should contain atleast (.+) booking IDs")]
        public void ThenTheResponseShouldContainBookingIDs(int noOfBooking)
        {
            if (_response.Content == null || string.IsNullOrWhiteSpace(_response.Content))
            {
                throw new Exception("Response content is null or empty");
            }

            try
            {
                _bookingIds = JsonConvert.DeserializeObject<List<BookingIdResponse>>(_response.Content);
                Assert.IsTrue(_bookingIds.Count >= noOfBooking, "Expected at least one booking ID in the response");
            }
            catch (JsonException ex)
            {
                throw new Exception("Error parsing response content", ex);
            }
        }

        public class BookingIdResponse
        {
            public int bookingid { get; set; }
        }
    }
}
