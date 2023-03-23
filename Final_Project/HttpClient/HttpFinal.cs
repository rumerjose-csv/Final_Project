using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpClientTest.DataModels;
using HttpClientTest.Helpers;
using HttpClientTest.Resources;
using HttpClientTest.Tests;
using HttpClientTest.Tests.TestData;
using Newtonsoft.Json;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]

namespace HttpClientTest
{
    [TestClass]

    public class HttpFinal : HttpBaseTest
    {
        private readonly List<OrderDetail> cleanUpList = new List<OrderDetail>();

        [TestInitialize]
        
        public async Task Initialize()
        {
            httpClient = new HttpClient();

            Token token = await userReference.AuthenticateUser(httpClient);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cookie", $"token={token.TokenAuth}");
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (var data in cleanUpList)
            {
                await httpClient.DeleteAsync(Endpoints.DeleteBookingById(data.BookingId));
            }
        }

        [TestMethod]

        public async Task AddBooking()
        {
            BookingModels booking = GenerateBooking.bookingDetails();
            
            var postBooking = await BookingHelper.PostBooking(httpClient, booking);
            var deserializedResp = JsonConvert.DeserializeObject<OrderDetail>(postBooking.Content.ReadAsStringAsync().Result);

            cleanUpList.Add(deserializedResp);

            var getResp = await BookingHelper.GetBookingById(httpClient, deserializedResp.BookingId);

            Assert.AreEqual(HttpStatusCode.OK, postBooking.StatusCode, "Status mismatch. Should be 200");
            Assert.AreEqual(booking.FirstName, getResp.FirstName, "Firstname mismatch");
            Assert.AreEqual(booking.LastName, getResp.LastName, "Lastname mismatch");
            Assert.AreEqual(booking.TotalPrice, getResp.TotalPrice, "Total price mismatch");
            Assert.AreEqual(booking.DepositPaid, getResp.DepositPaid, "Deposit paid mismatch");
            Assert.AreEqual(booking.BookingDates.CheckIn, getResp.BookingDates.CheckIn, "Check-in date mismatch");
            Assert.AreEqual(booking.BookingDates.CheckOut, getResp.BookingDates.CheckOut, "Check-out date mismatch");
            Assert.AreEqual(booking.AdditionalNeeds, getResp.AdditionalNeeds, "Additional need mismatch");
        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            BookingModels booking = GenerateBooking.bookingDetails();

            var postBooking = await BookingHelper.PostBooking(httpClient, booking);
            var deserializedResp = JsonConvert.DeserializeObject<OrderDetail>(postBooking.Content.ReadAsStringAsync().Result);

            cleanUpList.Add(deserializedResp);

            deserializedResp.Booking.FirstName = "John";
            deserializedResp.Booking.LastName = "Wick";

            var putResp = await BookingHelper.PutBookingById(httpClient, deserializedResp);

            var getResp = await BookingHelper.GetBookingById(httpClient, deserializedResp.BookingId);

            Assert.AreEqual(HttpStatusCode.OK, putResp.StatusCode, "Status code mismatch. Should be 200");
            Assert.AreEqual(deserializedResp.Booking.FirstName, getResp.FirstName, "Firstname mismatch");
            Assert.AreEqual(deserializedResp.Booking.LastName, getResp.LastName, "Lastname mismatch");
        }

        [TestMethod]

        public async Task RemoveBooking()
        {
            BookingModels booking = GenerateBooking.bookingDetails();

            var postBooking = await BookingHelper.PostBooking(httpClient, booking);
            var deserializedResp = JsonConvert.DeserializeObject<OrderDetail>(postBooking.Content.ReadAsStringAsync().Result);

            cleanUpList.Add(deserializedResp);

            var deleteResponse = await BookingHelper.DeleteBookingById(httpClient, deserializedResp.BookingId);

            Assert.AreEqual(HttpStatusCode.Created, deleteResponse.StatusCode, "Status code mismatch. Should be 200");
        }

        [TestMethod]
        public async Task GetInvalidBookingId()
        {
            long invId = 830830;

            var getResponse = await BookingHelper.GetBookingByInvalidId(httpClient, invId);

            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "Status code mismatch. Should be 200");
        }
    }
}
