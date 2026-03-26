using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class MythicalController : Singleton<MythicalController>
{
    private MapController mapController;
    [SerializeField] private MythicalAvatar mythicalAvatarPrefab;
    private List<MythicalAvatar> mythicalAvatars = new List<MythicalAvatar>();

    void Start()
    {
        mapController = MapController.Instance;
    }

    public void SetUp()
    {
        MythicalAvatar mythicalMain = Instantiate(mythicalAvatarPrefab, transform);
        mythicalMain.arenaController = mapController.mapMain;
        mythicalAvatars.Add(mythicalMain);
        var rtm = mythicalMain.GetComponent<RectTransform>();
        if (rtm != null)
        {
            rtm.anchoredPosition += new Vector2(0f, 0f);
        }

        for (int i = 0; i < mapController.mapArenas.Count; i++)
        {
            MythicalAvatar mythicalAvatar = Instantiate(mythicalAvatarPrefab, transform);
            mythicalAvatar.arenaController = mapController.mapArenas[i];
            var rt = mythicalAvatar.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition += new Vector2(0f, -200f * (i + 1));
            }
            else
            {
                mythicalAvatar.transform.localPosition += new Vector3(0f, -200f * (i + 1), 0f);
            }

            mythicalAvatars.Add(mythicalAvatar);
        }    
    }

    public void CheckMythicalAvatar(ArenaController arena)
    {
        for (int i = 0; i < mythicalAvatars.Count; i++)
        {
            bool isTargetArena = (mythicalAvatars[i].arenaController == arena);
            mythicalAvatars[i].ShowAvatar(isTargetArena);
        }
    }
}
