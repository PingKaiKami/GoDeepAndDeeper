using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            
        }
    }
    public void Resume()
    { 
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
