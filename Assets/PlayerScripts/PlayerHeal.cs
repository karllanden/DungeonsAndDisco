using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    [SerializeField]
    float healAmount;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            other.GetComponentInParent<PlayerHealthScript>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
