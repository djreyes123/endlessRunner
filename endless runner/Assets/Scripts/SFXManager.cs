using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip gameover, laserGun, UIclick;
    public AudioSource SFX;
    void OnEnable()
    {
        GameSystem.start_game += startGame;
        GameSystem.game_over += stopGame;
    }
    void OnDisable()
    {
        GameSystem.start_game -= startGame;
        GameSystem.game_over -= stopGame;
    }

    void startGame()
    {
        StartCoroutine("gameOngoing");
    }
    IEnumerator gameOngoing()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SFX.clip = laserGun;
                SFX.Play();
            }
            yield return null;
        }
    }

    void stopGame()
    {
        StopAllCoroutines();
        SFX.clip = gameover;
        SFX.Play();
    }

    public void UIpress()
    {
        SFX.clip = UIclick;
        SFX.Play();
    }
}
