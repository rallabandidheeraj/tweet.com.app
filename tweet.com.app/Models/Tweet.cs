using System;
using System.Collections.Generic;
using System.Text;

namespace com.tweetapp.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public string EmailId { get; set; }
        public DateTime? TweetTime { get; set; } 
        public string TweetMessage { get; set; }
        public virtual UserInfo User { get; set; }
    }
}
