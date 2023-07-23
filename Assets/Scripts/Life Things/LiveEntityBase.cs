using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LiveEntityBase : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float _life;
    [SerializeField]
    protected float _maxLife;
    [SerializeField]
    protected ParticleSystem _particleSystem;
    [SerializeField]
    protected ParticleSystem _deathParticleSystem;
    [SerializeField]
    protected AudioClip _hitSound;
    [SerializeField]
    protected AudioClip _deathSound;
    [SerializeField]
    protected AudioSource _audioSource;
    protected const int _constZero = 0;
    [SerializeField]
    protected float _invulnerableTime =0.1f;
    protected bool _invulnerable;

    public virtual void Start()
    {
        if (_maxLife == 0)
        {
            _maxLife = 1;
            Debug.LogError("La variable _maxLife se asigno automaticamente, por favor asignela");
        }
        _life = _maxLife;
    }
    public virtual void TakeDamage(float damage, Vector3 particlePosition)
    {
        if (_invulnerable)
        {
            return;
        }
        if (_life <= damage)
        {
            _life = _constZero;
            Death(particlePosition);
            return;
        }
        _life -= damage;
        _audioSource.clip = _hitSound;
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
            _audioSource.Play();
        Instantiate(_particleSystem);
        _particleSystem.transform.position = particlePosition;
        _invulnerable = true;
        StartCoroutine(Invulnerable(_invulnerableTime));

    }
    public virtual void Death(Vector3 particlePosition)
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.clip = _deathSound;
        _audioSource.Play();
        Instantiate(_deathParticleSystem, particlePosition, transform.rotation);
    }
    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
    private IEnumerator Invulnerable(float invulnerableTime)
    {
        yield return new WaitForSeconds(invulnerableTime);
        _invulnerable = false;
    }

}
