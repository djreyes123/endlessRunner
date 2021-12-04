using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    GameObject PC;
    float PC_position;
    public float trackingSpeed;
    int durability = 1;
    public Transform point100;

    void OnEnable()
    {
        GameSystem.start_game += clearObject;
        PC = GameObject.FindWithTag("PC");
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
    void Update()
    {
        if(transform.position.y > PC.transform.position.y)
        {
            transform.position = new Vector2(transform.position.x,transform.position.y-trackingSpeed*Time.deltaTime);
        }
        if(transform.position.y < PC.transform.position.y)
        {
            transform.position = new Vector2(transform.position.x,transform.position.y+trackingSpeed*Time.deltaTime);
        }
    }
    void OnMouseDown()
    {
        --durability;
        if(durability == 0)
        {
            Destroy(gameObject);
            GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().AddScore(100);
            Instantiate(point100, transform.position,transform.rotation);
        }
    }
}
