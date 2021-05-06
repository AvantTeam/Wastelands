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

	void Start()
	{
		prevPos = transform.position;
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = idle[0];
		shadow = transform.Find("Shadow").gameObject;
		shadowRenderer = shadow.GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (moving)
		{
			prevPos = Vector3.MoveTowards(prevPos, newPos, speed);

			transform.position = prevPos;

			if (prevPos == newPos)
			{
				StartCoroutine("Sleep");
			}
		}
		else
		{
			newPos = tryGetCircle(prevPos, moveRadius, colliderRadius);

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
	}

	IEnumerator Sleep()
	{
		yield return new WaitForSeconds(stopTime);
		moving = false;
	}
}
