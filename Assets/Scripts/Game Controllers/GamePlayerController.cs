 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayerController : MonoBehaviour {

	public static GamePlayerController instance;
	[SerializeField]
	private Text scoreText, endScore, bestScore, gameOverText;
	[SerializeField]
	private Button restartGameButton, instructionsButton;
	[SerializeField]
	private GameObject pausePanel;
	[SerializeField]
	private GameObject[] birds;
	[SerializeField]
	private Sprite[] medals;
	[SerializeField]
	private Image medalImage;

	

	// Use this for initialization
	void Awake () {
		MakeInstance ();
		Time.timeScale = 0f;
	}

	void MakeInstance () {
		if(instance == null) {
			instance = this;
		}
	}

	// Sự kiện click nút pause
	public void PauseGame () {
		if (BirdScripts.instance != null) {
			if (BirdScripts.instance.isAlive) {
				pausePanel.SetActive (true);
				gameOverText.gameObject.SetActive (false);
				endScore.text = "" + BirdScripts.instance.score;
				bestScore.text = "" + GameController.instance.GetHighScore();
				Time.timeScale = 0f;
				restartGameButton.onClick.RemoveAllListeners();
				restartGameButton.onClick.AddListener(() => ResumeGame());

				EnemyCollector.instance.SetPause();

				if(GameController.instance.GetHighScore() <= 20) {
					medalImage.sprite = medals[0];
				}
				else if(GameController.instance.GetHighScore() > 20 && GameController.instance.GetHighScore() <= 40) {
					medalImage.sprite = medals[1];
				}
				else {
					medalImage.sprite = medals[2];
				}
			}
		}
	}

	// Sự kiện click nút menu
	public void GoToMenuButton () {
		SceneFader.instance.FadeIn ("MainMenu");
	}
	
	// Sự kiện click nút resume
	public void ResumeGame () {
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
		EnemyCollector.instance.Spawner();
	}

	// Sự kiện click nút restart
	public void RestartGame () {
		SceneFader.instance.FadeIn ("GamePlay");
	}

	// Sự kiện click nút instruction play game
	public void PlayGame () {
		scoreText.gameObject.SetActive(true);
		birds[GameController.instance.GetSelectedBird()].SetActive(true);
		instructionsButton.gameObject.SetActive(false);
		Time.timeScale = 1f;
		EnemyCollector.instance.Spawner();
		
	}

	public void BestScore (int score) {
		scoreText.text = "" + score;
	}

	// Sự kiện khi bird chết
	public void PlayerDiedShowScore (int score) {
		pausePanel.SetActive(true);
		gameOverText.gameObject.SetActive(true);
		scoreText.gameObject.SetActive(false);
		endScore.text = "" + score;

		// Lấy điểm cao nhất
		if(score > GameController.instance.GetHighScore()) {
			GameController.instance.SetHighScore(score);
		}
		bestScore.text = "" + GameController.instance.GetHighScore();

		// Gắn huy chương
		if(score <= 20) {
			medalImage.sprite = medals[0];
		}
		else if(score > 20 && score <= 40) {
			medalImage.sprite = medals[1];
		}
		else {
			medalImage.sprite = medals[2];
		}
		restartGameButton.onClick.RemoveAllListeners();
		restartGameButton.onClick.AddListener(() => RestartGame());
	}
}
