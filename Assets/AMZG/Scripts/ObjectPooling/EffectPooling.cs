using UnityEngine;
using UnityEngine.Pool;

public class EffectPooler : MonoBehaviour
{
    public static EffectPooler Instance;
    [SerializeField] private ParticleSystem eClickPrefab;

    private ObjectPool<ParticleSystem> _pool;

    void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<ParticleSystem>(
            createFunc: CreateParticle,     
            actionOnGet: OnGetParticle,    
            actionOnRelease: OnReleaseParticle, 
            actionOnDestroy: Destroy,        
            collectionCheck: false,           
            defaultCapacity: 10,              
            maxSize: 20                       
        );
    }

    private ParticleSystem CreateParticle()
    {
        var ps = Instantiate(eClickPrefab, transform);
        var returner = ps.gameObject.AddComponent<ReturnToPool>();
        returner.SetPool(_pool);
        return ps;
    }

    private void OnGetParticle(ParticleSystem ps) => ps.gameObject.SetActive(true);
    private void OnReleaseParticle(ParticleSystem ps) => ps.gameObject.SetActive(false);

    // Hàm để gọi từ PlayerInputHandler
    public void SpawnEffect(Vector3 position)
    {
        var effect = _pool.Get();
        effect.transform.position = position;
        effect.Play();
    }
}