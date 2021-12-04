using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    int durability = 3;
    public Transform point300;
    
    void OnEnable()
    {
        GameSystem.start_game += clearObject;
    }
    void OnDisable()
    {
        GameSystem.start_game -= clearObject;
    }
    void clearObject()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "PC")
        {
            Debug.Log("gameover");
            GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().GameOver();
            Destroy(gameObject);
        }
    }
    void OnMouseDown()
    {
        --durability;
        if(durability == 0)
        {
            Destroy(gameObject);
            GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().AddScore(300);
            Instantiate(point300, transform.position,transform.rotation);
        }
        else{
            StartCoroutine("hitShake");
        }
    }
    IEnumerator hitShake()
    {
        transform.position = new Vector2(transform.position.x+0.1f,transform.position.y+0.1f);
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector2(transform.position.x-0.2f,transform.position.y-0.2f);
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector2(transform.position.x+0.1f,transform.position.y+0.1f);
        yield return null;
    }
}
