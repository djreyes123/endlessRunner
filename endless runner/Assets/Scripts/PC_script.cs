using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_script : MonoBehaviour
{

    Animator anim;
    Rigidbody2D rb2D;
    public Vector2 spawnPoint;
    public float maxSpeed;
    public float walkSpeed;
    public float jumpforce;
    public float hoverforce;
    bool grounded;
    public AudioSource jumpSFX;
    public AudioClip jump, hover;

    void OnEnable()
    {
        GameSystem.start_game += spawnPC;
        GameSystem.game_over += died;
    }
    void OnDisable()
    {
        GameSystem.start_game -= spawnPC;
        GameSystem.game_over -= died;
    }

    void spawnPC()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
        jumpSFX.enabled = true;
        transform.position = spawnPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") || Input.GetKey("w"))
        {
            if(grounded)
            {
                rb2D.velocity = new Vector2(0, jumpforce);
                jumpSFX.clip = jump;
                jumpSFX.loop = false;
                jumpSFX.Play();
            }
            grounded = false;
            anim.SetBool("grounded", grounded);
        }
        if(Input.GetKeyUp("space")||Input.GetKeyUp("w"))
        {
            jumpSFX.Stop();
        }
    }
    void FixedUpdate()
    {
        rb2D.velocity = new Vector2(Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb2D.velocity.y, -maxSpeed, maxSpeed));//limits the velocity from input, preventing super speed movement
        if(Input.GetKey("space") || Input.GetKey("w") && !grounded)
        {
            rb2D.AddForce(transform.up * hoverforce);
            if(jumpSFX.isPlaying == false)
            {
                jumpSFX.clip = hover;
                jumpSFX.loop = true;
                jumpSFX.Play();
            }
            
        }
        if(Input.GetKey("a"))
        {
            rb2D.AddForce(-transform.right * walkSpeed, ForceMode2D.Impulse);
        }
        if(Input.GetKey("d"))
        {
            rb2D.AddForce(transform.right * walkSpeed, ForceMode2D.Impulse);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
            anim.SetBool("grounded", grounded);
        }
    }
    void OnMouseDown()
    {
        GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().GameOver();
    }
    public void died()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        jumpSFX.enabled = false;
        rb2D.velocity = new Vector2(0,0);
    }
}

