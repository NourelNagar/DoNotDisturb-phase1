using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public Light flashlight;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponentInChildren<Light>();

        if (flashlight == null)
        {
            Debug.LogWarning("[FlashlightController] No Light found. Assign it in the Inspector.");
            return;
        }

        flashlight.enabled = false;
    }

    void Update()
    {
        if (flashlight == null) return;

        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
            flashlight.enabled = !flashlight.enabled;
    }
}
