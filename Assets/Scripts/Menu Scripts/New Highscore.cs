using System.Text.RegularExpressions;
using System.IO;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewHighscore : MonoBehaviour
{
	public ScorePasser	scorePasser;
	public string		playerName;
	
	[SerializeField] TMPro.TMP_InputField	inputField;
	[SerializeField] private MonoBehaviour SPPrefab;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		scorePasser = FindAnyObjectByType<ScorePasser>();
		if (scorePasser == null)
		{
			scorePasser = (ScorePasser)Instantiate(SPPrefab);
		}
		inputField.ActivateInputField();
	}
	string FormatString(string str)
	{
		string newstr;
		newstr = str.ToUpper();
		return Regex.Replace(newstr, "[^0-9A-Z ]", "");
	}

	public void SaveName()
	{
		playerName = FormatString(inputField.text);
		scorePasser.UpdateHighScoreList(playerName);
		File.WriteAllText(
			System.IO.Path.Combine(Application.persistentDataPath, "highscores.hs"),
			FileSaver.SaveToString(scorePasser.HighScoreList));
		SceneManager.LoadScene("Show High Scores Scene");
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
