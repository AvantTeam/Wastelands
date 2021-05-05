using UnityEngine;
using Content;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;

	[SerializeField] public GameState state;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	public void SetState(GameState state)
	{
		instance.state = state;
	}

    public static GameState GetState(){
		return instance.state;
	}

	public static GameManager Instance
	{
		get { return instance; }
	}
}
