using UnityEngine;

public class ThunderAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform thunderPoint;
    [SerializeField] private GameObject[] thunders;
    private Animator anim;

    private PlayerMove playerMove;
    private float cooldownTimer = Mathf.Infinity;
    public bool isAttack1 = false;
    [SerializeField] AudioClip thunderSound;
    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer > attackCooldown && playerMove.canAttack() && playerMove.skill == 1 && playerMove.num_skill > 0)
        {
            isAttack1 = true;
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(thunderSound);
        playerMove.num_skill--;
        anim.SetTrigger("thunder");
        cooldownTimer = 0;

        thunders[FindThunder()].transform.position = thunderPoint.position;
        thunders[FindThunder()].GetComponent<Thunder>().ThunderActivate();
    }
    private int FindThunder()
    {
        for (int i = 0; i < thunders.Length; i++)
        {
            if (!thunders[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}