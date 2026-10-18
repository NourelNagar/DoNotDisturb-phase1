using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    [Header("Settings")]
    public string itemName = "Item";
    public float interactionDistance = 3f;

    private static int _itemsCollected = 0;

    private Transform _player;
    private bool _showPrompt = false;
    private GUIStyle _promptStyle;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;
    }

    private void Update()
    {
        if (_player == null) return;

        float dist = Vector3.Distance(_player.position, transform.position);
        _showPrompt = dist <= interactionDistance;

        if (_showPrompt && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            PickUp();
    }

    private void PickUp()
    {
        _itemsCollected++;
        Debug.Log($"Picked up {itemName}. Total items: {_itemsCollected}");
        gameObject.SetActive(false);
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

        GUI.Label(new Rect(Screen.width / 2f - 100, Screen.height * 0.65f, 200, 30),
            $"[E] Pick up {itemName}", _promptStyle);
    }

    public static int GetItemsCollected() => _itemsCollected;
}
