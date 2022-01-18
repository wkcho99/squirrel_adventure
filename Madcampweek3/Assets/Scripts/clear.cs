using UnityEngine;

public class clear : MonoBehaviour
{
    [SerializeField] BossMove bossMove;
    [SerializeField] GameObject finish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossMove.health.currentHealth <= 0)
            finish.gameObject.SetActive(true);
    }
}
