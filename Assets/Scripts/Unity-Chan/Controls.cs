using UnityEngine;
[RequireComponent(typeof(PlayerMovment))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(PlayerAttack))]

public class Controls
{

    private PlayerMovment _playermovment;
    private PlayerJump _playerJump;
    private PlayerAttack _playerAttack;
    private bool _isAttacking;
    private bool _isJumping;
    private KeyCode _attackKey = KeyCode.Mouse0;
    private KeyCode _kickKey = KeyCode.Mouse1;
    private string _attackTriggerAnimatorName = "_attack";
    private string _kickTriggerAnimatorName = "_kicking";
    private bool _lostControl;
    private Player _player;
    public Controls SetPlayerMovment(PlayerMovment playerMovment)
    {
        _playermovment = playerMovment;
        return this;
    }
    public Controls SetPlayerJump(PlayerJump playerJump)
    {
        _playerJump = playerJump;
        return this;
    }
    public Controls SetPlayerAttack(PlayerAttack playerAttack)
    {
        _playerAttack = playerAttack;
        return this;
    }
  
    public void ListenKeys()
    {
        if (!_lostControl)
        {
        if (Input.GetKeyDown(KeyCode.Space) && !_isAttacking)
        {
            _playermovment.IsNotSlowedAnyMore();
            _playerAttack.TurnOffWeapon();
            _playerJump.Jump();
            IsJumpingNow();
        }
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        _playermovment.Move(v, h);
        if (Input.GetKeyDown(_attackKey) && !_isJumping)
        {
            _playerAttack.KatanaAttack(_attackTriggerAnimatorName);

        }
        if (Input.GetKeyDown(_kickKey) && !_isJumping)
        {
            _playerAttack.KickAttack(_kickTriggerAnimatorName);
        }
        }
    }
    public void IsNotAttackingAnyMore()
    {
        _isAttacking = false;
    }
    public void IsJumpingNow()
    {
        _isJumping = true;
    }
    public void IsNotJumpingNow()
    {
        _isJumping = false;
    }

    public void ControlIsLost()
    {
        _lostControl = true;
    }
    public void ResumeControl()
    {
        _lostControl = false;
    }
}
