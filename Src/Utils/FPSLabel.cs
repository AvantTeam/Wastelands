using UnityEngine;
using UnityEngine.UI;

public class FPSLabel : MonoBehaviour
{
	Text text;
	float fps;
	int wait = 20;
	int i = 20;
	void Start()
	{
		text = GetComponent<Text>();
	}
	void Update()
	{
		if (i >= wait)
		{
			i = 0;
			fps = 1f / Time.deltaTime;
			text.text = "FPS: " + fps.ToString();
		}
		i++;
	}
}
