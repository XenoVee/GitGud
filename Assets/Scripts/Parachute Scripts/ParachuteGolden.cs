using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParachuteGolden : ParachuteBase
{
	[SerializeField] protected int pointMultiplier = 5;
	protected override void HitFloor()
	{
		base.HitFloor();
		scoreTracker.ModifyLives( -damage );
	}
	protected override void HitPlayer()
	{
		base.HitPlayer();
		scoreTracker.IncrementScore( points, pointMultiplier );
	}
}
