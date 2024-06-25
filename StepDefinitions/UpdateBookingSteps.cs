using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

#pragma warning disable CS8602, CS8604, CS8618

namespace TestApiProject.StepDefinitions
{
    [Binding]
    public class UpdateBookingSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private int _bookingId;

        [Given(@"I have a created booking with (.*), (.*), (.*), (.*), (.*), (.*), and (.*)")]
        public void GivenIHaveACreatedBookingWith(string firstName, string lastName, int totalPrice, bool depositPaid, string checkInDate, string checkOutDate, string additionalNeeds)
        {
            var bookingRequest = new
            {
                firstname = firstName,
                lastname = lastName,
                totalprice = totalPrice,
                depositpaid = depositPaid,
                bookingdates = new
                {
                    checkin = checkInDate,
                    checkout = checkOutDate
                },
                additionalneeds = additionalNeeds
            };

            _client = new RestClient(Config.ApiBaseUrl);
            _request = new RestRequest("/booking", Method.Post);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Accept", "application/json");
            _request.AddJsonBody(bookingRequest);

            _response =  _client.Execute(_request);
            var bookingResponse = JsonConvert.DeserializeObject<dynamic>(_response.Content);
            _bookingId = bookingResponse.bookingid;
        }

        [When(@"I send a PUT request to update the booking with (.*), (.*), (.*), (.*), (.*), (.*), and (.*)")]
        public void WhenISendAPUTRequestToUpdateTheBookingWith(string updatedFirstName, string updatedLastName, int updatedTotalPrice, bool updatedDepositPaid, string updatedCheckInDate, string updatedCheckOutDate, string updatedAdditionalNeeds)
        {
            var updateRequest = new
            {
                firstname = updatedFirstName,
                lastname = updatedLastName,
                totalprice = updatedTotalPrice,
                depositpaid = updatedDepositPaid,
                bookingdates = new
                {
                    checkin = updatedCheckInDate,
                    checkout = updatedCheckOutDate
                },
                additionalneeds = updatedAdditionalNeeds
            };

            _request = new RestRequest($"/booking/{_bookingId}", Method.Put);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Accept", "application/json");
            _request.AddHeader("Authorization", $"{Config.AuthorizationKey}");
            _request.AddJsonBody(updateRequest);

            _response =  _client.Execute(_request);
        }

        [Then(@"the booking update should be (.*) with the same (.*), (.*), (.*), (.*), (.*), (.*), and (.*)")]
        public void ThenTheBookingShouldBeUpdatedSuccessfullyWith(string ExpectedStatus, string updatedFirstName, string updatedLastName, int updatedTotalPrice, bool updatedDepositPaid, string updatedCheckInDate, string updatedCheckOutDate, string updatedAdditionalNeeds)
        {

            var bookingResponse = JsonConvert.DeserializeObject<dynamic>(_response.Content);

            if (ExpectedStatus == "Successfull")
            {

                Assert.AreEqual(updatedFirstName, bookingResponse.firstname.ToString(), "First name mismatch");
                Assert.AreEqual(updatedLastName, bookingResponse.lastname.ToString(), "Last name mismatch");
                Assert.AreEqual(updatedTotalPrice, (int)bookingResponse.totalprice, "Total price mismatch");
                Assert.AreEqual(updatedDepositPaid, (bool)bookingResponse.depositpaid, "Deposit paid mismatch");
                Assert.AreEqual(updatedCheckInDate, bookingResponse.bookingdates.checkin.ToString(), "Check-in date mismatch");
                Assert.AreEqual(updatedCheckOutDate, bookingResponse.bookingdates.checkout.ToString(), "Check-out date mismatch");
                Assert.AreEqual(updatedAdditionalNeeds, bookingResponse.additionalneeds.ToString(), "Additional needs mismatch");
            }
            else
            {
                //Statement for Failure; Currently API does not support Failure conditions
            }
        }

        [Then(@"the update response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode, $"Expected status code {expectedStatusCode}, but got {_response.StatusCode}");
        }
    }
}
