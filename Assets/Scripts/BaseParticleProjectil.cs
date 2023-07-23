using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Movment))]

public abstract class BaseParticleProjectil : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem _projectil;
    [SerializeField]
    protected ParticleSystem _projectilTrail;
    [SerializeField]
    protected ParticleSystem _explosion;
    [SerializeField]
    protected Rigidbody _colliderRb;
    [SerializeField]
    protected float _lifeTime;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _timeLife;
    [SerializeField]
    protected Vector3 _direction;
    [SerializeField]
    protected SphereCollider _sc;
    [SerializeField]
    protected AudioSource _audioSource;
    [SerializeField]
    protected AudioClip _explosionClip;
    private Movment _move;
    private bool _itHit;


    protected virtual void Start()
    {
        _move = new Movment().SetRigidBody(_colliderRb).SetSpeed(_speed);
        _projectil.Play();
        _projectilTrail.Play();
        StartCoroutine(End());
    }
    protected virtual void Update()
    {
        _move.Move(_direction);
        if (_itHit)
        {
            DestroyThis();
        }
    }
    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
        damageable.TakeDamage(_damage,collision.GetContact(0).point);
        Destroy(_projectil);
        Destroy(_projectilTrail);
        _sc.enabled = false;
        _audioSource.clip = _explosionClip;
        _audioSource.Play();
        _explosion.Play();
        _itHit = true;
        }
    }
    protected virtual IEnumerator End()
    {
        yield return new WaitForSeconds(_timeLife);
        _audioSource.clip = _explosionClip;
        _audioSource.Play();
        _itHit = true;
    }
    protected virtual void DestroyThis()
    {
        if (_audioSource.isPlaying==false)
        {
            Destroy(gameObject);
        }
    }
}
