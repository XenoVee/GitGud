using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float					playerMoveSpeed;
	[SerializeField] private InputActionReference	moveAction;
	[SerializeField] private Rigidbody2D			rigidBody;
	[SerializeField] private SpriteRenderer			spriteRenderer;

	void Update()
	{
		Vector2 input = moveAction.action.ReadValue<Vector2>();
		rigidBody.AddForce( input * playerMoveSpeed );
		if ( rigidBody.linearVelocityX < 0 )
		{
			spriteRenderer.flipX = true;
		}
		if ( rigidBody.linearVelocityX > 0 )
		{
			spriteRenderer.flipX = false;
		}
	}
}
