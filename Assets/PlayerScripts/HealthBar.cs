using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Hampus
    public Slider slider;
    public GameObject healthBar;
    public PlayerHealthScript playerHealth;
    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color lowColor;

    Image healthBarImage;



    //Sätter spelarens maxhälsa
    public void SetMaxhealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthBarImage = healthBar.GetComponent<Image>();
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        healthBarImage.color = Color.Lerp(lowColor, fullColor, (slider.value/100));

    }

      
}
