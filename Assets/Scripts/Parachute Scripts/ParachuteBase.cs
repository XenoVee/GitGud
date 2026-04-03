using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParachuteBase : MonoBehaviour
{
	[SerializeField] protected float		rotationMagnitudeMax	= 5;
	[SerializeField] protected float		rotationMagnitudeMin	= 2;
	[SerializeField] protected float		rotationSpeedMax		= 3;
	[SerializeField] protected float		rotationSpeedMin		= 2;
	[SerializeField] protected float		horizontalSpeedMax		= 20;
	[SerializeField] protected float		horizontalSpeedMin		= 15;
	[SerializeField] protected float		fallSpeedMax			= 25;
	[SerializeField] protected float		fallSpeedMin			= 20;
	[SerializeField] protected int			points					= 1;
	[SerializeField] protected int			damage					= 1;
	[HideInInspector] public ScoreTracker	scoreTracker;

	protected Vector3	startingAngle;
	protected float		angleTimer;
	protected float		angle;
	protected float		rotationMagnitude; 
	protected float		rotationSpeed;
	protected float		horizontalSpeed;
	protected float		fallSpeed;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		startingAngle = transform.rotation.eulerAngles;
		angleTimer = Random.Range(0, Mathf.PI);
		rotationMagnitude = Random.Range(rotationMagnitudeMin, rotationMagnitudeMax);
		rotationSpeed = Random.Range(rotationSpeedMax, rotationSpeedMin);
		horizontalSpeed = Random.Range(horizontalSpeedMin, horizontalSpeedMax);
		fallSpeed = Random.Range(fallSpeedMin, fallSpeedMax);
	}

	// Update is called once per frame
	void Update()
	{
		// links-rechts sway via sinusgolf. (0 is helemaal links, pi is helemaal rechts)
		if (angleTimer >= 2 * Mathf.PI)
		{
			angleTimer -= 2 * Mathf.PI;
		}
		angleTimer += Time.deltaTime * rotationSpeed;
		angle = Mathf.Sin(angleTimer) * rotationMagnitude;
		transform.eulerAngles = new(0, 0, startingAngle.z + angle);

		Vector3 move = new(0, 0, 0);
		move.x += angle * horizontalSpeed / 1000;
		move.y = -(fallSpeed / 1000);
		transform.position += move;
	}

	protected virtual void hitFloor()
	{

	}
	protected virtual void hitPlayer()
	{

	}
	protected void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("LeftWall"))
		{
			angleTimer = 0;
		}
		else if (collision.gameObject.CompareTag("RightWall"))
		{
			angleTimer = Mathf.PI;
		}
		else if (collision.gameObject.CompareTag("Floor"))
		{
			hitFloor();
			Destroy(gameObject);
		}
		else if (collision.gameObject.CompareTag("Player"))
		{
			hitPlayer();
			Destroy(gameObject);
		}
	}
}
