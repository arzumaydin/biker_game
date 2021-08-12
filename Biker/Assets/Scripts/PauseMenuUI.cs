using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject MainPanel;
    public void PauseGame() {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void ResumeGame() {
        MainPanel.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {

        Application.Quit();
    }
}
