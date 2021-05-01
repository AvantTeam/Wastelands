using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class WeaponController : MonoBehaviour
{
    public bool onPlayer = true;
    // The weapon associated with the parent, dont edit, edit the parent controller's weapon.
    Weapon weapon;

    // The weapon renderer, used for flipping.
    SpriteRenderer sprr;

    // Wether or not the weapon is attacking.
    bool attacking;

    // Wether or not the weapon is returning after an attack, doesn't imply it goes to the left, i just suck at naming.
    bool swiftLeft = false;

    // The angle add for attacking, and a calculation for the attack return, dont edit any of these manually
    float atAng = 0f;
    float prevAng = 0f;

    // The desired angle of the weapon, edit this to change the weapon's angle.
    float desiredAngle = 0f;

    // The angle of the weapon, dont edit this manually.
    float angle = 0;

    // A ParticleSystem GameObject. Has the same parent as the weapon object.
    GameObject trail;

    // The ParticleSystem associated with the trail.
    ParticleSystem particles;

    //An AudioSource, object inside the weapon object.
    AudioSource source;
    List<GameObject> hits = new List<GameObject>();

    // Previous positions of each vector, used for calculations, dont edit.
    Vector3 prevPos;
    Vector3 prevPartPos;

    void Start() {
        // Setting up the variables.
        trail = transform.parent.Find("Weapon Trail").gameObject;
        prevPos = transform.position;
        prevPartPos = trail.transform.position;
        particles = trail.GetComponent<ParticleSystem>();
        sprr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        weapon = onPlayer ? transform.parent.gameObject.GetComponent<PlayerController>().weapon : /* When i make attacking ai, change this with the enemy controller*/null;

        if(!attacking){
            if(hits.ToArray().Length > 0) hits = new List<GameObject>();

            particles.Stop();

            Vector3 mouse_pos = Input.mousePosition;
            mouse_pos.z = 3f;
            Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.parent.position);
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            
            desiredAngle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            angle = moveToward(angle, desiredAngle, weapon.rotationSpeed);
            
            atAng = 0f;
            prevAng = angle;

            float weapAngle = transform.rotation.eulerAngles.z;
            if(weapAngle < 0) weapAngle = Mathf.Abs(weapAngle) + 180f;

            if(weapAngle >= 90 && weapAngle <= 270) sprr.flipY = true;
            else sprr.flipY = false;

            if(Input.GetMouseButtonDown(0) && Mathf.Abs(angleDist(desiredAngle, angle)) <= weapon.cone) attacking = true;
        } else {
            Collider2D[] hitArr = Physics2D.OverlapBoxAll(transform.position, new Vector2(weapon.height, weapon.width), angle);
            
            foreach (Collider2D hit in hitArr)
            {
                DamageHandler outHandler;
                if(hit.gameObject.TryGetComponent<DamageHandler>(out outHandler) && !hits.Contains(hit.gameObject))
                {
                    hits.Add(hit.gameObject);
                    outHandler.Damage(weapon.damage);
                }
            }

            if(!particles.isPlaying){
                particles.Play();
                source.clip = weapon.sfx;
                source.Play();
            }

            if(!swiftLeft) atAng = 3f * (sprr.flipY ? -1 : 1);
            else atAng = -3f * (sprr.flipY ? -1 : 1);

            angle -= atAng;

            if(sprr.flipY){
                if(angle >= prevAng + weapon.swift) swiftLeft = true;

                if(angle <= prevAng){
                    swiftLeft = false;
                    attacking = false;
                }
            } else {
                if(angle <= prevAng - weapon.swift) swiftLeft = true;

                if(angle >= prevAng){
                    swiftLeft = false;
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
        trail.transform.position = prevPartPos + partAdd + parentAdd;

        prevPos = transform.position - add - parentAdd;
        prevPartPos = trail.transform.position - partAdd - parentAdd;
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

    //Methods used for moveToward.
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
