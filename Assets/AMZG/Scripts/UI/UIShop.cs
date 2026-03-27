using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class UIShop : MonoBehaviour
{
    [SerializeField] private List<RectTransform> rectHeroInfos;
    private List<HeroInfo> heroInfos = new List<HeroInfo>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void SetUp(HeroData data)
    {
        for (int i = 0; i < rectHeroInfos.Count; i++)
        {
            HeroInfo heroInfo = rectHeroInfos[i].GetComponent<HeroInfo>();
            if (heroInfo != null)
            {
                heroInfos.Add(heroInfo);
            }
        }    

        for (int i = 0; i < heroInfos.Count; i++)
        {
            heroInfos[i].SetUp(data);
        }    
    }    
}
