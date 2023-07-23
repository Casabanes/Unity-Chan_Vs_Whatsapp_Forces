using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movment 
{
    private Rigidbody _rb;
    private float _speed;

    public void Move(Vector3 dir)
    {
        _rb.velocity = dir;
        dir.y = 0;
        _rb.velocity = (dir.normalized * _speed);
    }
    public Movment SetRigidBody(Rigidbody rb)
    {
        _rb = rb;
        return this;
    }
    public Movment SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
}
