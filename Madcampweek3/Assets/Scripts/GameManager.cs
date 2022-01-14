using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;

    public PlayerMove player;
    public GameObject[] Stages;
    
    public void NextStage()
    {
        if(stageIndex < Stages.Length -1){
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
        }else{//Game Clear
            //Player Control Lock
            Time.timeScale = 0;
            //Result UI
            Debug.Log("클리어");
            //Restart Button UI

        }

        //Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }
    public void HealthDown(int changehealth){
        health = changehealth;
        if(health > 1)
            health--;
        else{
            //Player Die Effect
            player.OnDie();

            //ResultUI
            Debug.Log("죽었습니다");
            //Retry Button

        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player"){
            //Health Down
            HealthDown(1);
            //Player Reposition
            PlayerReposition();
        }
    }

    void PlayerReposition(){
        player.transform.position = new Vector3(0, 0, 0);
        player.VelocityZero();
    }

}
