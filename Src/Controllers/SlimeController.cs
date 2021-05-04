using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class SlimeController : MonoBehaviour
{
	public Sprite[] idle, jumping;
	public Transform player;
	public float speed;
	public float moveLength, stopTime = 240f;
	SpriteRenderer sprr;
	float moveTime = 0f;
	Vector3 playerPos, origPos;
	GameObject shadow;
	SpriteRenderer shadowRenderer;
	bool attacking = false;

    void Start()
    {
		sprr = GetComponent<SpriteRenderer>();
		sprr.sprite = idle[0];
		shadow = transform.Find("Shadow").gameObject;
		shadowRenderer = shadow.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
		/*Movement doesnt work, have to rework
		if(!attacking){
			moveTime += 1f;
			playerPos = player.position;
			origPos = transform.position;
			if(moveTime >= stopTime){
				attacking = true;
				sprr.flipX = origPos.x > playerPos.x;
				transform.position += new Vector3(0f, 0.1f, 0f);
				shadow.transform.position -= new Vector3(0.2f, 0.2f, 0f);
				sprr.sprite = jumping[0];
				shadowRenderer.sprite = jumping[1];
			}
		} else {
			Vector3 pos = Vector3.MoveTowards(transform.position, playerPos, speed);
			transform.position = new Vector3(pos.x, pos.y, 0f);
			if(Vector3.Distance(origPos, transform.position) >= moveLength || Vector3.Distance(playerPos, transform.position) <= 5f){
				moveTime = 0f;
				attacking = false;
				sprr.sprite = idle[0];
				shadowRenderer.sprite = idle[1];
				transform.position -= new Vector3(0f, 0.1f, 0f);
				shadow.transform.position += new Vector3(0.2f, 0.2f, 0f);
			}
		}*/

		transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
