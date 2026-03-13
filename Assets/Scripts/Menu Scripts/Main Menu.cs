using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void startGame()
	{
		SceneManager.LoadScene("Game Scene");
	}

	public void howToPlay()
	{
		SceneManager.LoadScene("Tutorial Scene");
	}

	public void exitGame()
	{
		Application.Quit();
	}
}
