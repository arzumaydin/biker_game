using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClosingMenuUI : MonoBehaviour
{
    public EndTrigger endTrigger;
    public GameObject ClosingMenu;
    public GameObject ResultsPanel;
    public Text txt;

    public void CloseMenu() {

        ResultsPanel.SetActive(false);
        ClosingMenu.SetActive(true);

        if(endTrigger.firstPlace) {
            txt.text = "YOU WON!";
        }
        else if(endTrigger.secondPlace) {
            txt.text = "2nd Place! Not Bad!";
        }
        else if(endTrigger.thirdPlace) {
            txt.text = "3rd Place! Not Bad!";
        }
        else if(endTrigger.playerLost) {
            txt.text = "YOU LOST!";
        }
    }
}
