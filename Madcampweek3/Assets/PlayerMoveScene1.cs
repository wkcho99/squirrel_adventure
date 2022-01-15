using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMoveScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 4f;
    public int stagenum = 1;
    Rigidbody2D rigid;
    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        stagenum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject tempObj = null;
        if(Input.GetKeyDown(KeyCode.Space)==true){
            SceneManager.LoadScene("scene2");
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)==true){
            if(stagenum < 4){
                stagenum++;
                tempObj = GameObject.Find("Stage"+stagenum.ToString());
                if(tempObj!=null){
                    Debug.Log("받기 성공");
                    transform.position = Vector2.MoveTowards(transform.position,tempObj.transform.position,speed);
                }
                else{
                    Debug.Log("받기 실패");
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)==true){
            if(stagenum > 1){
                stagenum--;
                tempObj = GameObject.Find("Stage"+stagenum.ToString());
                if(tempObj!=null){
                    Debug.Log("받기 성공");
                    transform.position = Vector2.MoveTowards(transform.position,tempObj.transform.position,speed);
                }
                else{
                    Debug.Log("받기 실패");
                }
            }
        }
    }
}
