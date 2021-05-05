using UnityEngine;
using Content;
using static Utils;

public class PlayerController : MonoBehaviour
{
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
	SpriteRenderer sprr;
	float x, y;
	bool stepp = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sprr = GetComponent<SpriteRenderer>();
		weaponObject = transform.Find("Weapon").gameObject;
		weaponRenderer = weaponObject.GetComponent<SpriteRenderer>();
		weapon = Weapons.axe;
		BoxCollider2D wBox = weaponObject.GetComponent<BoxCollider2D>();
		wBox.size = new Vector2(weapon.height, weapon.width);
		sprites = Resources.LoadAll<Sprite>("Sprites/Player/player");
		source = GetComponent<AudioSource>();
	}

	void Update()
	{
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

		// Thanks NiChrosia
		int num = getDir(x, y);

		if (num >= 2)
		{
			num = dir;
		}

		if (num != -1)
		{
			sprr.sprite = sprites[getSprite(frames, frame, dir, 4)];
			dir = num;
		}
		else sprr.sprite = sprites[dir];

		stepp = x != 0 || y != 0;

		rb.position += new Vector2(x, y) * 0.03f * weapon.speedMultiplier;
		tempFrame += 0.05f;
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
