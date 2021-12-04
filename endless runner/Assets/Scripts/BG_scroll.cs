using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_scroll : MonoBehaviour
{
    public float scrollspeed;
    float storescrollspeed;
    BoxCollider2D col;
    Rigidbody2D rb2D;
    float width;

    void Start()
    {
        storescrollspeed = scrollspeed;
        col = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        width = col.size.x;
    }

    void OnEnable()
    {
        GameSystem.start_game += startScroll;
        difficultyManager.raiseDifficulty += speedUp;
        //GameSystem.game_over += stopScroll;
    }
    void OnDisable()
    {
        GameSystem.start_game += startScroll;
        difficultyManager.raiseDifficulty -= speedUp;
        //GameSystem.game_over += stopScroll;
    }
    void startScroll()
    {
        scrollspeed = storescrollspeed;
        rb2D.velocity = new Vector2(scrollspeed, 0);
        StartCoroutine("scrolling");
    }

    IEnumerator scrolling()
    {
        while(true)
        {
            if(transform.position.x < -width)
            {
                Vector2 resetPosition = new Vector2(width *2f,0);
                transform.position = (Vector2)transform.position + resetPosition;
                if(gameObject.tag == "Ground")
                {
                    GameObject.FindWithTag("GameSystem").GetComponent<difficultyManager>().raisingDifficulty();
                }
            }
            yield return null;
        }
    }
    /*
    void stopScroll()
    {
        scrollspeed = 0;
        rb2D.velocity = new Vector2(scrollspeed, 0);
        StopAllCoroutines();
    }*/

    void speedUp()
    {
        scrollspeed--;
        rb2D.velocity = new Vector2(scrollspeed, 0);
    }
}
