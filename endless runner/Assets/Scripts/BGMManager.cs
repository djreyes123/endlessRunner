using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource BGM;
    void OnEnable()
    {
        GameSystem.start_game += playMusic;
        GameSystem.game_over += stopMusic;
    }
    void OnDisable()
    {
        GameSystem.start_game -= playMusic;
        GameSystem.game_over -= stopMusic;
    }

    void playMusic()
    {
        Debug.Log("play");
        BGM.Play();
    }
    void stopMusic()
    {
        BGM.Stop();
    }
}
