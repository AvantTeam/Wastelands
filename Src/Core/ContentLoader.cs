using UnityEngine;

namespace Content
{
	public class ContentLoader : MonoBehaviour
	{
		public static Weapons weapons = new Weapons();
		public static Rooms rooms = new Rooms();

		void Start()
		{
			weapons.Load();
			rooms.Load();
		}
	}
}