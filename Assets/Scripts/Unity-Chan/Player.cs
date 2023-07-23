using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Controls))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(PlayerMovment))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(AnimatorBoolSetter))]
[RequireComponent(typeof(Katana))]
[RequireComponent(typeof(FootWeapon))]


public class Player : MonoBehaviour, IObserverGenericBar
{
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Controls _controls;
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _slowSpeed = 2;
    [SerializeField]
    private float _jumpForce = 10;
    [SerializeField]
    private Transform _animatorBoolSetterParent;
    private PlayerMovment _playerMovment;
    private PlayerAttack _playerAttack;
    private PlayerJump _playerJump;
    [SerializeField]
    private AnimatorBoolSetter _animatorBoolSetter;
    [SerializeField]
    private int[] _layerFloor;
    [SerializeField]
    private Katana _weapon;
    [SerializeField]
    protected FootWeapon _foot;
    [SerializeField]
    private bool _isDead;
    private const int _constZero = 0;
    [SerializeField]
    IObservableToGenericBar _liveEntity;
    [SerializeField]
    private float _stunedTime = 0.1f;

    void Start()
    {
        if (_layerFloor == null)
        {
            _layerFloor = new int[0];
            _layerFloor[0] = 8;
            Debug.LogError("La variable '_layerFloor' se asigno automaticamente, en caso de error asigne la variable manualmente");
        }
        _rigidBody = GetComponent<Rigidbody>();
        if (_rigidBody == null)
        {
            Debug.LogError("Error fatal, el objeto Player no tiene RigidBody asignado");
            return;
        }
        _weapon = GetComponentInChildren<Katana>();
        if (_weapon == null)
        {
            Debug.LogError("Error fatal, el objeto Player no tiene Katana asignado");
            return;
        }
        if (_animatorBoolSetterParent == null)
        {
            _animatorBoolSetterParent = Instantiate(_animatorBoolSetterParent);
            Debug.LogError("Error fatal, el objeto player no tiene AnimatorBoolSetterParent asignado, puede no funcionar correctamente");
            return;
        }
        if (_animatorBoolSetter == null)
        {
            Debug.LogError("Error semi fatal, el objeto player no tiene AnimatorBoolSetter asignado");
            Instantiate(_animatorBoolSetter, _animatorBoolSetterParent.position, _animatorBoolSetterParent.rotation);
            _animatorBoolSetter.transform.parent = _animatorBoolSetterParent;
            return;
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Error fatal, el objeto player no tiene Animator asignado");
            return;
        }

        _playerMovment = new PlayerMovment().SetAnimator(_animator)
                                            .SetRigidBody(_rigidBody)
                                            .SetSpeed(_speed)
                                            .SetTransform(transform);

        _playerJump = new PlayerJump().SetJumpForce(_jumpForce)
                                      .SetRigidBody(_rigidBody);

        _playerAttack = new PlayerAttack().SetAnimator(_animator)
                                          .SetPlayerMovment(_playerMovment)
                                          .SetSlowSpeed(_slowSpeed)
                                          .SetWeapon(_weapon)
                                          .SetFoot(_foot);

        _controls = new Controls().SetPlayerMovment(_playerMovment)
                                  .SetPlayerAttack(_playerAttack)
                                  .SetPlayerJump(_playerJump);

        if (_animatorBoolSetter != null)
        {
            Instantiate(_animatorBoolSetter).SetParent(_animatorBoolSetterParent)
                                            .SetPlayerJump(_playerJump)
                                            .SetAnimator(_animator)
                                            .SetLayerFloor(_layerFloor)
                                            .SetControls(_controls);
        }
        _liveEntity = GetComponent<IObservableToGenericBar>();
        if (_liveEntity != null)
        {
            _liveEntity.Suscribe(this);
        }
        else
        {
            Debug.LogError("Error fatal, no se encontró el componente LiveEntity en el objeto player");
            return;
        }
    }

    void Update()
    {
        if (!_isDead)
        {
            _controls.ListenKeys();
        }
        else
        {
            EventManager.Trigger(EventManager.EventType.GameOver);
        }
    }
    public void RemoveSlow()
    {
        _playerMovment.IsNotSlowedAnyMore();
    }
    public void IsAttacking()
    {
        _playerAttack.ResetWeapon();
        _playerAttack.ResetFoot();
    }
    public void IsNotAttacking()
    {
        _playerAttack.TurnOffWeapon();
    }
    public void IsKicking()
    {
        _playerAttack.ResetFoot();
    }
    public void IsNotKicking()
    {
        _playerAttack.TurnOffFoot();
    }

    public void RefreshValue(float actualLife)
    {
        if (actualLife <= _constZero)
        {
            _isDead = true;
        }
        else
        {
            StartCoroutine(GetHited());
        }
    }
    private IEnumerator GetHited()
    {
        _controls.ControlIsLost();
        yield return new WaitForSeconds(_stunedTime);
        _controls.ResumeControl();
    }

    public void SetMaxValue(float value)
    {
        return;
    }

    public void NotifyBarIsEmpty(bool barIsEmpty)
    {
        return;
    }
}