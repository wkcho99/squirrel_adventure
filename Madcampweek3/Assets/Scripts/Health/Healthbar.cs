using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider HPbar;

    private void Update() {
        float imsi = (float)playerHealth.currentHealth / (float)playerHealth.startingHealth;
        HPbar.value = Mathf.Lerp(HPbar.value, imsi, Time.deltaTime * 10);
    }
}
