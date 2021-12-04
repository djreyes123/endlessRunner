using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner_rocket : MonoBehaviour
{
    public Transform rocket;
    public float cooldownMin, cooldownMax;
    float StorecooldownMin, StorecooldownMax;
    float cooldown;
    public float speedMin, speedMax;
    float StorespeedMin, StorespeedMax;
    public float spawnRangeMin, spawnRangeMax;
    float StorespawnRangeMin, StorespawnRangeMax;
    public float rocketSpeed;
    float StorerocketSpeed;

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
        StorespeedMin = speedMin;
        StorespeedMax = speedMax;
        StorespawnRangeMin = spawnRangeMin;
        StorespawnRangeMax = spawnRangeMax;
        StorerocketSpeed = rocketSpeed;
    }
    void startSpawn()
    {
        //set to initial value then start spawnning
        cooldownMin = StorecooldownMin;
        cooldownMax = StorecooldownMax;
        speedMin = StorespeedMin;
        speedMax = StorespeedMax;
        spawnRangeMin = StorespawnRangeMin;
        spawnRangeMax = StorespawnRangeMax;
        rocketSpeed = StorerocketSpeed;
        StartCoroutine("spawnning");
    }
    IEnumerator spawnning()
    {
        while(true)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                transform.position = new Vector2(transform.position.x, Random.Range(spawnRangeMin,spawnRangeMax));
                var obj = Instantiate(rocket,transform.position, transform.rotation);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(speedMin, speedMax), 0);
                obj.GetComponent<rocket>().trackingSpeed = rocketSpeed;
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
        speedMin--;
        speedMax = speedMax - 2;
        rocketSpeed++;
    }
}
