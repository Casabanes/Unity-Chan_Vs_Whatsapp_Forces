using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WhatsAppManBaseBrain))]

public class WhatsAppMan : MonoBehaviour
{

    private IAction _actionEquiped;
    private IAction _actionIdle;
    private IAction _actionRun;
    private IAction _actionPunch;
    private IAction _actionMagic;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private WhatsAppManBaseBrain _brain;
    private bool _isDoingStuff;
    [SerializeField]
    public Player _player;
    [SerializeField]
    private EnemyWeapon _knucles;
    [SerializeField]
    private Transform _fireBallOrigin;
    [SerializeField]
    private BaseParticleProjectil _projectil;
    [SerializeField]
    private float _fireBallDelay;
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Error fatal, el objeto WhatsAppMan no tiene el componente Animator");
            return;
        }
        _actionIdle = new WhatsAppManIdle().SetAnimator(_animator).SetAnimationName(WhatsAppFlyweightPointer.BaseWhatsAppMan._running).SetWhatsAppMan(this);
        _actionRun = new WhatsAppManRunning().SetAnimator(_animator)
                                             .SetAnimationName(WhatsAppFlyweightPointer.BaseWhatsAppMan._running)
                                             .SetRigidBody(_rb)
                                             .SetPlayer(_player)
                                             .SetSpeed(WhatsAppFlyweightPointer.BaseWhatsAppMan._speed)
                                             .SetWhatsAppMan(this)
                                             .SetBodyTransform(transform);
        _actionPunch = new WhatsAppManMeleeAttack().SetAnimator(_animator)
                                                  .SetAnimationName(WhatsAppFlyweightPointer.BaseWhatsAppMan._physicalAttack)
                                                  .SetTransform(transform)
                                                  .SetPlayer(_player)
                                                  .SetWhatsAppMan(this);
        _actionMagic = GetComponent<WhatsAppManFireBall>().SetAnimator(_animator)
                                                          .SetAnimationName(WhatsAppFlyweightPointer.BaseWhatsAppMan._magicalAttack)
                                                          .SetWhatsAppMan(this)
                                                          .SetOrigin(_fireBallOrigin)
                                                          .SetProjectil(_projectil)
                                                          .SetDelay(_fireBallDelay);
        if (_brain == null)
        {
            Debug.LogError("Error fatal, WhatsAppMan no tiene cerebro (brain) asignado");
            return;
        }
        else
        {
            if (_player == null)
            {
                _player = FindObjectOfType<Player>();
            }
            if (_player == null)
            {
                Debug.LogError("Error fatal no hay Player en la escena");
                return;
            }
            else
            {
                _brain.SetPlayer(_player)
                      .SetIdle(_actionIdle)
                      .SetRun(_actionRun)
                      .SetMagic(_actionMagic)
                      .SetPunch(_actionPunch)
                      .SetBody(this);
            }
        }
        _actionEquiped = _actionIdle;
    }
    public void ChangeAction(IAction action)
    {
        _actionEquiped = action;
    }
    void Update()
    {
        _actionEquiped.Action();
    }
    public void Think()
    {
        _actionEquiped.EndTask();
        _brain.OrdersToBody();
    }
    public void ResetWeapon()
    {
        _knucles.OnOffColliders(true);
        _knucles.AttackSound();
    }
    public void TurnOffWeapon()
    {
        _knucles.OnOffColliders(false);
    }
    public void LookAtPlayer()
    {
        transform.forward = (_player.transform.position - transform.position).normalized;
    }
}
