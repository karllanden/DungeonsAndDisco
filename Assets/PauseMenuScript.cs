using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    
    public void Restart()
    {
        PlayerController.gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        PlayerController.gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        //Application.Quit();
    }
    public void Resume()
    {
        PlayerController.gameIsPaused = false;
        this.GetComponentInParent<PauseMenuScript>().gameObject.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game paused");
    }
}
