using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class DamageHandler : MonoBehaviour
{
    public int health;
    public ParticleSystem deathFx;
    public void Damage(int amount){
        health -= amount;

        if(health <= 0){
            Destroy(gameObject);
            if(deathFx != null) deathFx.Play();
        }
    }
}
