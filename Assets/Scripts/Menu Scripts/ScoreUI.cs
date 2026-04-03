using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreUI : MonoBehaviour
{
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private TMP_Text livesText;
	[SerializeField] private TMP_Text comboText;
	[SerializeField] private TMP_Text scoreBonusText;
	[SerializeField] ScoreTracker scoreTracker;

	public void DrawCombo( int combo )
	{
		comboText.text = "Combo: " + combo.ToString();
	}

	public void DrawBonus( int bonus )
	{
		scoreBonusText.text = "Score bonus: " + bonus.ToString();
	}

	public void DrawScore( int score )
	{
		scoreText.text = score.ToString();
	}

	public void DrawLives( int lives )
	{
		livesText.text = "Lives: " + lives.ToString();
	}
}
