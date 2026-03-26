using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MythicalAvatar : MonoBehaviour
{
    [SerializeField] private Image avatar;
    [SerializeField] private UIButton UIButton;
    [SerializeField] public ArenaController arenaController;

    public void MoveToArenaOfMythical()
    {
        if (arenaController == null) return;

        MapController.Instance.ViewMapOnly(arenaController);
    }

    public void ShowAvatar(bool isShow)
    {
        if (isShow)
        {
            LeanTween.scale(avatar.gameObject, Vector2.one * 0.8f, 0.1f);
        }
        else
        {
            LeanTween.scale(avatar.gameObject, Vector2.one , 0.1f);
        }
    }    
}
