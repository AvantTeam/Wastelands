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
	float moveTime = 0f;
	Vector3 playerPos;
	Vector3 origPos;
	bool attacking = false;

    void Start()
    {
    }

    void Update()
    {
		if(!attacking){
			moveTime += 1f;
			playerPos = player.position;
			origPos = transform.position;
			if(moveTime >= stopTime){
				attacking = true;
			}
		} else {
			
			transform.position = Vector3.MoveTowards(transform.position, playerPos, speed);
			if(Vector3.Distance(origPos, transform.position) >= moveLength || Vector3.Distance(playerPos, transform.position) <= 3f){
				moveTime = 0f;
				attacking = false;
			}
		}
    }
}
