using UnityEngine;
using UnityEngine.InputSystem; // Thêm thư viện này

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    void Update()
    {
        if (Mouse.current != null &&
            (Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame))
        {
            MoveToMouseClick();
        }
    }

    private void MoveToMouseClick()
    {
        // Lấy vị trí chuột hiện tại
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = CameraController.Instance.GameCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            if (MainCharacterController.Instance != null)
            {
                MainCharacterController.Instance.MoveToPositionFromClick(hit.point);
            }
        }
    }
}