using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class EnemyController : MonoBehaviour
{
	public Sprite sprite;
	public Transform player;
	public float speed;
	public float moveLength = 240f;
	public float stopLength = 240f;
	float movePos = 0f;
	Vector3 playerPos;
	Vector3 origPos;
	bool attacking = false;

    void Start()
    {
    }

    void Update()
    {
		if(!attacking){
			movePos += 1f;
			playerPos = player.position;
			origPos = transform.position;
			if(movePos >= moveLength) attacking = true;
		} else {
			transform.position = Vector3.MoveTowards(transform.position, playerPos, speed);
			movePos -= 1;
			if(movePos <= moveLength - stopLength){
				movePos = 0f;
				attacking = false;
			}
		}
    }
}
