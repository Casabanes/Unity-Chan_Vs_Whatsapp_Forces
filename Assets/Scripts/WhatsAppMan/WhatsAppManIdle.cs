using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WhatsAppMan))]

public class WhatsAppManIdle : IAction
{
    private Animator _animator;
    private string _actionName= "_isRunning";
    private WhatsAppMan _whatsAppMan;

    private bool _startTask;

    public WhatsAppManIdle SetAnimator(Animator animator)
    {
        _animator = animator;
        return this;
    }
    public WhatsAppManIdle SetAnimationName(string animationName)
    {
        _actionName = animationName;
        return this;
    }
    public WhatsAppManIdle SetWhatsAppMan(WhatsAppMan whatsAppMan)
    {
        _whatsAppMan = whatsAppMan;
        return this;
    }

    public void Action()
    {
        if (!_startTask)
        {
        _animator.SetBool(_actionName,false);
        _startTask = true;
        }
        _whatsAppMan.Think();
    }
    public void EndTask()
    {
        _startTask = false;
    }
}
