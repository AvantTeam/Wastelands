using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Content;

public class DamageHandler : MonoBehaviour
{
	public int maxHealth;
	int health;
	public AudioClip hit, death;
	ParticleSystem deathParticles;
	new AudioSource audio;
	SpriteRenderer objectRenderer, mask, healthBar;
	bool receivingDamage = true;

	void Start()
	{
		health = maxHealth;

		deathParticles = transform.Find("Death System").gameObject.GetComponent<ParticleSystem>();
		audio = transform.Find("Source").gameObject.GetComponent<AudioSource>();

		objectRenderer = GetComponent<SpriteRenderer>();
		mask = transform.Find("Sprite Mask").gameObject.GetComponent<SpriteRenderer>();

		PlayerController playerController;
		if (!TryGetComponent<PlayerController>(out playerController)) healthBar = transform.Find("Health Bar").Find("Bar Overlay").gameObject.GetComponent<SpriteRenderer>();
	}

	public void Damage(int amount)
	{
		if (health <= 0) return;
		health -= amount;

		if (audio != null)
		{
			audio.clip = hit;
			audio.Play();
		}

		if (healthBar != null)
		{
			float healthFraction = ((float)health) / ((float)maxHealth);
			healthBar.size = new Vector2(healthFraction, healthBar.size.y);
			healthBar.transform.localPosition = new Vector3(-((1f - healthFraction) / 2f), 0.5f, 0f);
		}

		StartCoroutine("DrawDamage");

		if (health <= 0)
		{
			if (deathParticles != null) deathParticles.Play();
			if (audio != null)
			{
				audio.clip = death;
				audio.Play();
			}

			healthBar.enabled = false;
			healthBar.gameObject.transform.parent.Find("Bar Base").gameObject.GetComponent<SpriteRenderer>().enabled = false;
			objectRenderer.enabled = false;
			mask.enabled = false;
			transform.Find("Shadow").gameObject.SetActive(false);

			StartCoroutine("EndDeath");
		}
	}

	IEnumerator EndDeath()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}

	IEnumerator DrawDamage()
	{
		mask.enabled = true;
		yield return new WaitForSeconds(0.2f);
		mask.enabled = false;
	}
}
