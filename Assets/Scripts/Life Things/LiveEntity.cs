using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class LiveEntity : LiveEntityBase, IObservableToGenericBar,IKnockBackable
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _damageTrigger = "_stuned";
    [SerializeField]
    private string _deathTrigger = "_death";
    private List<IObserverGenericBar> _lifeBar=new List<IObserverGenericBar>();
    [SerializeField]
    private CapsuleCollider _cc;
    [SerializeField]
    private Rigidbody _rb;
    private List<IObserverGenericBar> _observers = new List<IObserverGenericBar>();
    private IObserverGenericBar _observerWhenDies;
    public override void Start()
    {
        base.Start();
            _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Error semi fatal, el componente Animator no está asignado a este objeto");
            return;
        }
        _cc = GetComponent<CapsuleCollider>();
        if (_cc == null)
        {
            Debug.LogError("Error semi fatal, el componente CapsuleCollider no está asignado a este objeto");
            return;
        }
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Error semi fatal, el componente Rigidbody no está asignado a este objeto");
            return;
        }
    }

    public override void TakeDamage(float damage, Vector3 particlePosition)
    {
        base.TakeDamage(damage, particlePosition);
        _animator.SetTrigger(_damageTrigger);
        for (int count = 0; count < _lifeBar.Count; count++)
        {
            _lifeBar[count].RefreshValue(_life);
        }
        for (int i = 0; i < _observers.Count; i++)
        {
            _observers[i].RefreshValue(_life);
        }
    }
    public override void Death(Vector3 particlePosition)
    {
        base.Death(particlePosition);
        _animator.SetBool(_deathTrigger,true);
        _rb.isKinematic = true;
        _cc.enabled = false;
        if (_observerWhenDies != null)
        {
            _observerWhenDies.NotifyBarIsEmpty(true);
        }
    }

    public void Suscribe(IObserverGenericBar observer)
    {
        _lifeBar.Add(observer);
        for (int count = 0; count < _lifeBar.Count; count++)
        {
            _lifeBar[count].SetMaxValue(_maxLife);
        }
    }
    public void KnockBack(Vector3 direction, float force)
    {
        _rb.AddForce(direction * force, ForceMode.Impulse);
    }
    public void NotifyValueToObservers(float value)
    {
        for (int i = 0; i < _observers.Count; i++)
        {
            _observers[i].RefreshValue(value);
        }
    }

    public void NotifyIsEmptyToObserver(bool barIsEmpty)
    {
        if (_life == _constZero)
        {
            _observerWhenDies.NotifyBarIsEmpty(barIsEmpty);
        }
    }
}
