using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialScript2 : MonoBehaviour
{
    public Text ScriptTxt;
public Image ScriptImage;
 Color color1;
    Color color2;
    // Start is called before the first frame update
    void Start()
    {
        color1 = ScriptTxt.color;
        color2 = ScriptImage.color;
        Invoke("SetInvisible",1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        void SetInvisible(){
        color1.a = 0.0f;
        color2.a = 0.0f;
        ScriptTxt.color = color1;
        ScriptImage.color = color2;
    }

}
