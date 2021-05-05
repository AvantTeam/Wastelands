using UnityEngine;

namespace Content
{
	public class ContentLoader : MonoBehaviour
	{
		Weapons weapons = new Weapons();

		void Start()
		{
			weapons.Load();
		}
	}
}