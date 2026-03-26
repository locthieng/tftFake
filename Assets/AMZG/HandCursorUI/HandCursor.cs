using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandCursor : MonoBehaviour
{
    [Header("Hand Settings")]
    public RectTransform handIcon;
    public float scaleNormal = 1f;
    public float scalePressed = 0.85f;
    public float tweenTime = 0.1f;
    [SerializeField] private ParticleSystem clickEffect;
    private Canvas canvas;

    void Start()
    {
        if (handIcon == null) handIcon = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        handIcon.localScale = Vector3.one * scaleNormal;
        // ?n con tr? chu?t m?c ??nh n?u mu?n
        Cursor.visible = false;
    }

    void OnEnable()
    {
        PlayerInputHandler.OnPointerDownScreen += HandlePointerDownScreen;
        PlayerInputHandler.OnPointerUpScreen += HandlePointerUpScreen;
    }

    void OnDisable()
    {
        PlayerInputHandler.OnPointerDownScreen -= HandlePointerDownScreen;
        PlayerInputHandler.OnPointerUpScreen -= HandlePointerUpScreen;
    }

    private void HandlePointerDownScreen(Vector2 screenPos)
    {
        LeanTween.scale(handIcon, Vector3.one * scalePressed, tweenTime)
                 .setEaseOutQuad();

        if (clickEffect != null)
        {
            ParticleSystem e = Instantiate(clickEffect, handIcon.transform);
            e.gameObject.SetActive(true);
            e.Play();
        }
    }

    // Event handler khi pointer up
    private void HandlePointerUpScreen(Vector2 screenPos)
    {
        LeanTween.scale(handIcon, Vector3.one * scaleNormal, tweenTime)
                 .setEaseOutBack();
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out pos
        );
        handIcon.localPosition = pos;
    }
}
