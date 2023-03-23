using HttpClientTest.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest.Tests.TestData
{
    public class GenerateBooking
    {
        public static BookingModels bookingDetails()
        {
            return new BookingModels
            {
                FirstName = "Juan",
                LastName = "Carlos",
                TotalPrice = 1000,
                DepositPaid = true,
                BookingDates = new BookingDates 
                { 
                    CheckIn = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")), 
                    CheckOut = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) 
                },
                AdditionalNeeds = "Final Payment"
            };
        }

        public static OrderDetail newDetails()
        {
            return new OrderDetail
            {

            };
        }
    }
}
