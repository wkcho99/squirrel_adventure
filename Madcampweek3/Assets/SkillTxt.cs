using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTxt : MonoBehaviour
{
    public Text Skillname;
    public Image SkillWord;
    public PlayerMove playerMove;
    public RockAttack rockAttack;
    public FireballAttack fireballAttack;
    public ThunderAttack thunderAttack;
    Color color1;
    Color color2;
    // Start is called before the first frame update
    void Start()
    {
        // SkillWord.gameObject.SetActive(false);
        // Skillname.gameObject.SetActive(false);
        color1 = Skillname.color;
        color2 = SkillWord.color;
        color1.a = 0.0f;
        color2.a = 0.0f;
        Skillname.color = color1;
        SkillWord.color = color2;
    }

    // Update is called once per frame
    void Update()
    {
        if(fireballAttack.isAttack0){
            color1.a = 1.0f;
            color2.a = 1.0f;
        Skillname.color = color1;
        SkillWord.color = color2;
            // SkillWord.gameObject.SetActive(true);
            // Skillname.gameObject.SetActive(true);
            Skillname.text = "불이여!";
            Invoke("SetInvisible",0.3f);
        }
        else if(thunderAttack.isAttack1){
            color1.a = 1.0f;
            color2.a = 1.0f;
        Skillname.color = color1;
        SkillWord.color = color2;
            // SkillWord.gameObject.SetActive(true);
            // Skillname.gameObject.SetActive(true);
            Skillname.text = "하늘이여!";
            Invoke("SetInvisible",0.3f);

        }
        else if(rockAttack.isAttack2){
            color1.a = 1.0f;
            color2.a = 1.0f;
        Skillname.color = color1;
        SkillWord.color = color2;
            // SkillWord.gameObject.SetActive(true);
            // Skillname.gameObject.SetActive(true);
            Skillname.text = "땅이여!";
            Invoke("SetInvisible",0.3f);
        }
    }
    void SetInvisible(){
        color1.a = 0.0f;
        color2.a = 0.0f;
        Skillname.color = color1;
        SkillWord.color = color2;
        fireballAttack.isAttack0 = false;
        thunderAttack.isAttack1 = false;
        rockAttack.isAttack2 = false;
    }
}
