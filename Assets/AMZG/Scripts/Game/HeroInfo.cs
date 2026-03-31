using UnityEngine;
using UnityEngine.UI;

public class HeroInfo : MonoBehaviour
{
    public Image avatar;
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI clanType;
    public TMPro.TextMeshProUGUI classesType;
    public TMPro.TextMeshProUGUI price;
    [SerializeField] private UIButton button;

    public void SetUp(HeroData data)
    {
        avatar.sprite = data.Icon;
        nameText.text = data.Name;
        clanType.text = data.Clan.ToString();
        classesType.text = data.Classes.ToString();
        price.text = data.Price.ToString();
    }  
    
    public void OnClick()
    {
        // spawn hero on queue, off enable button, null hero data
        gameObject.SetActive(false);
    }
}
