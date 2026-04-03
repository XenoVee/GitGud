using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
	[SerializeField] private int		StartingLives;
	[SerializeField] private int		comboBonusIncreaseinterval;
	[SerializeField] private int		comboLifegainInterval;
	[SerializeField] private int		comboLifegainAmount;
	[SerializeField] private int		comboLifegainMaximum;

	private int			currentCombo;
	private int			comboScoreBonus;
	private int			highestCombo;
	private int			lives;
	private int			score = 0;
	private ScorePasser	scorePasser;

	[SerializeField] private ScoreUI		scoreUi;
	[SerializeField] private MonoBehaviour	SPPrefab;
	[SerializeField] public UnityEvent<int>	m_DrawScore;
	[SerializeField] public UnityEvent<int>	m_DrawCombo;
	[SerializeField] public UnityEvent<int>	m_DrawBonus;
	[SerializeField] public UnityEvent<int>	m_DrawLives;

	void Start()
	{

		lives = StartingLives;
		m_DrawScore.Invoke( 0 );
		m_DrawLives.Invoke( lives );
		
		scorePasser = FindAnyObjectByType<ScorePasser>();
		if ( scorePasser == null )
		{
			scorePasser = ( ScorePasser )Instantiate( SPPrefab );
		}
	}

	private void UpdateCombo()
	{
		comboScoreBonus = currentCombo / comboBonusIncreaseinterval;
		if ( currentCombo % comboLifegainInterval == 0 && currentCombo > 0 )
		{
			ModifyLives( comboLifegainAmount );
		}
		m_DrawCombo.Invoke( currentCombo );
		m_DrawBonus.Invoke( comboScoreBonus );
	}

	// Overload on incrementScore for treasure chests score mult
	public void IncrementScore( int increment )
	{
		IncrementScore( increment, 1 );
	}

	public void IncrementScore( int increment, int multiplier )
	{
		if ( increment * multiplier > 0 )
		{
			score += ( increment + comboScoreBonus ) * multiplier;
			m_DrawScore.Invoke( score );
			currentCombo++;
			UpdateCombo();
		}
	}

	public void ModifyLives( int amount )
	{
		if ( !( amount > 0 && lives >= comboLifegainMaximum ) )
		{ 
			lives += amount;
		}
		m_DrawLives.Invoke( lives );
		if ( amount < 0 )
		{
			if ( currentCombo > highestCombo )
			{
				highestCombo = currentCombo;
			}
			currentCombo = 0;
			UpdateCombo();
		}
		if ( lives < 1 )
		{
			scorePasser.score = score;
			scorePasser.highestCombo = highestCombo;
			if ( scorePasser.highScoreList.Count < 10 || score >= scorePasser.highScoreList[scorePasser.highScoreList.Count - 1].HsScore )
			{
				SceneManager.LoadScene( "New High Score" );
			}
			else
			{
				SceneManager.LoadScene( "Game Over Scene" );
			}
		}
	}
}
