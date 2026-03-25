using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [SerializeField] private CanvasScaler[] canvasScalers;
    [SerializeField] private Image cover;
    [SerializeField] private Image freezingFrame;
    [SerializeField] private InGameUI ingameUI;
    public static bool IsUIMatchWidth
    {
        get
        {
            return true;
        }
    }

    private void Start()
    {
        for (int i = 0; i < canvasScalers.Length; i++)
        {
            canvasScalers[i].matchWidthOrHeight = IsUIMatchWidth ? 0 : 1;
        }
        cover.gameObject.SetActive(true);
    }

    public void FadeCover(Action callback)
    {
        LeanTween.alpha(cover.rectTransform, 0, 0.5f).setOnComplete(() =>
        {
            cover.raycastTarget = false;
            callback?.Invoke();
        });
    }

    int tweenFreezingID;
    public void FadeFreezingFrame(float alpha)
    {
        if (tweenFreezingID > -1)
        {
            LeanTween.cancel(tweenFreezingID);
        }
        tweenFreezingID = LeanTween.alpha(freezingFrame.rectTransform, alpha, 0.2f).id;
    }
}
