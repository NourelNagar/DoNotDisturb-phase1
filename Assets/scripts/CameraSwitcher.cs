using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Assign the PlayerCameraRoot transform here")]
    public Transform cameraRoot;

    [Header("Heights")]
    public float standHeight = 1.375f;   // default eye height
    public float crouchHeight = 0.4f;    // hiding eye height
    public float transitionSpeed = 5f;

    private float _targetHeight;

    private void Start()
    {
        if (cameraRoot != null)
            standHeight = cameraRoot.localPosition.y;

        _targetHeight = standHeight;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.hKey.wasPressedThisFrame)
            _targetHeight = (_targetHeight == standHeight) ? crouchHeight : standHeight;

        if (cameraRoot != null)
        {
            Vector3 pos = cameraRoot.localPosition;
            pos.y = Mathf.Lerp(pos.y, _targetHeight, Time.deltaTime * transitionSpeed);
            cameraRoot.localPosition = pos;
        }
    }
}
