using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsTableUI : MonoBehaviour
{
    public EndTrigger endTrigger;
    public GameObject ResultsMenu;
    public GameObject GamePanel;
    public Text txt;
    private int index = 0;

    void Update()
    {
        if(endTrigger.gameOver) {
            GamePanel.SetActive(false);
            ResultsMenu.SetActive(true);
            Time.timeScale = 0;
            while(index < 3) {

                if(endTrigger.bikes[index] != null) {
                    txt.text += (index + 1).ToString() + ". " + endTrigger.bikes[index].GetComponentInChildren<TextMesh>().text + "\n";
                    index++;
                    if(index == 3) {
                        break;
                    }
                }
                else if(endTrigger.bikes[index] == null) {
                    txt.text += (index + 1).ToString() + ". " + "\n";
                    index++;
                }
            }
            endTrigger.gameOver = false;
        }
    }
}
