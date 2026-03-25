using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;

    internal void SetActive(bool isActive)
    {
        if (!isActive)
        {
            canvas.blocksRaycasts = false;
        }
        canvas.LeanAlpha(isActive ? 1 : 0, 0.2f).setOnComplete(() =>
        {
            if (isActive)
            {
                canvas.blocksRaycasts = true;
            }
        });
    }
}
