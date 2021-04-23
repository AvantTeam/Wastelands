using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class SlimeController : MonoBehaviour
{
	public Sprite sprite;
	public Transform player;
	public float speed;
	public float moveLength = 240f;
	public float stopTime = 240f;
	Rigidbody2D rb;
	float moveTime = 0f;
	Vector3 playerPos;
	Vector3 origPos;
	bool attacking = false;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		if(!attacking){
			moveTime += 1f;
			playerPos = player.position;
			origPos = rb.position;
			if(moveTime >= stopTime){
				attacking = true;
			}
		} else {
			
			rb.position = Vector3.MoveTowards(rb.position, playerPos, speed);
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			if(Vector3.Distance(origPos, rb.position) >= moveLength || Vector3.Distance(playerPos, rb.position) <= 3f){
				moveTime = 0f;
				attacking = false;
			}
		}
    }
}
