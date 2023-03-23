using System;
using HttpClientTest.DataModels;
using HttpClientTest.Resources;

namespace HttpClientTest
{
    [TestClass]
    public class HttpBaseTest
    {
        public HttpClient httpClient { get; set; }

        public BookingModels bookingModel { get; set; }

       

        [TestInitialize]
        public void Initialize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public void CleanUp()
        {
           
        }
    }
}