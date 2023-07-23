using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Player))]

public class WhatsAppManMeleeAttack: IAction
{
    private Animator _animator;
    private string _actionName = "_physicalAttack";
    private bool _startTask;
    private Transform _transform;
    private Player _player;
    private WhatsAppMan _whatsAppMan;
    public WhatsAppManMeleeAttack SetAnimator(Animator animator)
    {
        _animator = animator;
        return this;
    }
    public WhatsAppManMeleeAttack SetAnimationName(string animationName)
    {
        _actionName = animationName;
        return this;
    }
    public WhatsAppManMeleeAttack SetTransform(Transform transform)
    {
        _transform = transform;
        return this;
    }
    public WhatsAppManMeleeAttack SetPlayer(Player player)
    {
        _player = player;
        return this;
    }
    public WhatsAppManMeleeAttack SetWhatsAppMan(WhatsAppMan whatsAppMan)
    {
        _whatsAppMan = whatsAppMan;
        return this;
    }
    public void Action()
    {
        if (!_startTask)
        {
            _whatsAppMan.LookAtPlayer();
            _animator.SetTrigger(_actionName);
            _startTask = true;
        }
    }
    public void EndTask()
    {
        _startTask = false;
    }
}
