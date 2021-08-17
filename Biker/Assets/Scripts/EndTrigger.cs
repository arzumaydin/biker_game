using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private int count;
    public bool gameOver = false;
    public bool firstPlace = false;
    public bool secondPlace = false;
    public bool thirdPlace = false;
    public bool playerLost = false;
    public GameObject[] bikes;
    public BikeMovement bm;
    private bool playerArrived = false;
    private int activePlayers = 0;
    private void Start() {
        count = 0;
        bikes = new GameObject[3];
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "AIBike") {
            bikes[count] = other.gameObject;
            count++;
            if(count == 3) {
                playerLost = true;
                gameOver = true;
            }
        }
        else if(!playerArrived && other.tag =="Bicycle"){
            bikes[count] = other.gameObject;
            count++;
            playerArrived = true;
            if(count == 1) {
                firstPlace = true;
            }
            else if(count == 2) {
                secondPlace = true;
            }
            else if(count == 3) {
                thirdPlace = true;
            }
            bm.speed = 0;
            activePlayers = 1 + GameObject.FindGameObjectsWithTag("AIBike").Length;
            if(activePlayers < 3 && activePlayers == count) {
                gameOver = true;
            }
        }
        if(count == 3) {
            gameOver = true;
        }
    }

}
