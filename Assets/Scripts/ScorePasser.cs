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
	public List<S_HighScore> highScoreList;

	void Start()
	{
		if (File.Exists(System.IO.Path.Combine(Application.persistentDataPath, "highscores.hs")))
		{
			highScoreList = FileSaver.ReadToList(File.ReadAllText(System.IO.Path.Combine(Application.persistentDataPath, "highscores.hs")));
		}
		else
		{
			highScoreList = new();
		}
		DontDestroyOnLoad(gameObject);
	}

}
