using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialScript3 : MonoBehaviour
{

    
public Text ScriptTxt;
public Image ScriptImage;
     
    int cnt = 0;          

    Color color1;
    Color color2;
    string[] intro = new string[]{"가시에 닿으면 다람이가 아파요...ㅠㅠ", "하트를 먹어 다람이의 체력을 회복해요!", "뱀을 밟거나 F를 눌러 마법을 사용해 뱀을 처치해보아요!"+"\n"+"마법은 세번밖에 사용할 수 없으니 신중히 사용하도록 해요" };

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
                Invoke("SetInvisible",3.0f);
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
