using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	public Vector3 rectSize;
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, rectSize / 8f);
	}
	void Update()
	{
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}