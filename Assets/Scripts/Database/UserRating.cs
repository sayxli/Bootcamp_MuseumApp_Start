using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Table("UserRating")]
public class UserRating 
{
    [PrimaryKey]
    [AutoIncrement] //allows a unique number to be generated autmatically when a new record is inserted into the table (a primary key that is created)
    
    public int Id { get; set; }

    /// <summary>
    /// user name that rated the attraction
    /// </summary>
    public string Username { get; set; } //same as your user class Username

    /// <summary>
    /// museum attraction, or the thing the user is looking at and rating 
    /// </summary>
    public string AttractionId { get; set; } //something associated with your rating 

    /// <summary>
    /// actual user rating 
    /// </summary>
    public int Rating { get; set; } //the actual rating 
}
