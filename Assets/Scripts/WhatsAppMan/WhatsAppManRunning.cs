using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(WhatsAppMan))]
[RequireComponent(typeof(Movment))]

public class WhatsAppManRunning : IAction
{
    private Animator _animator;
    private string _actionName = "_isRunning";
    private Rigidbody _rb;
    private Player _player;
    private float _speed;
    private Transform _transform;
    private WhatsAppMan _whatsAppMan;
    private bool _startTask;
    private Movment _move;
    public WhatsAppManRunning SetAnimator(Animator animator)
    {
        _animator = animator;
        return this;
    }
    public WhatsAppManRunning SetAnimationName(string animationName)
    {
        _actionName = animationName;
        return this;
    }
    public WhatsAppManRunning SetRigidBody(Rigidbody rb)
    {
        _rb = rb;
        return this;
    }
    public WhatsAppManRunning SetPlayer(Player player)
    {
        _player = player;
        return this;
    }
    public WhatsAppManRunning SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    public WhatsAppManRunning SetBodyTransform(Transform transform)
    {
        _transform = transform;
        return this;
    }
    public WhatsAppManRunning SetWhatsAppMan(WhatsAppMan whatsAppMan)
    {
        _whatsAppMan = whatsAppMan;
        return this;
    }
    public void Action()
    {
        if (!_startTask)
        {
            _animator.SetBool(_actionName,true);
            _startTask = true ;

        }
        if (_move == null)
        {
            CreateMove();
        }
        _move.Move(_player.transform.position - _transform.position);
        _transform.forward += (_player.transform.position - _transform.position).normalized;
        _whatsAppMan.Think();
        

    }
    public void EndTask()
    {
        _startTask = false;
    }
    public void CreateMove()
    {
        _move = new Movment().SetRigidBody(_rb).SetSpeed(_speed);
    }
}
