using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Table("User")]
public class User //this class is keeping tarcck of who is logged in. it is retrieving the logged in username from the playerprefs file 
{
    [PrimaryKey] //single field or combo of fields that uniquely defines some sort of record 
    public string Username { get; set; } //username record is a primary key that will have a unique value 

    /// <summary>
    /// DONT STORE PASSWORDS LIKE THIS, they will be very vulnerable
    /// THIS IS JUST an EXAMPLE
    /// </summary>
    public string Password { get; set; }

    public static string loggedUserSaveKey = "loggedUserSaveKey";

    public static string LoggedInUsername => PlayerPrefs.GetString(loggedUserSaveKey, string.Empty); // we have to state the condition of this loggedusername string //the default value is empty

    public static bool IsLoggedIn => !LoggedInUsername.Equals(string.Empty);  //if your user has logged in and their logged username is not empty

    public static void Login(string username) => PlayerPrefs.SetString(loggedUserSaveKey, username); //takes in your string username. going to the playerprefs file, setting the string in your playerprefs to be associated with some sort of key and your usernamestring (and that string is what we're retreiving in loggedinusername above^

    public static void LogOff() => PlayerPrefs.DeleteKey(loggedUserSaveKey); //we're deleting that key here
}
