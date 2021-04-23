using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class EnemyController : MonoBehaviour
{
	public Sprite sprite;
	public Transform player;
	public float speed;

    void Start()
    {
    }

    void Update()
    {
		transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
    }
}
