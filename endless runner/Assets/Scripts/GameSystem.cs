using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSystem : MonoBehaviour
{
    public delegate void startGameHandler();
    public static event startGameHandler start_game;
    public delegate void pauseGameHandler(bool n);
    public static event pauseGameHandler pause_game;
    public delegate void gameOverHandler();
    public static event gameOverHandler game_over;
    public GameObject gameover_UI;
    float score, highscore;
    public TMP_Text scoreText, highScoreText;
    public BG_scroll distanceScore;

    IEnumerator trackScore()
    {
        score = 0;
        while(true)
        {
            score += Mathf.Round(-distanceScore.scrollspeed);
            scoreText.text = "Score: "+ score;
            yield return null;
        }
    }

    //game buttons
    public void startGame()
    {
        start_game?.Invoke();
        StartCoroutine("trackScore");
    }
    public void quitGame()
    {
        Application.Quit();
    }
    void pauseGame()
    {
        pause_game?.Invoke(true);
    }
    public void resumeGame()
    {
        pause_game?.Invoke(false);
    }

    //game states
    public void GameOver()
    {
        StopAllCoroutines();
        game_over?.Invoke();
        gameover_UI.SetActive(true);
        if(score > highscore)
        {
            highscore = score;
            highScoreText.text = "Highscore: " + highscore;
        }
    }
    public void AddScore(int n)
    {
        score += n;
        scoreText.text = "Score: "+ score;
    }
}
