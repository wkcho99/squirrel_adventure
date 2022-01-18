using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
 public Text ScriptTxt;
 public Image image;
 public Image ring;
    int cnt = 0;          
    string[] intro = new string[]{"혼자 힘으로 도토리를 왕창 모은 다람이는 따뜻한 굴안에서 겨울잠에 들었습니다.", "다람이는..","행복한 꿈을..","꾸었을까요..?"};
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
            if(cnt>=3) 
            {
                cnt = 0;
                ScriptTxt.text = "";
                StartCoroutine(FadeCoroutine());
            }
            else {
                cnt++;
                ScriptTxt.text = intro[cnt];
            }
        }
    }
    IEnumerator FadeCoroutine(){
        float fadeCount = 0;
        while(fadeCount < 1.0f){
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0,0,0,fadeCount);
            ring.color = new Color(255,255,255,fadeCount);
        }
        SceneManager.LoadScene("scene0");
    }
}
