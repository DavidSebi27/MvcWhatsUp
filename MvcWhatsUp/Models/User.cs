﻿namespace MvcWhatsUp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public User()
        {
            UserId = 0;
            UserName = "";
            MobileNumber = "";
            EmailAddress = "";
            Password = "";
        }
        
        public User(int userId, string userName, string mobileNumber, string emailAddress, string password)
        {
            UserId = userId;
            UserName = userName;
            MobileNumber = mobileNumber; 
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}
