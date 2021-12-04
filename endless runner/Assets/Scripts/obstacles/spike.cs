using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    void OnEnable()
    {
        GameSystem.start_game += clearObject;
        difficultyManager.raiseDifficulty += increaseSpeed;
    }
    void OnDisable()
    {
        GameSystem.start_game -= clearObject;
        difficultyManager.raiseDifficulty -= increaseSpeed;
    }
    void clearObject()
    {
        Destroy(gameObject);
    }
    void increaseSpeed()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(GameObject.FindWithTag("Ground").GetComponent<BG_scroll>().scrollspeed, 0);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "PC")
        {
            Debug.Log("gameover");
            GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().GameOver();
        }
    }
}
