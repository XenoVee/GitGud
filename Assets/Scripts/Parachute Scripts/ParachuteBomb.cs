using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParachuteBomb : ParachuteBase
{
	protected override void hitFloor()
	{
		base.hitFloor();
		scoreTracker.incrementScore(points);
	}
	protected override void hitPlayer()
	{
		base.hitPlayer();
		scoreTracker.modifyLives(-damage);
	}
}
