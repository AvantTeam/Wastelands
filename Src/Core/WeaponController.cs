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
    bool attacking = false;
    bool swingBack = false;
    float attackAngle = 0f;
    float prevAttackAngle = 0f;
    float desiredAngle = 0f;
    float angle = 0;
    GameObject trailObject;
    ParticleSystem trailParticles;
    AudioSource swingAudioSource;
    List<GameObject> hits = new List<GameObject>();
    Vector3 prevPos;
    Vector3 prevPartPos;

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
        weapon = onPlayer ? transform.parent.gameObject.GetComponent<PlayerController>().weapon : /* When i make attacking ai, change this with the enemy controller*/null;

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

            if(!swingBack) attackAngle = 3f * (weaponRenderer.flipY ? -1 : 1);
            else attackAngle = -3f * (weaponRenderer.flipY ? -1 : 1);

            angle -= attackAngle;

            if(weaponRenderer.flipY){
                if(angle >= prevAttackAngle + weapon.swift) swingBack = true;

                if(angle <= prevAttackAngle){
                    swingBack = false;
                    attacking = false;
                }
            } else {
                if(angle <= prevAttackAngle - weapon.swift) swingBack = true;

                if(angle >= prevAttackAngle){
                    swingBack = false;
                    attacking = false;
                }
            }
        } else {
            if(hits.Count > 0) hits = new List<GameObject>();

            trailParticles.Stop();

            desiredAngle = angleToMouse(transform.parent);
            angle = moveToward(angle, desiredAngle, weapon.rotationSpeed);
            
            attackAngle = 0f;
            prevAttackAngle = angle;

            float weapAngle = transform.rotation.eulerAngles.z;
            if(weapAngle < 0) weapAngle = Mathf.Abs(weapAngle) + 180f;

            if(weapAngle >= 90 && weapAngle <= 270) weaponRenderer.flipY = true;
            else weaponRenderer.flipY = false;

            if(Input.GetMouseButtonDown(0) && Mathf.Abs(angleDist(desiredAngle, angle)) <= weapon.cone) attacking = true;
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
