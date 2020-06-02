using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    public Transform[] hands;
    void Start()
    {
        hands = new Transform[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            InventoryPickUp();

        InventorySwitch();
        if (transform.parent == null)
        {
            player = null;
        }

            Drop();
        
    }
    //Söker efter Spelaren och kollar om spelarens child objekt (LeftHand och RightHand) har inga child objekts
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
                            gameObject.transform.position = hands[0].transform.position;
                            gameObject.transform.rotation = hands[0].transform.rotation;
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
                            gameObject.transform.rotation = hands[1].transform.rotation;
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
    public void Drop()
    {
        if (Input.GetKey(KeyCode.C) && hands[0] != null)
        {
            Transform tempObject = transform.parent;
            tempObject.DetachChildren();
            hands[0] = null;

        }
        if (Input.GetKey(KeyCode.V) && hands[1] != null)
        {
            Transform tempObject = transform.parent;
            tempObject.DetachChildren();
            hands[1] = null;

        }
    }
    //När vapnen är uppplockad ska vapnen kolla om vilken hand är parent objekten och bytta till den andra hand parent
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
                    gameObject.transform.rotation = hands[i].transform.rotation;
                }
                else if (hands[i] != null)
                {
                    hands[i] = null;
                }
            }
        }
    }
}