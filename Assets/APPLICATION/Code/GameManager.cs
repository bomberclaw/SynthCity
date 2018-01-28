using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	public Player2D player1;
	public Player2D player2;

	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<GameManager>();
				if (_instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(GameManager).ToString();
					_instance = obj.AddComponent<GameManager>();
				}
			}
			return _instance;
		}
	}

	public void GameOver(string winner) {
		if (winner == "j1") {
			//Player 1 Wins
		} else {
			//Player 2 Wins
		}
	}

	public void Exit() {
		Application.Quit ();
	}

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
