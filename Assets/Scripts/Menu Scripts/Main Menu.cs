using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene( "Game Scene" );
	}

	public void HowToPlay()
	{
		SceneManager.LoadScene( "Tutorial Scene" );
	}

	public void ShowHighScores()
	{
		SceneManager.LoadScene( "Show High Scores Scene" );
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
