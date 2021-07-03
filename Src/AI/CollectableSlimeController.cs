using System.Collections;
using UnityEngine;
using static Utils;

public class CollectableSlimeController : MonoBehaviour
{
	public Sprite[] sprites;
	public float speed;
	public float moveRadius = 2f;
	public float stopTime = 1f;
	public float colliderRadius = 0.5f;
    float frame, playerAng = 0f;
	int offset = 0;
	bool moving = true;
	SpriteRenderer spriteRenderer, shadowRenderer;
	Vector3 newPos, prevPos;
	GameObject shadow, player;
	BoxCollider2D boxCollider;

	void Start()
	{
		offset = Random.Range(0, 30);
		spriteRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprites[0];
		shadow = transform.Find("Shadow").gameObject;
		shadowRenderer = shadow.GetComponent<SpriteRenderer>();
		shadow.transform.localPosition = (new Vector3(0f, 0f, 1f));
		boxCollider = GetComponent<BoxCollider2D>();
        newPos = transform.position;
        playerAng = Vector2.Angle(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
	}

	void Update()
	{
		if (moving)
		{
			if (transform.position == newPos)
			{
				if (spriteRenderer.sprite != sprites[0])
				{
					spriteRenderer.sprite = sprites[0];
				}

				prevPos = transform.position;

				if (frame >= stopTime + offset)
				{
					moving = false;
				}
				frame += Time.deltaTime * speed;
				Debug.Log(stopTime + offset);
			}
			else
			{
				offset = Random.Range(0, 10);
				transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);

				float height = JumpY(Vector3.Distance(transform.position, newPos) / moveRadius, 1.5f);
				float spriteHeight = Vector3.Distance(transform.position, newPos) / moveRadius;

				int index = Mathf.Clamp((int)Mathf.Round(spriteHeight * (sprites.Length - 1)), 0, (int)sprites.Length - 1);

				if (spriteRenderer.sprite != sprites[index])
				{
					spriteRenderer.sprite = sprites[index];
				}

				spriteRenderer.transform.localPosition = (new Vector3(0f, height, -1f));
			}
		}
		else
		{
			frame = 0;
			newPos = tryGetCircle(Vector2.Angle(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y)), prevPos, moveRadius, colliderRadius, (Collider2D)boxCollider);

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