using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class SlimeController : MonoBehaviour
{
	public Transform player;
	public float speed;
	public float moveLength = 240f;
	public float stopTime = 240f;
	SpriteRenderer sprr;
	Rigidbody2D rb;
	float moveTime = 0f;
	Vector3 playerPos;
	Vector3 origPos;
	bool attacking = false;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		sprr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
		if(!attacking){
			moveTime += 1f;
			playerPos = player.position;
			origPos = rb.position;
			if(moveTime >= stopTime){
				attacking = true;
				sprr.flipX = origPos.x > playerPos.x;
			}
		} else {
			rb.position = Vector3.MoveTowards(rb.position, playerPos, speed);
			if(Vector3.Distance(origPos, rb.position) >= moveLength || Vector3.Distance(playerPos, rb.position) <= 3f){
				moveTime = 0f;
				attacking = false;
			}
		}

		transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
