using UnityEngine;

public class EconomyController : MonoBehaviour
{
    public static EconomyController Instance { get; private set; }

    // Biến lưu trữ số tiền thực tế trong trận
    [SerializeField] private int _currentGold;

    // Event để báo cho Shop và UI biết khi tiền thay đổi
    public static event System.Action<int> OnGoldChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Hàm lấy số dư (để các script khác check)
    public int GetCurrentGold() => _currentGold;

    // Hàm cộng tiền (Hết vòng đấu, bán tướng...)
    public void AddGold(int amount)
    {
        _currentGold += amount;
        OnGoldChanged?.Invoke(_currentGold);
    }

    // Hàm tiêu tiền (Mua tướng, Roll shop, Lên cấp)
    public bool SpendGold(int amount)
    {
        if (_currentGold >= amount)
        {
            _currentGold -= amount;
            OnGoldChanged?.Invoke(_currentGold);
            return true; // Giao dịch thành công
        }
        return false; // Không đủ tiền sếp ơi!
    }
}