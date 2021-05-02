using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class DamageHandler : MonoBehaviour
{
    public int health;
    public AudioClip hit;
    public AudioClip death;
    ParticleSystem deathParticles;
    AudioSource audio;
    SpriteRenderer objectRenderer;
    SpriteRenderer mask;
    bool receivingDamage = true;

    void Start() {
        deathParticles = transform.Find("Death System").gameObject.GetComponent<ParticleSystem>();
        audio = transform.Find("Source").gameObject.GetComponent<AudioSource>();
        objectRenderer = GetComponent<SpriteRenderer>();
        mask = transform.Find("Sprite Mask").gameObject.GetComponent<SpriteRenderer>();
    }

    public void Damage(int amount){
        health -= amount;
        if(audio != null)
        {
            audio.clip = hit;
            audio.Play();
        }

        StartCoroutine("DrawDamage");

        if(health <= 0 && receivingDamage){
            if(deathParticles != null) deathParticles.Play();
            if(audio != null)
            {
                audio.clip = death;
                audio.Play();
            }

            objectRenderer.enabled = false;
            mask.enabled = false;
            receivingDamage = false;
            transform.Find("Shadow").gameObject.SetActive(false);
            
            StartCoroutine("EndDeath");
        }
    }

    IEnumerator EndDeath(){
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator DrawDamage(){
        mask.enabled = true;
        yield return new WaitForSeconds(0.2f);
        mask.enabled = false;
    }
}
