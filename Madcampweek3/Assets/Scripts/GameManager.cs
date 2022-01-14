using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;

    public PlayerMove player;
    public GameObject[] Stages;
    //private Health health = player.GetComponent<Health>();
    
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

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player"){
            //Health Down
            //health.TakeDamage(1);
            //Player Reposition
            PlayerReposition();
        }
    }

    void PlayerReposition(){
        player.transform.position = new Vector3(0, 0, 0);
        player.VelocityZero();
    }

}
