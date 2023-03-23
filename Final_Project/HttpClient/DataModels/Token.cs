﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest.DataModels
{
    public partial class Token
    {
        [JsonProperty("token")]
        public string TokenAuth { get; set; }
    }

    public class User
    {
        public User()
        {
            Username = "admin";
            Password = "password123";
        }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
