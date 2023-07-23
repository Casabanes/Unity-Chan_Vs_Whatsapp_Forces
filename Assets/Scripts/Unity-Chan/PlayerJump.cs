using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerJump 
{
	private float _jumpForce;
	private ForceMode _forceMode = ForceMode.Impulse;
	private bool _canJump;
	private Rigidbody _rigidBody;
	public PlayerJump SetJumpForce(float jumpforce)
	{
		_jumpForce = jumpforce;
		return this;
	}
	public PlayerJump SetRigidBody(Rigidbody rb)
	{
		_rigidBody = rb;
		return this;
	}
	public void Jump()
	{
		if (_canJump)
        {
			_rigidBody.AddForce(Vector3.up * _jumpForce, _forceMode);
        }
	}
	public void ResetJump()
	{
		_canJump = true;
	}
	public void HasJumped()
	{
		_canJump = false;
	}
}
