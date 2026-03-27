using UnityEngine;

[System.Serializable]
public class HeroData
{
    public string Name;
    public Sprite Icon;
    public ClanType Clan;
    public ClassesType Classes;
    public int Price;
}

public enum ClanType
{
    Fated,
    Heavenly,
    Ghostly,
    Mythic,
    Storyweaver,
    Inkshadow,
    Dragonlord,
    Umbral,
    Porcelain,
    Arcanist
}

public enum ClassesType
{
    Behemoth,
    Warden,
    Duelist,
    Reaper,
    Sniper,
    Arcanist,
    Invoker,
    Sage
}

public class HeroesController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
