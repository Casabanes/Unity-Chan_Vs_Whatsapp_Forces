using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WhatsAppMan))]

public class WhatsAppManFireBall : MonoBehaviour,IAction
{
    private Animator _animator;
    private string _actionName = "_magicalAttack";
    private WhatsAppMan _whatsAppMan;
    private bool _startTask;
    private BaseParticleProjectil _fireBall;
    private Transform _origin;
    private float _attackDelay;
    public WhatsAppManFireBall SetAnimator(Animator animator)
    {
        _animator = animator;
        return this;
    }
    public WhatsAppManFireBall SetAnimationName(string animationName)
    {
        _actionName = animationName;
        return this;
    }
    public WhatsAppManFireBall SetWhatsAppMan(WhatsAppMan whatsAppMan)
    {
        _whatsAppMan = whatsAppMan;
        return this;
    }
    public WhatsAppManFireBall SetProjectil(BaseParticleProjectil projectil)
    {
        _fireBall = projectil;
        return this;
    }
    public WhatsAppManFireBall SetOrigin(Transform origin)
    {
        _origin = origin;
        return this;
    }
    public WhatsAppManFireBall SetDelay(float delay)
    {
        _attackDelay = delay;
        return this;
    }
    public void Action()
    {
        if (!_startTask)
        {
            _animator.SetTrigger(_actionName);
            StartCoroutine(RangeAttack());
            _startTask = true;
        }
    }
    public IEnumerator RangeAttack()
    {
        _whatsAppMan.LookAtPlayer();
        yield return new WaitForSeconds(_attackDelay);
        _fireBall.SetDirection(_origin.forward);
        Instantiate(_fireBall, _origin.transform.position,_origin.transform.rotation);
    }
    public void EndTask()
    {
        _startTask = false;
    }
}
