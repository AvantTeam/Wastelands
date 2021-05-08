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
	float moveTime = 0f;
	int frame = 0;
	bool moving = false;
	SpriteRenderer spriteRenderer, shadowRenderer;
	Vector3 newPos, prevPos;
	GameObject shadow, healthBar;
	BoxCollider2D boxCollider;

	void Start()
	{
		spriteRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
		healthBar = transform.Find("Health Bar").gameObject;
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
				if (spriteRenderer.sprite != idle[0]) spriteRenderer.sprite = idle[0];

				prevPos = transform.position;

				if (frame >= stopTime)
				{
					moving = false;
				}
				frame++;
			}
			else
			{
				if (spriteRenderer.sprite != jumping[0]) spriteRenderer.sprite = jumping[0];
				transform.position = Vector3.MoveTowards(transform.position, newPos, speed);
				spriteRenderer.transform.localPosition = healthBar.transform.localPosition = (new Vector3(0f, JumpY(Vector3.Distance(transform.position, newPos) / moveRadius, 1f), 0f));
			}
		}
		else
		{
			frame = 0;
			newPos = tryGetCircle(prevPos, moveRadius, colliderRadius, (Collider2D)boxCollider);

			if (newPos.x > prevPos.x) spriteRenderer.flipX = false;
			else if (newPos.x < prevPos.x) spriteRenderer.flipX = true;

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
