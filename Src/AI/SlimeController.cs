using System.Collections;
using UnityEngine;
using static Utils;

public class SlimeController : MonoBehaviour
{
	public Sprite[] sprites;
	public Transform player;
	public float speed;
	public float moveRadius = 2f;
	public float stopTime = 1f;
	public float colliderRadius = 0.5f;
	float frame = 0f;
	int offset = 0;
	bool moving = true;
	SpriteRenderer spriteRenderer, shadowRenderer;
	SpriteMask maskRenderer;
	Vector3 newPos, prevPos;
	GameObject shadow, healthBar;
	BoxCollider2D boxCollider;

	void Start()
	{
		offset = Random.Range(0, 30);
		newPos = transform.position;
		spriteRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
		healthBar = transform.Find("Health Bar").gameObject;
		spriteRenderer.sprite = sprites[0];
		shadow = transform.Find("Shadow").gameObject;
		maskRenderer = transform.Find("Sprite Mask").gameObject.GetComponent<SpriteMask>();
		shadowRenderer = shadow.GetComponent<SpriteRenderer>();
		shadow.transform.localPosition = (new Vector3(0f, 0f, 1f));
		boxCollider = GetComponent<BoxCollider2D>();
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
					maskRenderer.sprite = sprites[0];
				}

				prevPos = transform.position;

				if (frame >= stopTime + offset)
				{
					moving = false;
				}
				frame += Time.deltaTime;
			}
			else
			{
				offset = Random.Range(0, 30);
				transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);

				float height = JumpY(Vector3.Distance(transform.position, newPos) / moveRadius, 1.5f);
				float spriteHeight = Vector3.Distance(transform.position, newPos) / moveRadius;

				int index = Mathf.Clamp((int)Mathf.Round(spriteHeight * (sprites.Length - 1)), 0, (int)sprites.Length - 1);

				if (spriteRenderer.sprite != sprites[index])
				{
					spriteRenderer.sprite = sprites[index];
					maskRenderer.sprite = sprites[index];
				}

				spriteRenderer.transform.localPosition = healthBar.transform.localPosition = maskRenderer.transform.localPosition = (new Vector3(0f, height, -1f));
				maskRenderer.gameObject.transform.localPosition -= new Vector3(0f, 0f, 0.5f);
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
				DamageHandler damageHandler = GetComponent<DamageHandler>();
				damageHandler.Damage(damageHandler.maxHealth + 1);
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
