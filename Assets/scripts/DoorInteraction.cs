using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteraction : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 3f;
    public float interactionDistance = 5f;

    [Header("Pivot Override")]
    [Tooltip("Drag the 'door' child here. Leave empty to auto-detect.")]
    public Transform doorPivot;

    private bool _isOpen = false;
    private bool _isMoving = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Transform _player;
    private bool _showPrompt = false;
    private GUIStyle _promptStyle;

    // Collider on the parent frame that blocks passage when the door child rotates away
    private Collider _frameCollider;

    private void Start()
    {
        // Auto-find the pivot: prefer a child named "door", else use self
        if (doorPivot == null)
        {
            Transform found = transform.Find("door");
            doorPivot = found != null ? found : transform;
        }

        _closedRotation = doorPivot.rotation;
        _openRotation = _closedRotation * Quaternion.Euler(0, openAngle, 0);

        // If the pivot is a child, the parent may have its own collider that doesn't
        // rotate with the door — find it so we can disable it when the door is open.
        if (doorPivot != transform)
        {
            _frameCollider = GetComponent<Collider>();
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;
        else
            Debug.LogWarning("[DoorInteraction] No GameObject tagged 'Player' found.", this);
    }

    private void Update()
    {
        if (_player == null) return;

        float dist = Vector3.Distance(_player.position, transform.position);
        _showPrompt = !_isMoving && dist <= interactionDistance;

        if (_showPrompt && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            StopAllCoroutines();
            StartCoroutine(RotateDoor(_isOpen ? _closedRotation : _openRotation));
            _isOpen = !_isOpen;
        }
    }

    private IEnumerator RotateDoor(Quaternion target)
    {
        _isMoving = true;

        // Disable the frame collider immediately when opening so the player
        // isn't blocked the moment they step through the doorway.
        if (_frameCollider != null)
            _frameCollider.enabled = _isOpen; // _isOpen is still the OLD state here:
                                              // true  → closing → re-enable collider
                                              // false → opening → disable collider

        while (Quaternion.Angle(doorPivot.rotation, target) > 0.05f)
        {
            doorPivot.rotation = Quaternion.Slerp(doorPivot.rotation, target, Time.deltaTime * openSpeed);
            yield return null;
        }
        doorPivot.rotation = target;
        _isMoving = false;
    }

    private void OnGUI()
    {
        if (!_showPrompt) return;

        if (_promptStyle == null)
        {
            _promptStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };
        }

        string label = _isOpen ? "[E] Close" : "[E] Open";
        GUI.Label(new Rect(Screen.width / 2f - 100, Screen.height * 0.65f, 200, 30), label, _promptStyle);
    }
}
