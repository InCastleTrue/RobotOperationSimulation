using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Player player;
    public Slider healthSlider;
    public Slider expSlider;
    public float Smoothness = 5.0f;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        if (healthSlider == null || expSlider == null)
        {
            Debug.LogError("Slider reference not set in the inspector.");
        }
        healthSlider.maxValue = player.playerMaxHp;
        expSlider.maxValue = player.maxExp;

    }

    private void Update()
    {

        float targetHealth = player.playerHp;

        healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, Time.deltaTime * Smoothness);

        float targetExp = player.exp;
        healthSlider.maxValue = player.playerMaxHp;

        expSlider.value = Mathf.Lerp(expSlider.value, targetExp, Time.deltaTime * Smoothness);

        expSlider.maxValue = player.maxExp;
    }
}