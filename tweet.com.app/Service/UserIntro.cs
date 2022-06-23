using com.tweetapp.DAO;
using com.tweetapp.Models;
using com.tweetapp.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace com.tweetapp.Service
{
    class UserIntro : IUserIntro
    {
        private readonly IRepository repository;
        private string currentUser;
        public UserIntro(IRepository repo)
        {
            this.repository = repo;
        }
        public void WelcomeConsole()
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("TWEETAPP");
            Console.WriteLine("**********************************");
        }
        public void IntroPageNonLoggedUser()

        {
            WelcomeConsole();

            Console.WriteLine(string.Format("{0,-10}|{1,10}", "1.Create Account  ", "2. Login"));
            Console.WriteLine(string.Format("{0,-10}|{1,10}", "3.Forget Password  ", "4. Rest Password"));
            Console.Write("\nProvide your response (1/2/3/4) : ");
            var response = Console.ReadLine();
            if (response == "1")
            {
                if (UserRegistration())
                {
                    Console.WriteLine("\n** Registration Successfull**");
                    Dashboard();
                }
                else
                    Console.WriteLine("\n**Registration Failed. Please Try again**");
            }

            else if (response == "2")
            {
                while (!UserLogin())
                {
                    Console.WriteLine("**Login Failed. Check Credentials**");
                }
                Console.WriteLine("\n**Login Successfull**");
                Dashboard();


            }
            else if (response == "3")
            {
                ForgetPassword();
            }
            else if (response == "4")
                ResetPassword();
            else
                Console.WriteLine("\n**Invalid Response**");



        }

        public bool UserRegistration()
        {
            WelcomeConsole();
            Console.WriteLine("**Enter Your Details**");
            UserInfo registrationData = new UserInfo();
            var reponse = string.Empty;
            while (reponse == string.Empty)
            {
                Console.Write(string.Format("{0,-10}", "First Name : "));
                reponse = Console.ReadLine();
                if (reponse == string.Empty)
                    Console.WriteLine("** First Name is Mandatory**");
                else
                    registrationData.FirstName = reponse;
            }

            Console.Write(string.Format("{0,-10}", "Last Name : "));
            reponse = Console.ReadLine();
            registrationData.LastName = reponse;

            reponse = string.Empty;
            while (reponse == string.Empty)
            {
                Console.Write(string.Format("{0,-10}", "Gender : 1-Male  2- Female  3-Others"));

                reponse = Console.ReadLine();
                if (reponse == string.Empty)
                    Console.WriteLine("** Gender is Mandatory**");
                else
                {
                    switch (reponse)
                    {
                        case "1":
                            registrationData.Gender = "Male";
                            break;
                        case "2":
                            registrationData.Gender = "Female";
                            break;
                        case "3":
                            registrationData.Gender = "Others";
                            break;
                        default:
                            Console.WriteLine("**Invalid Response**");
                            reponse = string.Empty;
                            break;

                    }
                }
            }

            reponse = string.Empty;
            while (reponse == string.Empty)
            {
                Console.Write(string.Format("{0,-10}", "DOB(dd-MM-yyyy) : "));
                reponse = Console.ReadLine();
                DateTime dateTime = new DateTime(); ;
                bool validDate = DateTime.TryParseExact(reponse, "dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
                if (validDate)
                    registrationData.Dob = dateTime;
                else
                {
                    reponse = string.Empty;
                    Console.WriteLine("**Invalid Date format**");
                }
            }



            reponse = string.Empty;
            while (reponse == string.Empty)
            {
                Console.Write(string.Format("{0,-10}", "Email Id : "));
                reponse = Console.ReadLine();
                if (reponse == string.Empty)
                    Console.WriteLine("** EmailID is Mandatory**");
                else
                {
                    //chk for unique ness
                    //chk regrex
                    registrationData.EmailId = reponse;
                }
            }
            reponse = string.Empty;
            while (reponse == string.Empty)
            {
                Console.Write(string.Format("{0,-10}", "Password : "));
                //** pasword
                reponse = Console.ReadLine();
                if (reponse == string.Empty)
                    Console.WriteLine("**Password is Mandatory**");
                else
                    registrationData.Passcode = reponse;

                reponse = string.Empty;
                Console.Write(string.Format("{0,-10}", "Confirm Password : "));
                reponse = Console.ReadLine();
                if (reponse == string.Empty)
                    Console.WriteLine("** Please confirm your password**");
                else
                {
                    if (registrationData.Passcode == reponse)
                        Console.WriteLine("**Password Matches**");
                    else
                    {
                        Console.WriteLine("**Password Mismath**");
                        reponse = string.Empty;
                    }
                }
            }
            currentUser = registrationData.EmailId;
            return this.repository.UserRegistration(registrationData);
        }


        public bool UserLogin()
        {

            Console.Write("Enter username  :");
            var user = Console.ReadLine();
            Console.Write("\nEnter Password  :");
            var pass = Console.ReadLine();
            currentUser = user;
            return this.repository.UserLogin(user, pass);

        }
        public void Dashboard()
        {
            WelcomeConsole();
            Console.WriteLine(string.Format("{0,-10}|{1,10}|{2,-10}", "1.Post Tweet  ", "2. View All My Tweet", "3. View All Public Tweets"));
            Console.WriteLine(string.Format("{0,-10}|{1,10}", "4.LogOut  ", "5. Registration Page"));

            Console.Write("Provide your response (1/2/3/4/5): ");
            var response = Console.ReadLine();
            switch (response)
            {
                case "1":
                    {
                        PostTweet();

                        Dashboard();
                        break;
                    }
                case "2":
                    {
                        GetMyTweets();
                        Console.Write("\nPress any key to load dashboard");
                        Dashboard();
                        break;
                    }
                case "3":
                    {
                        GetAllTweets();
                        Console.Write("\nPress any key to load dashboard");
                        Dashboard();
                        break;
                    }
                case "4":
                    IntroPageNonLoggedUser();
                    break;
                case "5":
                    UserRegistration();
                    break;
                default:
                    Console.WriteLine("**Invalid Input**");
                    break;


                    
            }

        }
        public void PostTweet()
        {
            WelcomeConsole();
            Console.WriteLine("Logged in as:  " + currentUser);
            Tweet tweet = new Tweet();
            Console.WriteLine("POST YOUR TWEET");
            Console.Write(string.Format("{1,-10}", "Type Your Tweet:", ":-"));
            tweet.TweetMessage = Console.ReadLine();
            if (tweet.TweetMessage == string.Empty)
            {
                Console.WriteLine("\n**Nothing to Post**");
                PostTweet();
            }

            tweet.TweetTime = DateTime.Now;
            tweet.EmailId = currentUser;
            this.repository.PostTweet(tweet);
            Console.WriteLine("\nTweet Succesfully Posted");
        }
        public void GetMyTweets()
        {
            WelcomeConsole();
            Console.WriteLine("MY TWEETS");
            var tweet = this.repository.GetTweetById(currentUser);
            foreach (Tweet tweets in tweet)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.Write( "    Tweet By:" + tweets.EmailId);
                Console.WriteLine( "    Tweet At:" + tweets.TweetTime);
                Console.WriteLine("\t"+tweets.TweetMessage);
            }


        }
        public void GetAllTweets()
        {
            WelcomeConsole();
            Console.WriteLine("ALL PUBLIC TWEETS");
            var tweet = this.repository.GetAllTweet();
            foreach (Tweet tweets in tweet)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.Write("    Tweet By:" + tweets.EmailId);
                Console.WriteLine("    Tweet At:" + tweets.TweetTime);
                Console.WriteLine("\t" + tweets.TweetMessage);
            }


        }
        public void ForgetPassword()
        {
            WelcomeConsole();
            Console.WriteLine("**FORGET PASSWORD: Use DOB info to reset your password**");
            Console.Write("Enter username  :");
            var user = Console.ReadLine();
            Console.Write("\nEnter Registered DOB(dd-MM-yyyy)  :");
            var DOB = Console.ReadLine();
            DateTime dateTime = new DateTime();
            bool validDate = DateTime.TryParseExact(DOB, "dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
            if (validDate)
            {
                Console.Write("\nEnter new Password  :");
                var pass = Console.ReadLine();
                if (this.repository.ForgetPassword(user, dateTime, pass))
                {
                    Console.WriteLine("\n**Password Updated Successfully**\n");
                    IntroPageNonLoggedUser();

                }
                else
                {
                    Console.WriteLine("** Username or DOB Failed to validate: Try again**");
                    IntroPageNonLoggedUser();

                }
                }
                else
            {
                Console.WriteLine("**Invalid Date format**");
                IntroPageNonLoggedUser();
            }
           
        }
        public void ResetPassword()
        {
            WelcomeConsole();
            Console.WriteLine("**RESET PASSWORD **\n");
            Console.Write("Enter username  :");
            var user = Console.ReadLine();
            Console.Write("\nEnter Old passsword  :");
            var oldpass = Console.ReadLine();
            Console.Write("\nEnter New passsword  :");
            var pass = Console.ReadLine();
            if(this.repository.ResetPassword(user,oldpass,pass))
            {

                Console.WriteLine("\n**Password Reset Successfully**\n");

            }
            else
                Console.WriteLine("**\n Username or Old Password Failed to validate: Try again**");

            IntroPageNonLoggedUser();

        }
    }
}

