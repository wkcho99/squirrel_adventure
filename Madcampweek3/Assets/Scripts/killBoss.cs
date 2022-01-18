using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killBoss : MonoBehaviour
{
    public bool bossKill;
    [SerializeField] private GameObject finish;
    // Start is called before the first frame update
    void Start()
    {
        bossKill = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossKill) finish.gameObject.SetActive(true);
    }
}
