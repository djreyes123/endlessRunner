using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class difficultyManager : MonoBehaviour
{
    public delegate void raisedifficultyHandler();
    public static event raisedifficultyHandler raiseDifficulty;
    public TMP_Text difficultyText;
    int count;
    bool gameover;
    void OnEnable()
    {
        GameSystem.start_game += resetDifficulty;
        GameSystem.game_over += stopDifficulty;
    }
    void OnDisable()
    {
        GameSystem.start_game -= resetDifficulty;
        GameSystem.game_over -= stopDifficulty;
    }
    void resetDifficulty()
    {
        count = 0;
        gameover = false;
        difficultyText.text = "DIFFICULTY: EASY";
    }
    void stopDifficulty()
    {
        gameover = true;
    }
    public void raisingDifficulty()
    {
        if(!gameover)
        {
            count++;
            switch(count)
            {
                case(2):
                raiseDifficulty?.Invoke();
                difficultyText.text = "DIFFICULTY: NORMAL";
                break;
                case(4):
                raiseDifficulty?.Invoke();
                difficultyText.text = "DIFFICULTY: MEDIUM";
                break;
                case(8):
                raiseDifficulty?.Invoke();
                difficultyText.text = "DIFFICULTY: HARD";
                break;
                default:
                break;
            }
        }
        
    }
}
