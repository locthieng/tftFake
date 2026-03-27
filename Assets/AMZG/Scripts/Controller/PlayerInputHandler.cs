using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    public static event System.Action<Vector2> OnPointerDownScreen;
    public static event System.Action<Vector2> OnPointerUpScreen;
    public static event System.Action<Vector3> OnPointerDownWorld;

    void Update()
    {
        if (Mouse.current == null)
            return;

        // Cập nhật trạng thái click dựa trên Input System
        if (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Phát event screen position
            OnPointerDownScreen?.Invoke(mousePosition);

            Ray ray = CameraController.Instance.GameCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                OnPointerDownWorld?.Invoke(hit.point);
                if (EffectPooler.Instance != null)
                {
                    EffectPooler.Instance.SpawnEffect(hit.point);
                }
                if (MainCharacterController.Instance != null)
                {
                    MainCharacterController.Instance.MoveToPositionFromClick(hit.point);
                }
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame || Mouse.current.rightButton.wasReleasedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            OnPointerUpScreen?.Invoke(mousePosition);
        }
    }
}