using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
	[SerializeField] private int		StartingLives;
	[SerializeField] private int		comboBonusIncreaseinterval;
	[SerializeField] private int		comboLifegainInterval;
	[SerializeField] private int		comboLifegainAmount;
	[SerializeField] private int		comboLifegainMaximum;

	[Header("Canvas text fields")]
	[SerializeField] private TMP_Text	scoreText;
	[SerializeField] private TMP_Text	livesText;
	[SerializeField] private TMP_Text	comboText;
	[SerializeField] private TMP_Text	scoreBonusText;

	private ScorePasser	scorePasser;
	private int			currentCombo;
	private int			comboScoreBonus;
	private int			highestCombo;
	private int			lives;
	private int			score = 0;

	void Start()
	{
		lives = StartingLives;
		scoreText.text = "0";
		printLives();
		scorePasser = FindAnyObjectByType<ScorePasser>();
	}

	void Update()
	{
		if (lives < 1)
		{
			scorePasser.score = score;
			scorePasser.highestCombo = highestCombo;
			SceneManager.LoadScene("Game Over Scene");
		}
	}

	private void updateCombo()
	{
		comboScoreBonus = currentCombo / comboBonusIncreaseinterval;
		if (currentCombo % comboLifegainInterval == 0 && currentCombo > 0)
		{
			modifyLives(comboLifegainAmount);
		}
		comboText.text = "Combo: " + currentCombo;
		scoreBonusText.text = "Score bonus: " + comboScoreBonus;
	}

	// Overload on incrementScore for treasure chests score mult
	public void incrementScore(int increment)
	{
		incrementScore(increment, 1);
	}

	public void incrementScore(int increment, int multiplier)
	{
		if (increment * multiplier > 0)
		{
			score += (increment + comboScoreBonus) * multiplier;
			scoreText.text = score.ToString();
			currentCombo++;
			updateCombo();
		}
	}

	void printLives()
	{
		livesText.text = "Lives: " + lives.ToString();
	}

	public void modifyLives(int amount)
	{
		if (!(amount > 0 && lives >= comboLifegainMaximum))
		{ 
			lives += amount;
		}
		printLives();
		if (amount < 0)
		{
			if (currentCombo > highestCombo)
			{
				highestCombo = currentCombo;
			}
			currentCombo = 0;
			updateCombo();
		}
	}
}
