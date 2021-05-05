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

	public static int getSprite(int[] frames, int frame, int id, int maxFrame)
	{
		int pos = id + (frame % frames[id]) * maxFrame;
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
}