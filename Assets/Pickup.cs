using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    public Transform[] hands;
    bool E_isPressed, Q_ispressed;
    void Start()
    {
        //hands = GetComponentsInChildren<Transform>();
        E_isPressed = Input.GetKey(KeyCode.E);
        Q_ispressed = Input.GetKey(KeyCode.Q);
        hands = new Transform[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            InventoryPickUp();

        InventorySwitch();
    }

    void InventoryPickUp()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider hit in hits)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                if (hit.CompareTag("Player"))
                {
                    player = hit.gameObject;
                    if (hands[0] == null)
                    {
                        hands[0] = player.GetComponentInParent<Transform>().GetChild(0);
                        if (hands[0].childCount == 0)
                        {
                            transform.SetParent(hands[0]);
                            Debug.Log("Open hand");
                            gameObject.transform.position = hands[0].transform.position;
                        }

                        else
                        {
                            hands[0] = null;
                            player = null;
                        }
                    }
                }
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if (hit.CompareTag("Player"))
                {
                    player = hit.gameObject;
                    if (hands[1] == null)
                    {
                        hands[1] = player.GetComponentInParent<Transform>().GetChild(1);
                        if (hands[1].childCount == 0)
                        {

                            transform.SetParent(hands[1]);
                            gameObject.transform.position = hands[1].transform.position;
                        }
                        else
                        {
                            hands[1] = null;
                            player = null;
                        }
                    }
                }
            }


        }


    }
    void InventorySwitch()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < 2; i++)
            {
                if (hands[i] == null)
                {
                    hands[i] = player.GetComponentInParent<Transform>().GetChild(i);
                    transform.SetParent(hands[i]);
                    gameObject.transform.position = hands[i].transform.position;
                }
                else if (hands[i] != null)
                {
                    hands[i] = null;
                }
            }
        }
    }
}