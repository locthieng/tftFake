using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool : MonoBehaviour
{
    private IObjectPool<ParticleSystem> _pool;
    private ParticleSystem _ps;

    void Awake() => _ps = GetComponent<ParticleSystem>();

    public void SetPool(IObjectPool<ParticleSystem> pool) => _pool = pool;

    void OnParticleSystemStopped()
    {
        if (_pool != null)
        {
            _pool.Release(_ps);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}