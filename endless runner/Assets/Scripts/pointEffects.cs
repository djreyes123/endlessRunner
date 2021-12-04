using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointEffects : MonoBehaviour
{
    public AudioSource SFX;

    void Awake()
    {
        StartCoroutine("dissapearTimer");
    }
    IEnumerator dissapearTimer()
    {
        SFX.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
