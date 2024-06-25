using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;


#pragma warning disable CS8602, CS8604, CS8618

namespace TestApiProject.StepDefinitions
{
    [Binding]
    public class CreateBookingSteps
    {
        private RestClient _client; 
        private RestRequest _request;
        private RestResponse _response;

        //[Given(@"I have a booking request with (.+), (.+), (.+), (.+), (.+), (.+), and (.+)")]
        [Given(@"I have a booking request with (.*), (.*), (.*), (.*), (.*), (.*), and (.*)")]
        public void GivenIHaveABookingRequestWith(string firstName, string lastName, int totalPrice, bool depositPaid, string checkInDate, string checkOutDate, string additionalNeeds)
        {
            // Prepare the booking request
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

            // Initialize RestClient and RestRequest
            _client = new RestClient("https://restful-booker.herokuapp.com");
            _request = new RestRequest("/booking",RestSharp.Method.Post);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Accept", "application/json");
            _request.AddJsonBody(bookingRequest);

            var jsonBody = JsonConvert.SerializeObject(bookingRequest, Formatting.Indented);
                  
        }

        [When(@"I send a POST request to create the booking")]
        public void WhenISendAPOSTRequestToCreateTheBooking()
        {
            // Execute the request
            _response = _client.Execute(_request);

        }

        //[Then(@"the booking should be successfully created with the same (.+), (.+), (.+), (.+), (.+), (.+), and (.+)")]
        [Then(@"the booking creation should be (.*) with the same (.*), (.*), (.*), (.*), (.*), (.*), and (.*)")]
        public void ThenTheBookingShouldBeCreatedWith(string ExpectedStatus, string firstName, string lastName, int totalPrice, bool depositPaid, string checkInDate, string checkOutDate, string additionalNeeds)
        {
            // Deserialize the response to verify the booking details
            var bookingResponse = JsonConvert.DeserializeObject<dynamic>(_response.Content);
            
            // Assert the response data
            if(ExpectedStatus == "Successfull")
            {
                Assert.AreEqual(firstName, bookingResponse.booking.firstname.ToString(), "First name mismatch");
                Assert.AreEqual(lastName, bookingResponse.booking.lastname.ToString(), "Last name mismatch");
                Assert.AreEqual(totalPrice, (int)bookingResponse.booking.totalprice, "Total price mismatch");
                Assert.AreEqual(depositPaid, (bool)bookingResponse.booking.depositpaid, "Deposit paid mismatch");
                Assert.AreEqual(checkInDate, bookingResponse.booking.bookingdates.checkin.ToString(), "Check-in date mismatch");
                Assert.AreEqual(checkOutDate, bookingResponse.booking.bookingdates.checkout.ToString(), "Check-out date mismatch");
                Assert.AreEqual(additionalNeeds, bookingResponse.booking.additionalneeds.ToString(), "Additional needs mismatch");
            }
            else
            {
                //Validate Error Message as Required
                //Placeholder currently API only handles happy scenarios
            }
        }

        [Then(@"the response status code should be (.+)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            // Assert the response status code
            Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode, $"Expected status code {expectedStatusCode}, but got {_response.StatusCode}");
        }
    }
}
