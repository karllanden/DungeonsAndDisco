using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField]
    public float maxHealth, currentHealth;
    [SerializeField]
    public GameObject DeathAnimation;
    [SerializeField]
    public GameObject PlayerObject;

    [SerializeField]
    public GameObject endScreen;


    public HealthBar healthBar;


    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        healthBar.SetMaxhealth(maxHealth);
        endScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Receives damage
    public void takeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //Plays deathanimation and shows death screen
    void Die()
    {
        Vector3 pos = PlayerObject.transform.position;
        DeathAnimation = GameObject.Instantiate(DeathAnimation, pos, Quaternion.identity);
        PlayerObject.SetActive(false);
        endScreen.SetActive(true);
    }
    //Called to increase players health
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }
}
