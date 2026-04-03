using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParachuteBomb : ParachuteBase
{
	protected override void HitFloor()
	{
		base.HitFloor();
		scoreTracker.IncrementScore( points );
	}
	protected override void HitPlayer()
	{
		base.HitPlayer();
		scoreTracker.ModifyLives( -damage );
	}
}
