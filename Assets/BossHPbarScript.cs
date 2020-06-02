using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPbarScript : MonoBehaviour
{
    public Slider slider;
    public GameObject healthBar;
    public AiProcessing AiProcess;
    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color lowColor;

    Image healthBarImage;

    private float maxHealth, currentHealth;

    //public void Start()
    //{
    //    maxHealth = 500;
    //    currentHealth = 500;
    //    slider.maxValue = maxHealth;
    //    slider.value = currentHealth;
    //    healthBarImage = healthBar.GetComponent<Image>();
    //}

    public void SetMaxhealth(float health)
    {
        maxHealth = health;
        slider.maxValue = health;
        slider.value = health;
        healthBarImage = healthBar.GetComponent<Image>();
        
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        Debug.Log(slider.value);
        healthBarImage.color = Color.Lerp(lowColor, fullColor, (slider.value / maxHealth));
        Color tempColor = healthBarImage.color;
        tempColor.a = 1f;
        healthBarImage.color = tempColor;

    }
    //public void Update()
    //{

    //    //var barRenderer = healthBar.GetComponent<Renderer>();

    //}

}
