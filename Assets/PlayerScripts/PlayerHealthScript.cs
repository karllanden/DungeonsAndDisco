using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] float maxHealth, currentHealth;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void takeDamage(float damageTaken)
    {
        maxHealth -= damageTaken;
    }

    void Die()
    {

    }

    public void Heal()
    {

    }
}
