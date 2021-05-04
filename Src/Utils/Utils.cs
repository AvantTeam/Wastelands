using UnityEngine;
public static class Utils {
	public static float moveToward(float angle, float to, float speed){
		if(Mathf.Abs(angleDist(angle, to)) < speed) return to;
		angle %= 360f;
		to %= 360f;

		if(angle > to == backwardDistance(angle, to) > forwardDistance(angle, to)){
			angle -= speed;
		}else{
			angle += speed;
		}

		return angle;
	}

	public static float angleDist(float a, float b){
		return Mathf.Min((a - b) < 0 ? a - b + 360 : a - b, (b - a) < 0 ? b - a + 360 : b - a);
	}

	public static float forwardDistance(float angle1, float angle2){
		return Mathf.Abs(angle1 - angle2);
	}

	public static float backwardDistance(float angle1, float angle2){
		return 360 - Mathf.Abs(angle1 - angle2);
	}

	public static float angleToMouse(Transform transform){
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = 3f;

        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
            
        return Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
    }
}