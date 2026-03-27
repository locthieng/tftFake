using UnityEngine;
using UnityEngine.Pool;

public class EffectPooler : MonoBehaviour
{
    public static EffectPooler Instance;
    [SerializeField] private ParticleSystem eClickPrefab;

    // Đối tượng Pool chính
    private ObjectPool<ParticleSystem> _pool;

    void Awake()
    {
        Instance = this;
        // Khởi tạo Pool
        _pool = new ObjectPool<ParticleSystem>(
            createFunc: CreateParticle,       // Cách tạo mới 1 cái
            actionOnGet: OnGetParticle,       // Lệnh chạy khi lấy ra (Active)
            actionOnRelease: OnReleaseParticle, // Lệnh chạy khi trả về (Deactive)
            actionOnDestroy: Destroy,         // Lệnh chạy khi pool đầy/xóa bớt
            collectionCheck: false,           // Kiểm tra lỗi logic (tắt để tăng tốc)
            defaultCapacity: 10,              // Số lượng khởi tạo dự kiến
            maxSize: 20                       // Số lượng tối đa lưu trong pool
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