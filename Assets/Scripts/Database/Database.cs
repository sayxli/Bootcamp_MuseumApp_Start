using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Linq;

namespace MuseumApp
{
    public static class Database
    {
        private static string databasePath = "GameDatabase.db";

        //we need to create an object for the sqlite connection to this database
        private static readonly SQLiteConnection connection;

        static Database() //we are creating a new connection with a database pack 
        {
            connection = new SQLiteConnection(databasePath);

            connection.CreateTable<User>();
            connection.CreateTable<UserRating>();
        }

        //we need a way to create and retrieve users for this database 
        //this method will be used to create a new user in the database 
        public static void RegisterPlayer(string username, string password)
        {
            connection.Insert(new User
            {
                Username = username,
                Password = password,
            });
        }

        //we need a way to retreive a user from the database
        public static User GetUser(string username)
        {
            try
            {
                //has any code that might cause an exception
                //this little block is executed until an exception is thrown or 
                // its completely 100% successful
                return connection.Get<User>(username);

            }
            catch
            {
                // "Catch" any type of exception 
                return null;
            }
            //try catch statement is excceptionn handling which we need o handle any sort of outlier cases or null variables etc
            //if no catch block is found,  your language runtime thing displayes an OnhandleException message to the user and stops executing the program
        }

        //method that gets the user attraction rating 
        public static UserRating GetUserAttractionRating(string attractionId)
        {
            if (!User.IsLoggedIn)
            {
                return null;
            }

            var username = User.LoggedInUsername;
            //@ = string interpolation = it interpolates a string from a variable given in curly brackets  // $ takes the string for verbatum (using exactly the same words as what was used originally)
            var results = connection.Query<UserRating>(
                $@"SELECT * FROM {nameof(UserRating)} WHERE 
                {nameof(UserRating.AttractionId)} = '{attractionId}' AND 
                {nameof(UserRating.Username)} = '{username}'");
            //SELECT everything FROM table_name WHERE userrating.attractionid = the attraction id that we are passing in the mathod
            //and the userratigng.username is the same as the username that is logged in (at the var above)

            Debug.Assert(results.Count <= 1, $"{username} has multiple ratings for the same attraction"); //logged at some sort of failure

            return results.Count == 1 ? results[0] : null; //spit out the first result (0) if the results count = 1. and if it doesnt = 1, null.
        }

        /// <summary>
        ///  we want to get a total rating of the attraction from all users
        ///  Gives every record on the database for the UserRating table 
        ///  Using Linq to manipulate the list 
        /// </summary>
        /// <param name="attractionId"></param>
        /// <returns></returns>
        public static int GetAttractionTotalRating(string attractionId)
        {
            //var ratings = connection.Table<UserRating>().Where(userRating => userRating.AttractionId == attractionId);
            var ratings = (from userRating in connection.Table<UserRating>() where userRating.AttractionId == attractionId select userRating); //does the same thing as the line commented out above 

            return ratings.Any() ? ratings.Sum(userRating => userRating.Rating) / ratings.Count() : 0;
        }

        public static void Rate(string attractionId, int rating)
        {
            var userRating = GetUserAttractionRating(attractionId);

            //if we have a user rating, rewrite in the database
            if (userRating != null)
            {
                userRating.Rating = rating;
                connection.Update(userRating);
                return;
            }

            //if we dont have a rating, we'll inset into the database
            connection.Insert(new UserRating
            {
                AttractionId = attractionId,
                Username = User.LoggedInUsername,
                Rating = rating,
            });
        }

        public static void ClearDatabase()
        {
            connection.DeleteAll<User>();
            connection.DeleteAll<UserRating>();
        }

    }
}
