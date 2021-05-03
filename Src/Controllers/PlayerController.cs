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
    int dir, frame = 0;
    float tempFrame = 0f;
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

        // Thanks NiChrosia
        int num = getSprite(x, y, dir);

        if(num != -1){
            int pos = num + (frame % frames[num]) * 4;
            sprr.sprite = sprites[pos];
            dir = num;
        } else sprr.sprite = sprites[dir];

        stepp = x != 0 && y != 0;

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

    public int getSprite(float x, float y, int dir){
        if(x > 0) {
            return 0;
        } else if(x < 0)
        {
            return 1;
        } else if(y > 0)
        {
            return 2;
        } else if(y < 0)
        {
            return 3;
        }
        return -1;
    }
}