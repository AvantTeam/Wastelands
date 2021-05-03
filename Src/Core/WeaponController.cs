using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;
using static Utils;

public class WeaponController : MonoBehaviour
{
    public bool onPlayer = true;
    Weapon weapon;
    SpriteRenderer weaponRenderer;
    bool attacking, swingBack = false;
    float attackAngle, prevAttackAngle, desiredAngle = 0f;
    float angle = 0;
    GameObject trailObject;
    ParticleSystem trailParticles;
    AudioSource swingAudioSource;
    List<GameObject> hits = new List<GameObject>();
    Vector3 prevPos, prevPartPos;

    void Start() {
        trailObject = transform.parent.Find("Weapon Trail").gameObject;
        prevPos = transform.position;
        prevPartPos = trailObject.transform.position;
        trailParticles = trailObject.GetComponent<ParticleSystem>();
        weaponRenderer = GetComponent<SpriteRenderer>();
        swingAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        weapon = onPlayer ? transform.parent.gameObject.GetComponent<PlayerController>().weapon : /* When I make attacking AI, change this with the enemy controller */null;

        if(attacking){
            Collider2D[] hitArr = Physics2D.OverlapBoxAll(transform.position, new Vector2(weapon.height, weapon.width), angle);
            
            foreach (Collider2D hit in hitArr)
            {
                DamageHandler outHandler;
                PlayerController outController;
                if(!swingBack && !hit.gameObject.TryGetComponent<PlayerController>(out outController) && hit.gameObject.TryGetComponent<DamageHandler>(out outHandler) && !hits.Contains(hit.gameObject))
                {
                    hits.Add(hit.gameObject);
                    outHandler.Damage(weapon.damage);
                }
            }

            if(!trailParticles.isPlaying){
                trailParticles.Play();
                swingAudioSource.clip = weapon.sfx;
                swingAudioSource.Play();
            }

            attackAngle = swingBack ? -3f * (weaponRenderer.flipY ? -1 : 1) : 3f * (weaponRenderer.flipY ? -1 : 1);

            angle -= attackAngle;

            if(weaponRenderer.flipY){
                swingBack = (angle >= prevAttackAngle + weapon.swift);
                swingBack = attacking = (angle <= prevAttackAngle)
            } else {
                swingBack = (angle <= prevAttackAngle - weapon.swift);
                swingBack = attacking = (angle >= prevAttackAngle);
            }
        } else {
            if(hits.Count > 0) hits = new List<GameObject>();

            trailParticles.Stop();

            desiredAngle = angleToMouse(transform.parent);
            prevAttackAngle = angle = moveToward(angle, desiredAngle, weapon.rotationSpeed);
            
            attackAngle = 0f;

            float weapAngle = transform.rotation.eulerAngles.z;
            weapAngle = weapAngle < 0 ? Mathf.Abs(weapAngle) + 180f : weapAngle;

            weaponRenderer.flipY = (weapAngle >= 90 && weapAngle <= 270);
            attacking = (Input.GetMouseButtonDown(0) && Mathf.Abs(angleDist(desiredAngle, angle)) <= weapon.cone);
        }

        // Rotate the weapon. Quaternions are pretty damn weird, so its (y, x, z)
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        Vector3 add = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
        Vector3 partAdd = add * (weapon.height - 0.5f);
        add *= weapon.height / 2;

        transform.localPosition = add;
        trailObject.transform.localPosition = partAdd;
    }
}