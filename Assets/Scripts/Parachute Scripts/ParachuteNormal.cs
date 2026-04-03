using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParachuteNormal : ParachuteBase
	{
	protected override void HitFloor()
	{
		base.HitFloor();
		scoreTracker.ModifyLives( -damage );
	}
	protected override void HitPlayer()
	{
		base.HitPlayer();
		scoreTracker.IncrementScore( points );
	}
}
