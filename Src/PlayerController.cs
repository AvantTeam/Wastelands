using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class PlayerController : MonoBehaviour
{
    public int[] frames;
    public AudioClip step;
    AudioSource source;
    Sprite[] sprites;
    GameObject weaponObject;
    SpriteRenderer weaponRenderer;
    public Weapon weapon;
    int dir = 0;
    int frame = 0;
    float tempFrame = 0;
    Rigidbody2D rb;
    SpriteRenderer sprr;
    float x, y;
    bool stepp = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprr = GetComponent<SpriteRenderer>();
        weaponObject = transform.Find("Weapon").gameObject;
        weaponRenderer = weaponObject.GetComponent<SpriteRenderer>();
        weapon = Weapons.axe;
        BoxCollider2D wBox = weaponObject.GetComponent<BoxCollider2D>();
        wBox.size = new Vector2(weapon.height, weapon.width);
        sprites = Resources.LoadAll<Sprite>("Sprites/Player/player");
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(weapon == null){
            weapon = Weapons.axe;
        } else {
            weaponRenderer.sprite = weapon.sprite;
        }
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if(x > 0)
        {
            int pos = (frame % frames[0]) * 4;
            sprr.sprite = sprites[pos];
            dir = 0;
            stepp = true;
        } else if(x < 0)
        {
            int pos = 1 + (frame % frames[1]) * 4;
            sprr.sprite = sprites[pos];
            dir = 1;
            stepp = true;
        } else if(y > 0)
        {
            int pos = 2 + (frame % frames[2]) * 4;
            sprr.sprite = sprites[pos];
            dir = 2;
            stepp = true;
        } else if(y < 0)
        {
            int pos = 3 + (frame % frames[3]) * 4;
            sprr.sprite = sprites[pos];
            dir = 3;
            stepp = true;
        } else {
            sprr.sprite = sprites[dir];
        }

        rb.position += new Vector2(x, y) * 0.03f * weapon.speedMultiplier;
        tempFrame += 0.05f;
        frame = (int) Mathf.Round(tempFrame);

        if(!stepp) source.Stop();

        if(stepp && !source.isPlaying){
            source.clip = step;
            source.Play();
        }

        stepp = false;
    }
}
