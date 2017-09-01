using UnityEngine;
using System.Collections;

public class share : MonoBehaviour 
{
	/* TWITTER VARIABLES*/

	//Twitter Share Link
	string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";

	//Language
	string TWEET_LANGUAGE = "en";

	//This is the text which you want to show
	string textToDisplay="Hey Guys! Check out my score: ";

	/*END OF TWITTER VARIABLES*/

	/* FACEBOOK VARAIBLES */

	//App ID
	string AppID = "636178773254709";

	//This link is attached to this post
	string Link = "https://google.com";

	//The URL of a picture attached to this post. The Size must be atleat 200px by 200px.
	string Picture = "https://k61.kn3.net/E/B/1/0/3/D/87F.png";

	//The Caption of the link appears beneath the link name
	string Caption = "Check out My New Score: ";

	//The Description of the link
	string Description = "Enjoy Fun, free games! Challenge yourself or share with friends. Fun and easy to use games.";

    /* END OF FACEBOOK VARIABLES */

    int HighScore;

	// Twitter Share Button	
	public void shareScoreOnTwitter () 
	{
        HighScore = PlayerPrefs.GetInt("BestScore");

        Application.OpenURL (TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(textToDisplay) + HighScore + "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
	}
	
	// Facebook Share Button
	public void shareScoreOnFacebook ()
	{
        HighScore = PlayerPrefs.GetInt("BestScore");
        Application.OpenURL("https://www.facebook.com/dialog/feed?" + "app_id=" + AppID + "&link=" + Link + "&picture=" + Picture
                              + "&caption=" + Caption + HighScore + "&description=" + Description);
    }
}
