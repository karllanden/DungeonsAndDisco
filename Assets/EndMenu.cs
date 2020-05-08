using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("MasterV4");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
