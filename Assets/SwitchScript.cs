using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    //Jeff Code

    [SerializeField]
    private GameObject UIElement;
    [SerializeField]
    private GameObject armUp;
    [SerializeField]
    private GameObject armDown;
    [SerializeField]
    private GameObject Unlockobject1;
    [SerializeField]
    private GameObject Unlockobject2;
    [SerializeField]
    private GameObject Unlockobject3;

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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchPulled = true;
                armUp.SetActive(false);
                armDown.SetActive(true);
                UIElement.SetActive(false);
                Unlockobject1.transform.position += new Vector3(0, -8, 0);
                if (Unlockobject2 != null)
                {
                    Unlockobject2.SetActive(false);
                }
                if (Unlockobject3 != null)
                {
                    Unlockobject3.SetActive(false);
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
