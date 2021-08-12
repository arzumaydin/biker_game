using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelUI : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject GamePanel;
    private bool gameStarted = false;
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 0;
        StartMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !gameStarted) {
            StartMenu.SetActive(false);
            Time.timeScale = 1;
            GamePanel.SetActive(true);
            gameStarted = true;
        }
    }
}
