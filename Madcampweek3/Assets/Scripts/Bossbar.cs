using UnityEngine;
using UnityEngine.UI;

public class Bossbar : MonoBehaviour
{
    [SerializeField] private BossMove bossMove;
    [SerializeField] public Slider bossbar;

    private void Update() {
        float imsi = (float)bossMove.health.currentHealth / (float)5;
        bossbar.value = Mathf.Lerp(bossbar.value, imsi, Time.deltaTime * 10);
    }

}
