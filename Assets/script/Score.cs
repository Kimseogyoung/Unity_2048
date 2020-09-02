using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalscoreText;
    public TextMeshProUGUI endingscoreText;
    public int score=0;
    
    public void SetScoreZero() { score = 0; UpdateScore(0); }
    public void UpdateScore(int num)
    {
        score += num;
        scoreText.text = ""+score+"";
        finalscoreText.text = "" + score + "";
        endingscoreText.text = "" + score + "";
    }
}
