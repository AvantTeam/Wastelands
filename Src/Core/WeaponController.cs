using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class WeaponController : MonoBehaviour
{
    public bool onPlayer = true;
    Weapon weapon;

    SpriteRenderer weaponRenderer;

    bool attacking = false;

    bool swiftBack = false;

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

        if(!attacking){
            if(hits.ToArray().Length > 0) hits = new List<GameObject>();

            trailParticles.Stop();

            Vector3 mouse_pos = Input.mousePosition;
            mouse_pos.z = 3f;
            Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.parent.position);
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            
            desiredAngle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            angle = moveToward(angle, desiredAngle, weapon.rotationSpeed);
            
            attackAngle = 0f;
            prevAttackAngle = angle;

            float weapAngle = transform.rotation.eulerAngles.z;
            if(weapAngle < 0) weapAngle = Mathf.Abs(weapAngle) + 180f;

            if(weapAngle >= 90 && weapAngle <= 270) weaponRenderer.flipY = true;
            else weaponRenderer.flipY = false;

            if(Input.GetMouseButtonDown(0) && Mathf.Abs(angleDist(desiredAngle, angle)) <= weapon.cone) attacking = true;
        } else {
            Collider2D[] hitArr = Physics2D.OverlapBoxAll(transform.position, new Vector2(weapon.height, weapon.width), angle);
            
            foreach (Collider2D hit in hitArr)
            {
                DamageHandler outHandler;
                PlayerController outController;
                if(!hit.gameObject.TryGetComponent<PlayerController>(out outController) && hit.gameObject.TryGetComponent<DamageHandler>(out outHandler) && !hits.Contains(hit.gameObject))
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

            if(!swiftBack) attackAngle = 3f * (weaponRenderer.flipY ? -1 : 1);
            else attackAngle = -3f * (weaponRenderer.flipY ? -1 : 1);

            angle -= attackAngle;

            if(weaponRenderer.flipY){
                if(angle >= prevAttackAngle + weapon.swift) swiftBack = true;

                if(angle <= prevAttackAngle){
                    swiftBack = false;
                    attacking = false;
                }
            } else {
                if(angle <= prevAttackAngle - weapon.swift) swiftBack = true;

                if(angle >= prevAttackAngle){
                    swiftBack = false;
                    attacking = false;
                }
            }
        }

        // Rotate the weapon. Quaternions are pretty damn weird, so its (y, x, z)
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        Vector3 add = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
        Vector3 partAdd = add * (weapon.height - 0.5f);
        add = add * (weapon.height / 2);

        Vector3 parentPos = transform.parent.position;
        Vector3 parentAdd = new Vector3(parentPos.x, parentPos.y, 0f);

        transform.position = prevPos + add + parentAdd;
        trailObject.transform.position = prevPartPos + partAdd + parentAdd;

        prevPos = transform.position - add - parentAdd;
        prevPartPos = trailObject.transform.position - partAdd - parentAdd;
    }

    // Edits slowly an angle towards a position.
    public static float moveToward(float angle, float to, float speed){
        if(Mathf.Abs(angleDist(angle, to)) < speed) return to;
        angle = angle % 360f;
        to = to % 360f;

        if(angle > to == backwardDistance(angle, to) > forwardDistance(angle, to)){
            angle -= speed;
        }else{
            angle += speed;
        }

        return angle;
    }

    public static float angleDist(float a, float b){
        return Mathf.Min((a - b) < 0 ? a - b + 360 : a - b, (b - a) < 0 ? b - a + 360 : b - a);
    }

    public static float forwardDistance(float angle1, float angle2){
        return Mathf.Abs(angle1 - angle2);
    }

    public static float backwardDistance(float angle1, float angle2){
        return 360 - Mathf.Abs(angle1 - angle2);
    }
}
