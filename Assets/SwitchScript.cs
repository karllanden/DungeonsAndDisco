using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject UIElement;
    [SerializeField]
    private GameObject armUp;
    [SerializeField]
    private GameObject armDown;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject roof;

    bool switchActive;
    bool SwitchPulled;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SwitchPulled == false)
            {
                UIElement.SetActive(true);
            }
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SwitchPulled = true;
                armUp.SetActive(false);
                armDown.SetActive(true);
                UIElement.SetActive(false);
                door.transform.position += new Vector3(0, -4, 0);
                if (roof != null)
                {
                    roof.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SwitchPulled == false)
            {
                UIElement.SetActive(false);
            }
        }
    }
}
