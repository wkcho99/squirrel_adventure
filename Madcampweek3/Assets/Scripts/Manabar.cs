using UnityEngine;
using UnityEngine.UI;

public class Manabar : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] public Slider manabar;

    private void Update() {
        float imsi = (float)playerMove.num_skill / (float)3;
        manabar.value = Mathf.Lerp(manabar.value, imsi, Time.deltaTime * 10);
    }

}
