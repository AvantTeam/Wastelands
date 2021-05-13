using UnityEngine;
using Content;
using static Utils;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public int[] frames;
	public AudioClip step;
	AudioSource source;
	Sprite[] sprites;
	GameObject weaponObject;
	SpriteRenderer weaponRenderer;
	public Weapon weapon;
	int dir, frame = 0;
	float tempFrame = 0;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	float x, y;
	bool stepp = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		weaponObject = transform.Find("Weapon").gameObject;
		weaponRenderer = weaponObject.GetComponent<SpriteRenderer>();
		weapon = Weapons.axe;
		sprites = Resources.LoadAll<Sprite>("Sprites/Player/player");
		source = GetComponent<AudioSource>();
	}

	void Update()
	{
		transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		if (weapon == null)
		{
			weapon = Weapons.axe;
		}
		else
		{
			weaponRenderer.sprite = weapon.sprite;
		}
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");

		if (x != 0) spriteRenderer.flipX = x < 0;

		// Thanks NiChrosia
		int num = getDir(x, y);

		if (num != -1)
		{
			spriteRenderer.sprite = sprites[getSprite(frames, frame, 0, 2)];
		}
		else spriteRenderer.sprite = sprites[getSprite(frames, frame, 1, 2)];

		stepp = x != 0 || y != 0;

		rb.position += (new Vector2(x, y) * speed * weapon.speedMultiplier * Time.deltaTime);
		tempFrame += Time.deltaTime;
		frame = (int)Mathf.Round(tempFrame);

		if (!stepp) source.Stop();

		if (stepp && !source.isPlaying)
		{
			source.clip = step;
			source.Play();
		}

		stepp = false;
	}
}
