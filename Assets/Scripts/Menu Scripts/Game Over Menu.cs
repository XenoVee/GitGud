using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
	[SerializeField] private TMP_Text		scoreText;
	[SerializeField] private TMP_Text		comboText;
	[SerializeField] public ScorePasser		scorePasser;
	[SerializeField] private MonoBehaviour	SPPrefab;

	void Start()
	{
		scorePasser = FindAnyObjectByType<ScorePasser>();
		if ( scorePasser == null )
		{
			scorePasser = ( ScorePasser )Instantiate( SPPrefab );
		}
		scoreText.text = "Score: " + scorePasser.score;
		comboText.text = "Highest combo: " + scorePasser.highestCombo;
	}
	public void RestartGame()
	{
		SceneManager.LoadScene( "Game Scene" );
	}
	public void MainMenu() 
	{
		SceneManager.LoadScene( "Main Menu Scene" );
	}

	public void HighScoreMenu()
	{
		SceneManager.LoadScene( "Show High Scores Scene" );
	}


}
	