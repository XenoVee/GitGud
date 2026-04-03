using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Highscorelistmenu : MonoBehaviour
{
	[HideInInspector] public ScorePasser scorePasser;
	[SerializeField] private TMP_Text highScoreNames;
	[SerializeField] private TMP_Text highScoreScores;
	[SerializeField] private TMP_Text highScoreCombos;
	[SerializeField] private MonoBehaviour SPPrefab;

	void Start()
	{
		string hsNames = "";
		string hsScores = "";
		string hsCombos = "";

		scorePasser = FindAnyObjectByType<ScorePasser>();
		if (scorePasser == null)
		{
			scorePasser = (ScorePasser)Instantiate(SPPrefab);
		}
		foreach (var highScore in scorePasser.HighScoreList)
		{
			hsNames += highScore.HsName;
			hsNames += "\n";
			hsScores += "Score: ";
			hsScores += highScore.HsScore;
			hsScores += "\n";
			hsCombos += "Highest Combo: ";
			hsCombos += highScore.HsCombo;
			hsCombos += "\n";
		}
		highScoreNames.text = hsNames;
		highScoreScores.text = hsScores;
		highScoreCombos.text = hsCombos;
	}

	void Update()
	{
		
	}

	public void restartGame()
	{
		SceneManager.LoadScene("Game Scene");
	}
	public void mainMenu()
	{
		SceneManager.LoadScene("Main Menu Scene");
	}
}
