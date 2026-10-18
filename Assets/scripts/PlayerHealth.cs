using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float damagePerSecond = 20f;
    public float ghostDamageRadius = 1.5f;

    private float _currentHealth;
    private Transform _ghost;
    private GUIStyle _healthStyle;
    private GUIStyle _deadStyle;
    private bool _isDead = false;

    private Texture2D _bgTex;
    private Texture2D _barTex;

    private void Start()
    {
        _currentHealth = maxHealth;

        _bgTex = new Texture2D(1, 1);
        _bgTex.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 0.85f));
        _bgTex.Apply();

        _barTex = new Texture2D(1, 1);
        _barTex.SetPixel(0, 0, Color.white);
        _barTex.Apply();

        try
        {
            GameObject ghostObj = GameObject.FindGameObjectWithTag("Ghost");
            if (ghostObj != null)
            {
                _ghost = ghostObj.transform;
                Debug.Log("[PlayerHealth] Ghost found: " + ghostObj.name);
            }
            else
                Debug.LogWarning("[PlayerHealth] No GameObject tagged 'Ghost' found. Tag your DemonDoll as Ghost.");
        }
        catch
        {
            Debug.LogWarning("[PlayerHealth] 'Ghost' tag not defined. Add it via Edit > Project Settings > Tags and Layers.");
        }
    }

    private void Update()
    {
        if (_isDead || _ghost == null) return;

        float dist = Vector3.Distance(transform.position, _ghost.position);
        if (dist <= ghostDamageRadius)
        {
            _currentHealth -= damagePerSecond * Time.deltaTime;
            _currentHealth = Mathf.Max(_currentHealth, 0f);

            if (_currentHealth <= 0f)
                Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        Debug.Log("Player died!");
        // Reload scene on death
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void OnGUI()
    {
        if (_healthStyle == null)
        {
            _healthStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                normal = { textColor = Color.white }
            };
        }

        float ratio = _currentHealth / maxHealth;

        // Three-stage color: red → yellow → green
        Color barColor;
        if (ratio > 0.5f)
            barColor = Color.Lerp(new Color(1f, 0.85f, 0f), new Color(0.1f, 0.9f, 0.1f), (ratio - 0.5f) * 2f);
        else
            barColor = Color.Lerp(new Color(0.9f, 0.05f, 0.05f), new Color(1f, 0.85f, 0f), ratio * 2f);

        // Dark background
        GUI.color = Color.white;
        GUI.DrawTexture(new Rect(20, 20, 210, 26), _bgTex);

        // Colored health fill
        GUI.color = barColor;
        GUI.DrawTexture(new Rect(23, 23, 204f * ratio, 20), _barTex);

        // HP label on top
        GUI.color = Color.white;
        GUI.Label(new Rect(26, 20, 204, 26), $"HP  {Mathf.CeilToInt(_currentHealth)} / {Mathf.CeilToInt(maxHealth)}", _healthStyle);
    }
}
