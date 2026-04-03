using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class NewHighscore : MonoBehaviour
{
	public ScorePasser	scorePasser;
	public string		playerName;
	
	[SerializeField] TMPro.TMP_InputField	inputField;
	[SerializeField] private MonoBehaviour	SPPrefab;
	void Start()
	{
		scorePasser = FindAnyObjectByType<ScorePasser>();
		if ( scorePasser == null )
		{
			scorePasser = ( ScorePasser )Instantiate( SPPrefab );
		}
		inputField.ActivateInputField();
	}

	public void SaveName()
	{
		playerName = inputField.text.ToUpper();
		UpdateHighScoreListWithNew( playerName, scorePasser.score, scorePasser.highestCombo );
		File.WriteAllText(
			System.IO.Path.Combine( Application.persistentDataPath, "highscores.hs" ), 
			FileSaver.SaveToString( scorePasser.highScoreList ) );
		SceneManager.LoadScene( "Show High Scores Scene" );
	}

	S_HighScore NewHighScore(string playerName, int score, int highestCombo)
	{
		S_HighScore newScore = new S_HighScore();
		newScore.HsName = playerName;
		newScore.HsScore = score;
		newScore.HsCombo = highestCombo;
		return (newScore);
	}

	// adds a new highscore to the list, if it is high enough
	public void UpdateHighScoreListWithNew(string playerName, int score, int highestCombo)
	{
		if (scorePasser.highScoreList.Count > 0)
		{
			if (score < scorePasser.highScoreList[Mathf.Min(9, scorePasser.highScoreList.Count) - 1].HsScore && scorePasser.highScoreList.Count >= 10)
			{
				return;
			}
			if (scorePasser.highScoreList.Count == 10)
			{
				scorePasser.highScoreList.RemoveAt(9);
			}
		}
		S_HighScore newScore = NewHighScore(playerName, score, highestCombo);
		scorePasser.highScoreList.Add(newScore);
		scorePasser.highScoreList.Sort((x, y) => y.HsScore - x.HsScore);
	}
}
