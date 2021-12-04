using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner_spike : MonoBehaviour
{
    public Transform spike;
    public float cooldownMin, cooldownMax;
    float StorecooldownMin, StorecooldownMax;
    float cooldown;
    public BG_scroll ground;
    void OnEnable()
    {
        GameSystem.start_game += startSpawn;
        GameSystem.game_over += stopSpawn;
        difficultyManager.raiseDifficulty += reduceTimer;
    }
    void OnDisable()
    {
        GameSystem.start_game -= startSpawn;
        GameSystem.game_over -= stopSpawn;
        difficultyManager.raiseDifficulty -= reduceTimer;
    }
    void Start()
    {
        //keeps the initial values for reset
        StorecooldownMin = cooldownMin;
        StorecooldownMax = cooldownMax;
    }
    void startSpawn()
    {
        //set to initial value then start spawnning
        cooldownMin = StorecooldownMin;
        cooldownMax = StorecooldownMax;
        StartCoroutine("spawnning");
    }
    IEnumerator spawnning()
    {
        while(true)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                var obj = Instantiate(spike,transform.position, transform.rotation);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(ground.scrollspeed, 0);//for the spike to scroll the same speed as the ground
                cooldown = Random.Range(cooldownMin,cooldownMax);
            }
            yield return null;
        }
    }
    void stopSpawn()
    {
        StopAllCoroutines();
    }  
    void reduceTimer()
    {
        cooldownMin--;
        cooldownMax = cooldownMax - 3;
    }
}
