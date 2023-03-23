using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace HttpClientTest.Resources
{
    public class Endpoints
    {
        public const string BaseUrl = "https://restful-booker.herokuapp.com";

        public static string PostBooking() => $"{BaseUrl}/booking";

        public static string GetBookingById(long id) => $"{BaseUrl}/booking/{id}";

        public static string PutBookingById(long id) => $"{BaseUrl}/booking/{id}";

        public static string DeleteBookingById(long id) => $"{BaseUrl}/booking/{id}";

        public static string AuthenticateUser() => $"{BaseUrl}/auth";
    }
}
