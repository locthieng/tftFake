using System;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float countdownTime;
    [SerializeField] public UIProgressCreative uiProgressCreative;
    private float currentTime;
    public bool isRunning = false;
    public bool isActive = false;
    private float countdownTimeStart;

    public static event Action OnProgessEnd;

    void OnEnable()
    {
        ResetAndStart();
        countdownTimeStart = countdownTime;
    }

    void Update()
    {
        if (!isRunning || !isActive) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
                OnProgessEnd?.Invoke();
            }
            UpdateCountdownText(currentTime);
        }
        else
        {
            StopAllCoroutines();
            isRunning = false;
        }
    }

    public void ShowClock(bool b)
    {
        isRunning = b;
        isActive = b;
        uiProgressCreative.SetProgress(0f, countdownTime);
        gameObject.SetActive(b);
        uiProgressCreative.gameObject.SetActive(b);
    }

    public void ResetProgess(Action callback = null)
    {
        countdownTime = countdownTimeStart;
        uiProgressCreative.fill.fillAmount = 1f;
        UpdateCountdownText(countdownTime);
        ResetAndStart();
        callback?.Invoke();
    }

    private void ResetAndStart()
    {
        currentTime = countdownTime;
        isRunning = true;
        UpdateCountdownText(currentTime);
    }

    private void UpdateCountdownText(float timeRemaining)
    {
        timeRemaining = Mathf.Max(0, timeRemaining);
        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        //timeText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        timeText.text = Mathf.FloorToInt(timeRemaining).ToString();
    }
}