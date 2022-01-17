using UnityEngine;
using UnityEngine.UI;

public class Glidebar : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] public Slider glidebar;

    private void Update() {
        float imsi = (float)(1.0f-playerMove.glideCooldown) / (float)1.0f;
        if(imsi < 0.001) glidebar.value = 0;
        else glidebar.value = Mathf.Lerp(glidebar.value, imsi, Time.deltaTime * 10);
    }

}
