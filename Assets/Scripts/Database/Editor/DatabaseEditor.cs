using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class DatabaseEditor 
{
   [MenuItem("Database/Clear Database")]

   private static void ClearDatabase()
    {
        MuseumApp.Database.ClearDatabase();
    }
}
