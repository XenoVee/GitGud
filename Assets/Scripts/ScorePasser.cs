using System.Text.RegularExpressions;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.Events;

public struct S_HighScore
{
	public string	HsName;
	public int		HsScore;
	public int		HsCombo;
}


public class ScorePasser : MonoBehaviour
{
	public int score;
	public int highestCombo;
	public List<S_HighScore> HighScoreList;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		if (File.Exists(System.IO.Path.Combine(Application.persistentDataPath, "highscores.hs")))
		{
			HighScoreList = FileSaver.ReadToList(File.ReadAllText(System.IO.Path.Combine(Application.persistentDataPath, "highscores.hs")));
		}
		else
		{
			HighScoreList = new();
		}
		DontDestroyOnLoad(gameObject);
	}

	S_HighScore NewHighScore(string playerName)
	{
		S_HighScore newScore = new S_HighScore();
		newScore.HsName = playerName;
		newScore.HsScore = score;
		newScore.HsCombo = highestCombo;
		return (newScore);
	}
	public void UpdateHighScoreList(string playerName)
	{
		if (HighScoreList.Count > 0)
		{
			if (score < HighScoreList[Mathf.Min(9, HighScoreList.Count) - 1].HsScore && HighScoreList.Count >= 10)
			{
				return;
			}
			if (HighScoreList.Count == 10)
			{
				HighScoreList.RemoveAt(9);
			}
		}
		S_HighScore newScore = NewHighScore(playerName);
		HighScoreList.Add(newScore);
		HighScoreList.Sort((x, y) => y.HsScore - x.HsScore);
	}
}
