using UnityEngine;
using UnityEngine.UI;

namespace MuseumApp
{
    public static class StarsRatingLib
    {
        private static readonly Color publicRatingStarColor = new Color(1, 0.75f, 0);
        private static readonly Color userRatingStarColor = new Color(0.1f, 0.34f, 0.72f);
        private static readonly Color inactiveStarColor = new Color(0.78f, 0.78f, 0.78f);

        private static void SetupStars(Image[] stars, int activeStarsCount, bool isUserRating)
        {
            var activeColor = isUserRating ? userRatingStarColor : publicRatingStarColor;

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].color = i < activeStarsCount ? activeColor : inactiveStarColor;
            }
        }

        public static void SetupStars(Image[] stars, string attractionId) //we can have the same methood name if it has different params
        {
            var userRating = Database.GetUserAttractionRating(attractionId);

            if(userRating != null)
            {
                SetupStars(stars, userRating.Rating, true);
                return;
            }

            var rating = Database.GetAttractionTotalRating(attractionId);
            SetupStars(stars, rating, false); //not your user rating, its your overall userrating s its false 


        }
    }
}