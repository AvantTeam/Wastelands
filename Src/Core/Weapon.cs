using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Content{
	public class Weapon{
		public string name;
		public float damage;
		public float height;
		public float width;
		public float swift = 90f;
		public float rotationSpeed = 2f;
		public float speedMultiplier;
		public float cone = 20f;
		public AudioClip sfx = Resources.Load<AudioClip>("Sfx/slash");
		public WeaponType type;
		public Sprite sprite;
	}
}