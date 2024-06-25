using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

#pragma warning disable CS8602, CS8604, CS8618

namespace TestApiProject.StepDefinitions
{
    [Binding]
    public class CheckBookingSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _bookingId;
        private int expectedStatusCode;

        [Given(@"I have a GET booking request with (.+), (.+), (.+), (.+), (.+), (.+), (.+) and (.*)")]
        public void GivenIHaveABookingRequestWith(string firstName, string lastName, int totalPrice, bool depositPaid, string checkInDate, string checkOutDate, string additionalNeeds, string bookingId)
        {
        
            _client = new RestClient(Config.ApiBaseUrl);
            if (string.IsNullOrWhiteSpace(bookingId))
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

                _request = new RestRequest("/booking", Method.Post);
                _request.AddHeader("Content-Type", "application/json");
                _request.AddHeader("Accept", "application/json");
                _request.AddJsonBody(bookingRequest);
                
            }
            else
            {
                _bookingId = bookingId;
            }
        }

        [Given(@"I create a new booking if not ID provided")]
        public void WhenICreateANewBooking()
        {

            if (string.IsNullOrWhiteSpace(_bookingId))
            {
                _response = _client.Execute(_request);

                Console.WriteLine("--response body--");
                Console.WriteLine(_response.Content);
                Console.WriteLine("----");


                var bookingResponse = JsonConvert.DeserializeObject<dynamic>(_response.Content);
                _bookingId = bookingResponse.bookingid;

                Console.WriteLine("--create response--");
                Console.WriteLine(bookingResponse);
                Console.WriteLine("----");
                Console.WriteLine(_bookingId);
                Console.WriteLine("--_bookingId up--");

                
            }
      


        }

        [When(@"I retrieve booking details for (.*) (.*)")]
        public void WhenIRetrieveBookingDetailsFor(string scenario, string bookingId)
        {
            Console.WriteLine("--input bookingid--");
            Console.WriteLine(bookingId);
            Console.WriteLine("----");

            if (string.IsNullOrWhiteSpace(bookingId))
            {
                bookingId = _bookingId;
            }
           
            _request = new RestRequest($"/booking/{bookingId.Trim()}", Method.Get);
            _request.AddHeader("Accept", "application/json");
            _response =  _client.Execute(_request);


        }

        [Then(@"the booking details should match for the (.+) data (.+), (.+), (.+), (.+), (.+), (.+), (.+) and (.*)")]
        public void ThenTheBookingDetailsShouldMatchTheProvidedData(string scenario, string firstName, string lastName, int totalPrice, bool depositPaid, string checkInDate, string checkOutDate, string additionalNeeds, string bookingId)
        {
                    
            if (scenario == "provided" || scenario == "created")
            {
                expectedStatusCode = 200;
                var bookingDetails = JsonConvert.DeserializeObject<dynamic>(_response.Content);

                Assert.AreEqual(firstName, bookingDetails.firstname.ToString(), "First name mismatch");
                Assert.AreEqual(lastName, bookingDetails.lastname.ToString(), "Last name mismatch");
                Assert.AreEqual(totalPrice, (int)bookingDetails.totalprice, "Total price mismatch");
                Assert.AreEqual(depositPaid, (bool)bookingDetails.depositpaid, "Deposit paid mismatch");
                Assert.AreEqual(checkInDate, bookingDetails.bookingdates.checkin.ToString(), "Check-in date mismatch");
                Assert.AreEqual(checkOutDate, bookingDetails.bookingdates.checkout.ToString(), "Check-out date mismatch");
                Assert.AreEqual(additionalNeeds, bookingDetails.additionalneeds.ToString(), "Additional needs mismatch");
            }
            else
            {
                expectedStatusCode = 404;
            }


            // Assert the response status code
            Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode, $"Booking ID: {_bookingId} is Not found; Expected status code {expectedStatusCode}, but got {(int)_response.StatusCode}");

        }
    }
}
