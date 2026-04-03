using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private float	spawninterval;
	[SerializeField] MonoBehaviour	parachuteNormal;
	[SerializeField] MonoBehaviour	parachuteBomb;
	[SerializeField] MonoBehaviour	parachuteGolden;
	[SerializeField] ScoreTracker	scoreTracker;
	[SerializeField] private int	spawnHeight;
	[SerializeField] private int	spawnXMin;
	[SerializeField] private int	spawnXMax;

	private float timer;

	void Update()
	{
		timer += Time.deltaTime;
		if ( timer >= spawninterval )
		{
			int randomizer = Random.Range( 0, 10 );
			timer = 0;
			Vector3 spawnpos = new( Random.Range( spawnXMin, spawnXMax ), spawnHeight, 0 );
			Quaternion spawnrot = Quaternion.identity;
			if ( randomizer < 6 )
			{
				ParachuteNormal newParachute = ( ParachuteNormal )Instantiate( parachuteNormal, spawnpos, spawnrot );
				newParachute.scoreTracker = scoreTracker;
			}
			else if ( randomizer < 9 )
			{
				ParachuteBomb newParachute = ( ParachuteBomb )Instantiate( parachuteBomb, spawnpos, spawnrot );
				newParachute.scoreTracker = scoreTracker;
			}
			else
			{
				ParachuteGolden newParachute = ( ParachuteGolden )Instantiate( parachuteGolden, spawnpos, spawnrot );
				newParachute.scoreTracker = scoreTracker;
			}
		}
	}
}
