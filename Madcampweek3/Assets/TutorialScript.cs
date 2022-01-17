using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialScript : MonoBehaviour
{

    
public Text ScriptTxt;
public Image ScriptImage;
     
    int cnt = 0;          

    Color color1;
    Color color2;
    string[] intro = new string[]{"좌우방향키 또는 A,D를 이용해 다람이를 움직여 보아요!", "Space바를 눌러 점프하세요!", "도토리를 3개 다 모아야 다음 스테이지로 넘어갈 수 있어요!" };

    [SerializeField] PlayerMove player;
    // Use this for initialization
    void Start()
    {
        color1 = ScriptTxt.color;
        color2 = ScriptImage.color;
        ScriptTxt.text = intro[0];
        Invoke("SetInvisible",1.5f);
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (player.isTutorial1==true)
        {
            if(cnt>=3) 
            {
                cnt = 0;
                SetInvisible();
                ScriptTxt.text = "";
            }
            else {
                cnt++;
                color1.a = 1.0f;
                color2.a = 1.0f;
                ScriptTxt.color = color1;
                ScriptImage.color = color2;
                ScriptTxt.text = intro[cnt];
                player.isTutorial1 = false;
                Invoke("SetInvisible",1.5f);
            }
        }
    }
    void SetInvisible(){
        color1.a = 0.0f;
        color2.a = 0.0f;
        ScriptTxt.color = color1;
        ScriptImage.color = color2;
    }

}
