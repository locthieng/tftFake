using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HeroesDatabase", menuName = "HeroesDatabase")]
public class HeroesDatabase : ScriptableObject
{
    public List<HeroData> HeroDatas;
}
