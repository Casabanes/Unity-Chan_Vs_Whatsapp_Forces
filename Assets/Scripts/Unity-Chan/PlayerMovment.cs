using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Transform))]

public class PlayerMovment
{
	private Vector3 _direction;
	private Rigidbody _rigidBody;
	private float _originalSpeed;
	private float _speed;
	private Animator _animator;
	private string _animatorParameter = "_speed";
	private Transform _transform;
	public event Action<string,float> _isMoving;
	public PlayerMovment SetRigidBody(Rigidbody rb)
    {
		_rigidBody = rb;
		return this;
	}
	public PlayerMovment SetAnimator(Animator animator)
    {
		_animator = animator;
		return this;
	}
	public PlayerMovment SetTransform(Transform transform)
    {
		_transform = transform;
		return this;
	}
	public PlayerMovment SetSpeed(float speed)
    {
		_speed = speed;
		_originalSpeed = _speed;
		return this;
    }
	public void Move(float v, float h)
	{
		_direction.z= v;
		_direction.x = h;
		_rigidBody.MovePosition(_transform.position + _direction * (_speed * Time.deltaTime));

		//_animator.SetFloat(_animatorParameter, _direction.magnitude);
		_isMoving?.Invoke(_animatorParameter,_direction.magnitude);
		_transform.LookAt(_transform.position + _direction);
	}
	public void IsSlowed(float slowedSpeed)
    {
		_speed = slowedSpeed;
    }
	public void IsNotSlowedAnyMore()
    {
		_speed = _originalSpeed;
    }
}
