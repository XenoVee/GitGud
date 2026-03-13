using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParachuteGolden : ParachuteBase
{
	[SerializeField] protected int pointMultiplier = 5;
	protected override void hitFloor()
	{
		base.hitFloor();
		scoreTracker.modifyLives(-damage);
	}
	protected override void hitPlayer()
	{
		base.hitPlayer();
		scoreTracker.incrementScore(points, pointMultiplier);
	}
}
