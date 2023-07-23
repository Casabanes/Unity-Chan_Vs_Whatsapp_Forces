using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FootWeapon))]
[RequireComponent(typeof(Katana))]
[RequireComponent(typeof(PlayerMovment))]

public class PlayerAttack
{
    private Animator _animator;
    private PlayerMovment _playerMovment;
    private float _slowSpeed;
    private Katana _weapon;
    protected FootWeapon _foot;
    public PlayerAttack SetFoot(FootWeapon foot)
    {
        _foot = foot;
        return this;
    }
    public PlayerAttack SetWeapon(Katana weapon)
    {
        _weapon = weapon;
        return this;
    }
    public PlayerAttack SetAnimator(Animator animator)
    {
        _animator = animator;
        return this;
    }
    public PlayerAttack SetPlayerMovment(PlayerMovment playerMovment)
    {
        _playerMovment = playerMovment;
        return this;
    }
    public PlayerAttack SetSlowSpeed(float slowSpeed=1)
    {
        _slowSpeed = slowSpeed;
        return this;
    }
    public void KatanaAttack(string attackTriggerAnimatorName)
    {
        _animator.SetTrigger(attackTriggerAnimatorName);
        _playerMovment.IsSlowed(_slowSpeed);
    }
    public void KickAttack(string kickTriggerAnimatorName)
    {
        _animator.SetTrigger(kickTriggerAnimatorName);
        _playerMovment.IsSlowed(_slowSpeed);
    }
    public void ResetWeapon()
    {
        _weapon.OnOffColliders(true);
        _weapon.AttackSound();
    }
    public void TurnOffWeapon()
    {
        _weapon.OnOffColliders(false);
    }
    public void ResetFoot()
    {
        _foot.OnOffColliders(true);
        _foot.AttackSound();
    }
    public void TurnOffFoot()
    {
        _foot.OnOffColliders(false);
    }
}
