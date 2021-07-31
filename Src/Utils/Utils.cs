using System;
using System.Collections.Generic;
using UnityEngine;
public static class Utils
{
	public static float moveToward(float angle, float to, float speed)
	{
		if (Mathf.Abs(angleDist(angle, to)) < speed) return to;
		angle %= 360f;
		to %= 360f;

		if (angle > to == backwardDistance(angle, to) > forwardDistance(angle, to))
		{
			angle -= speed;
		}
		else
		{
			angle += speed;
		}

		return angle;
	}

	public static float angleDist(float a, float b)
	{
		return Mathf.Min((a - b) < 0 ? a - b + 360 : a - b, (b - a) < 0 ? b - a + 360 : b - a);
	}

	public static float forwardDistance(float angle1, float angle2)
	{
		return Mathf.Abs(angle1 - angle2);
	}

	public static float backwardDistance(float angle1, float angle2)
	{
		return 360 - Mathf.Abs(angle1 - angle2);
	}

	public static float angleToMouse(Transform transform)
	{
		Vector3 mouse_pos = Input.mousePosition;
		mouse_pos.z = 3f;

		Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;

		return Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
	}

	public static int getDir(float x, float y)
	{
		if (x > 0)
		{
			return 0;
		}
		else if (x < 0)
		{
			return 1;
		}
		else if (y > 0)
		{
			return 2;
		}
		else if (y < 0)
		{
			return 3;
		}
		return -1;
	}

	public static int getSprite(int[] frames, int frame, int id, int width)
	{
		int pos = id + (frame % frames[id]) * width;
		return pos;
	}

	public static Vector3 moveTangent(Vector3 position, float angle, float radius, float amount)
	{
		/*float hypothenuse = Mathf.Sqrt(radius * radius + amount * amount);
		float totalAngle = angle + Mathf.Atan((amount / radius) * Mathf.Deg2Rad);

		Vector3 relativePosition = new Vector3(Mathf.Cos(totalAngle * Mathf.Deg2Rad) * hypothenuse, Mathf.Sin(totalAngle * Mathf.Deg2Rad) * hypothenuse, 0f);*/
		Vector3 relativePosition = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * radius + Mathf.Cos(angle * Mathf.Deg2Rad) * amount, Mathf.Cos(angle * Mathf.Deg2Rad) * radius - Mathf.Sin(angle * Mathf.Deg2Rad) * amount, 0f);
		return position + relativePosition;
	}

	public static Vector3 tryGetCircle(float angle, Vector3 position, float radius, float colliderRadius, Collider2D ignoreCollider)
	{
		while (angle <= 400f)
		{
			Vector3 newPos = position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);

			Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, colliderRadius);

			if (colliders.Length > (InArray(colliders, ignoreCollider) ? 1 : 0))
			{
				angle += 1f;
			}
			else
			{
				return newPos;
			}

			if (angle > 360f)
			{
				return position;
			}
		}

		return position;
	}

	public static bool InArray(UnityEngine.Object[] array, UnityEngine.Object element)
	{
		return Array.IndexOf(array, element) > -1;
	}

	public static float JumpY(float portion, float maxHeight)
	{
		return Mathf.Pow(Mathf.Clamp(Mathf.Sin(portion * 180f * Mathf.Deg2Rad), 0.0001f, 1f), 0.5f) * maxHeight;
	}

	public static string DebugList<T>(List<T> input)
	{
		string o = "";

		foreach (T i in input)
		{
			o += i.ToString() + ", ";
		}

		return o;
	}

	public static List<String> SplitByChar(string input, char split)
	{
		List<string> output = new List<string>();
		string chunk = "";

		foreach (char i in input)
		{
			if (i == split)
			{
				output.Add(chunk);
				chunk = "";
			}
			else
			{
				chunk += i.ToString();
			}
		}

		if (!input.EndsWith(split.ToString()))
		{
			output.Add(chunk);
		}

		return output;
	}

	public static string JoinByString(List<string> input, string join)
	{
		string output = "";

		int id = 0;
		foreach (string i in input)
		{
			output += i;
			if (id < input.Count) output += join;
		}

		return output;
	}

	public static List<Vector3Int> Range(int x, int y, int x1, int y1, int z)
	{
		List<Vector3Int> output = new List<Vector3Int>();

		for (int i = y; i <= y1; i++)
		{
			for (int j = x; j <= x1; j++)
			{
				output.Add(new Vector3Int(j, i, z));
			}
		}

		return output;
	}

	public static string ListRoomToString(List<List<string>> room){
		string o = "";

		foreach(List<string> row in room){
			foreach(string str in row){
				o += str + ".";
			}
			o += ";";
		}

		return o;
	}
}
