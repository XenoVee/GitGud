using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct HighScore
{
	public string	HsNname;
	public int		HsScore;
	public int		HsCombo;
}

public class ScorePasser : MonoBehaviour
{
	public int score;
	public int highestCombo;
	public List<HighScore> HighScoreList;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		HighScoreList = new List<HighScore>();
		DontDestroyOnLoad(gameObject);
		//HighScore sc1 = new HighScore();
		//HighScore sc2 = new HighScore();
		//HighScore sc3 = new HighScore();
		//HighScore sc4 = new HighScore();
		//HighScore sc5 = new HighScore();
		//sc1.HsScore = 15;
		//sc2.HsScore = 12;
		//sc3.HsScore = 16;
		//sc4.HsScore = 41;
		//sc5.HsScore = 12;
		//sc1.HsNname = "jeff";
		//sc2.HsNname = "jeff";
		//sc3.HsNname = "jeff";
		//sc4.HsNname = "jeff";
		//sc5.HsNname = "jeff";
		//HighScoreList.Add(sc1);
		//HighScoreList.Add(sc2);
		//HighScoreList.Add(sc3);
		//HighScoreList.Add(sc4);
		//HighScoreList.Add(sc5);
		//UpdateHighScoreList();
	}

	public void UpdateHighScoreList()
	{
		Debug.Log(Mathf.Min(9, HighScoreList.Count) - 1);
		if (HighScoreList.Count > 0)
		{
			if (score < HighScoreList[Mathf.Min(9, HighScoreList.Count) - 1].HsScore)
			{
				return;
			}
			if (HighScoreList.Count == 3)
			{
				HighScoreList.RemoveAt(2);
			}
		}
		HighScore newScore = new HighScore();
		newScore.HsNname = "Names not yet implemented";
		newScore.HsScore = score;
		newScore.HsCombo = highestCombo;
		HighScoreList.Add(newScore);
		HighScoreList.Sort((x, y) => y.HsScore - x.HsScore);
		foreach (HighScore highScore in HighScoreList)
		{
			Debug.Log(highScore.HsNname + " " + highScore.HsScore);
		}
	}
}
