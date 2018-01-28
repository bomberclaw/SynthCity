using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	public Player2D player1;
	public Player2D player2;
	public bool playing;

	private float timer = 0;
	private float min = 0;
	private float sec = 0;

	public int player1Score = 0;
	public int player2Score = 0;

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

	void Start() {
		playing = true;
	}

	void Update() {
		if (playing) {
			timer += Time.deltaTime;
			if (timer >= 1) {
				sec += 1;
				timer = 0;
			}
			if (sec > 59) {
				min += 1;
				sec = 0;
			}

			if (min > 0)
				Debug.Log ("Timer: " + min.ToString ("00") + ":" + sec.ToString ("00"));
			else
				Debug.Log ("Timer: " + sec.ToString ("00"));

			if (NPCManager.Instance.npcList.Length == (player1Score + player2Score)) {
				if (player1Score > player2Score) {
					Debug.Log ("player 1 wins");
					playing = false;
				} else if (player2Score > player1Score) {
					Debug.Log ("player 2 wins");
					playing = false;
				}
			}
		}
	}

	public void Exit() {
		Application.Quit ();
	}

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
