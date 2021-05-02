using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class DamageHandler : MonoBehaviour
{
    public int health;
    ParticleSystem deathParticles;
    AudioSource deathAudio;
    SpriteRenderer objectRenderer;

    void Start() {
        deathParticles = transform.Find("Death System").gameObject.GetComponent<ParticleSystem>();
        deathAudio = transform.Find("Source").gameObject.GetComponent<AudioSource>();
        objectRenderer = GetComponent<SpriteRenderer>();
    }

    public void Damage(int amount){
        health -= amount;

        if(health <= 0){
            if(deathParticles != null) deathParticles.Play();
            if(deathAudio != null) deathAudio.Play();
            objectRenderer.enabled = false;
            transform.Find("Shadow").gameObject.SetActive(false);
            
            StartCoroutine("endDeath");
        }
    }

    IEnumerator endDeath(){
        Debug.Log("Test");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
