using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private TMP_Text comboText;

	public ScorePasser	scorePasser;

	void Start()
	{
		scorePasser = FindAnyObjectByType<ScorePasser>();
		scoreText.text = "Score: " + scorePasser.score;
		comboText.text = "Highest combo: " + scorePasser.highestCombo;
		scorePasser.UpdateHighScoreList();
;	}

	public void restartGame()
	{
		SceneManager.LoadScene("Game Scene");
	}
	public void mainMenu() 
	{
		SceneManager.LoadScene("Main Menu Scene");
	}


}
	