using UnityEngine;
using UnityEngine.InputSystem;

public class LampToggle : MonoBehaviour
{
    [Header("Settings")]
    public Light lampLight;
    public float interactionDistance = 3f;

    private Transform _player;
    private bool _showPrompt = false;
    private GUIStyle _promptStyle;

    private void Start()
    {
        if (lampLight == null)
        {
            // Try to find a child named "LampLight" first
            Transform found = transform.Find("LampLight");
            if (found != null)
                lampLight = found.GetComponent<Light>();

            // Fall back to any Light in children
            if (lampLight == null)
                lampLight = GetComponentInChildren<Light>();

            if (lampLight == null)
                Debug.LogWarning("[LampToggle] No Light found. Assign it manually in the Inspector.", this);
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;
        else
            Debug.LogWarning("[LampToggle] No GameObject tagged 'Player' found.", this);
    }

    private void Update()
    {
        if (_player == null || lampLight == null) return;

        float dist = Vector3.Distance(_player.position, transform.position);
        _showPrompt = dist <= interactionDistance;

        if (_showPrompt && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            lampLight.enabled = !lampLight.enabled;
    }

    private void OnGUI()
    {
        if (!_showPrompt || lampLight == null) return;

        if (_promptStyle == null)
        {
            _promptStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };
        }

        string label = lampLight.enabled ? "[E] Turn Off" : "[E] Turn On";
        GUI.Label(new Rect(Screen.width / 2f - 100, Screen.height * 0.65f, 200, 30), label, _promptStyle);
    }
}
