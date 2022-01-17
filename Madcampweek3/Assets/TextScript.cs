using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TextScript : MonoBehaviour
{
    
public Text ScriptTxt;
    int Gold = 0;          
    int cnt = 0;          
    string[] intro = new string[]{"옛날 옛적, 숲속 마을에 한 아기다람쥐가 살았다.", "아기다람쥐 다람이는 상냥한 부모님과 함께 행복하게 살았는데..","어느날 먹이를 구하러 나가신 부모님이 영영 돌아오지 않으셨고,","다람이는 혼자가 되었다.","혼자가 된 다람이의 첫 겨울나기를 도와주자!" };
    // Use this for initialization
    void Start()
    {
        ScriptTxt.text = intro[0];
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space)==true)
        {
            if(cnt>=4) 
            {
                cnt = 0;
                ScriptTxt.text = "";
                SceneManager.LoadScene("1");
            }
            else {
                cnt++;
                ScriptTxt.text = intro[cnt];
            }
        }
    }
}
