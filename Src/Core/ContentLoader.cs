using UnityEngine;

namespace Content
{
	public class ContentLoader : MonoBehaviour
	{
		Weapons weapons = new Weapons();
		Rooms rooms = new Rooms();

		void Start()
		{
			weapons.Load();
			rooms.Load();
		}
	}
}