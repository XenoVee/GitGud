using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParachuteNormal : ParachuteBase
	{
	protected override void hitFloor()
	{
		base.hitFloor();
		scoreTracker.modifyLives(-damage);
	}
	protected override void hitPlayer()
	{
		base.hitPlayer();
		scoreTracker.incrementScore(points);
	}
}
