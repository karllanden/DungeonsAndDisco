using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] public float maxHealth, currentHealth;


    public HealthBar healthBar;


    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        healthBar.SetMaxhealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        //healthBar.fillAmount = currentHealth / maxHealth;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(2);
        }
    }

    public void takeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }

    public void Heal()
    {

    }
}
