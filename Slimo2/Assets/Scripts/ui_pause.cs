using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* allows players to bring up a menu displaying all the controls. 
 * Does not pause all functions in the game.
*/
public class ui_pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            AudioListener.pause = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseGame();
        }
    }
}
