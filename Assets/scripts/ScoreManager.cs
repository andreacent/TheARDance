using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour{

    public static int score;   // The player's score.
    Text score_text; // Reference to the Text component.

    void Awake (){
        // Set up the reference.
		score_text = GetComponent <Text> ();

        // Reset the score.
        score = 0;
    }

    void Update (){
        // Set the displayed text to be the word "Score" followed by the score value.
        score_text.text = "SCORE: " + score;
    }
}