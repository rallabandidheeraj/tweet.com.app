using System;
using System.Collections.Generic;
using System.Text;

namespace com.tweetapp.Models
{
    public class UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string EmailId { get; set; }
        public string Passcode { get; set; }
        public ICollection<Tweet> Tweets { get; set; }
    }
}
