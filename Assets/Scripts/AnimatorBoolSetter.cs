using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(Controls))]

public class AnimatorBoolSetter : MonoBehaviour
{
    private int _touchingObjects;
    private string _isInTouchingTheGround = "_isInTouchingTheGround";
    private Animator _animator;
    private int[] _layerFloor;
    private const int _constZero = 0;
    private const int _constOne = 1;
    private PlayerJump _playerJump;
    private Controls _controls;
    public AnimatorBoolSetter SetControls(Controls controls)
    {
        _controls = controls;
        return this;
    }
    public AnimatorBoolSetter SetAnimator(Animator animator)
    {
        _animator = animator;
        return this;
    }
    public AnimatorBoolSetter SetParent(Transform parent)
    {
        transform.parent = parent;
        return this;
    }
    public AnimatorBoolSetter SetPlayerJump(PlayerJump playerJump)
    {
        _playerJump = playerJump;
        return this;
    }
    public AnimatorBoolSetter SetLayerFloor(int[] layerFloor)
    {
        _layerFloor = layerFloor;
        return this;
    }
    private void OnTriggerEnter(Collider other)
    {
        for (int count = 0; count < _layerFloor.Length; count++)
        {
        if(other.gameObject.layer== _layerFloor[count])
        {
            _touchingObjects++;
        }
        }
        if(_touchingObjects> _constZero)
        {
            _animator.SetBool(_isInTouchingTheGround, true);
            _playerJump.ResetJump();
            _controls.IsNotJumpingNow();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        for (int count = 0; count < _layerFloor.Length; count++)
        {
        if (other.gameObject.layer == _layerFloor[count])
        {
            _touchingObjects--;
        }
        }
        if (_touchingObjects < _constOne)
        {
            _animator.SetBool(_isInTouchingTheGround, false);
            _playerJump.HasJumped();
            _controls.IsJumpingNow();
        }
    }
}
