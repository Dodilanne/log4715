using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    [SerializeField]
    Text pointsText;

    [SerializeField]
    Text timerText;

    [SerializeField]
    Text scoreText;

    private int score = ScoreControler.score;
    private float elapsedTime = ScoreControler.elapsedTime;
    private int finalScore = ScoreControler.score;
    private string timerScore;
    // Start is called before the first frame update
    void Start()
    {
        ScoreControler.gameOver = true;
        calculateScore();
        pointsText.text = "POINTS: " + score;
        timerText.text = "TIME: " + elapsedTime.ToString("f2") + timerScore;
        scoreText.text = "SCORE: " + finalScore;
    }

    void calculateScore() {
        if(elapsedTime <= 10) {
            finalScore+= 50;
            timerScore = " (+50 points)";
        }
        else if( elapsedTime > 10 && elapsedTime <= 20) {
            finalScore+= 30;
            timerScore = " (+30 points)";
        }
        else if(elapsedTime > 20 && elapsedTime <= 30) {
            finalScore+= 10;
            timerScore = " (+10 points)";
        }
        else {
            timerScore = " (+0 points)";
        }
    }

    private void OnGUI() {

  }

}
