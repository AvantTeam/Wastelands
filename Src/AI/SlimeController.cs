using System.Collections;
using UnityEngine;
using static Utils;

public class SlimeController : MonoBehaviour
{
	public Sprite[] idle, jumping;
	public Transform player;
	public float speed;
	public float moveRadius = 2f;
	public float stopTime = 1f;
	public float colliderRadius = 0.5f;
	SpriteRenderer spriteRenderer;
	float moveTime = 0f;
	Vector3 newPos, prevPos;
	GameObject shadow;
	SpriteRenderer shadowRenderer;
	bool moving = false;
	BoxCollider2D boxCollider;
	float frame = 0f;

	void Start()
	{
		prevPos = transform.position;
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = idle[0];
		shadow = transform.Find("Shadow").gameObject;
		shadowRenderer = shadow.GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	void Update()
	{
		if (moving)
		{
			if (transform.position == newPos)
			{
				prevPos = transform.position;
				if (frame >= stopTime)
				{
					moving = false;
				}
				frame++;
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, newPos, speed);
			}
		}
		else
		{
			frame = 0f;
			newPos = tryGetCircle(prevPos, moveRadius, colliderRadius, (Collider2D)boxCollider);

			//Destroy trapped Slimes to avoid unbeatable levels
			if (newPos == prevPos)
			{
				Destroy(gameObject);
			}

			moving = true;
		}

		transform.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, colliderRadius);
		Gizmos.DrawWireSphere(prevPos, moveRadius);
		Gizmos.DrawLine(prevPos, newPos);
	}
}
