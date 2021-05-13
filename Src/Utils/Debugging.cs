using UnityEngine;
using UnityEngine.UI;

public class Debugging : MonoBehaviour
{
	Text text;
	Slider slider;
	float fps;
	int wait = 20;
	int i = 20;
	void Start()
	{
		text = GetComponent<Text>();
		slider = GetComponent<Slider>();
	}
	void Update()
	{
		if (text != null)
		{
			if (i >= wait)
			{
				i = 0;
				fps = 1f / Time.deltaTime;
				text.text = "FPS: " + fps.ToString();
			}
			i++;
		}

		if (slider != null)
		{
			int value = (int)slider.value;

			if (value <= 15)
			{
				value = -1;
			}

			Application.targetFrameRate = value;
		}
	}
}
