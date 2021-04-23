using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Content{
	public class Weapons{
		public static Weapon

		// Melee
		sword, spear, axe, rapier, hammer, scythe,

		// Ranged
		gun, longGun, ammoGun, quickFiregun, caliperGun, spreadGun, firegun, acidGun, electroGun, slimeGun,

		// Magic
		fireWand, iceWand, acidWand, electricWand, fireBomb, iceBomb, acidBomb, electricBomb;

		public void Load(){
			sword = new Weapon(){
				name = "Sword",
				damage = 5,
				height = 3,
				width = 1,
				type = WeaponType.melee,
				speedMultiplier = 1f,
				sprite = LoadWeapon("Melee/sword")
			};
			
			spear = new Weapon(){
				name = "Spear",
				damage = 6,
				height = 3,
				width = 1,
				type = WeaponType.melee,
				speedMultiplier = 1.1f,
				sprite = LoadWeapon("Melee/spear")
			};

			axe = new Weapon(){
				name = "Axe",
				damage = 6,
				width = 1,
				height = 3,
				type = WeaponType.melee,
				speedMultiplier = 0.8f,
				sprite = LoadWeapon("Melee/axe")
			};

			rapier = new Weapon(){
				name = "Rapier",
				damage = 3,
				width = 1,
				height = 3,
				speedMultiplier = 1.5f,
				type = WeaponType.melee,
				sprite = LoadWeapon("Melee/rapier")
			};

			hammer = new Weapon(){
				name = "Hammer",
				damage = 15,
				width = 1,
				height = 3,
				swift = 110,
				rotationSpeed = 1.2f,
				speedMultiplier = 0.5f,
				type = WeaponType.melee,
				sprite = LoadWeapon("Melee/hammer")
			};
			
			scythe = new Weapon(){

			};
		}

		public Sprite LoadWeapon(string name){
			return Resources.Load<Sprite>("Sprites/Weapons/" + name);
		}
	}
}