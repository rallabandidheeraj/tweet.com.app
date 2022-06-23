using com.tweetapp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.tweetapp.DAO
{
    interface IRepository
    {
        public bool UserLogin(string emailId, string password);
        public bool UserRegistration(UserInfo reg);
        public bool PostTweet(Tweet tweet);
        public List<Tweet> GetTweetById(string EmailID);

        public List<Tweet> GetAllTweet();
        public bool ForgetPassword(string user, DateTime dob, string pass);
        public bool ResetPassword(string user, string oldpass, string pass);



    }
}
