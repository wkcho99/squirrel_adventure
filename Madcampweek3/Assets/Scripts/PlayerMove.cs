using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] public float maxSpeed;
    public float jumPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D boxCollider;
    public Health health;
    [SerializeField] private LayerMask groundLayer;
    private float wallJumpCooldown;
    private float horizontalInput;
    private int cnt_hurt_frame;
    private bool isGliding;
    public int skill;
    [SerializeField] private GameObject fire_skilled;
    [SerializeField] private GameObject thunder_skilled;
    [SerializeField] private GameObject rock_skilled;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        health = GetComponent<Health>();
        gameManager.cnt_dotory = 0;
        cnt_hurt_frame = 0;
        isGliding = false;
        System.Random rand = new System.Random();
        gameManager.skill = rand.Next(3);
        skill = gameManager.skill;
        if(skill == 0) fire_skilled.SetActive(true);
        else if(skill == 1) thunder_skilled.SetActive(true);
        else if(skill == 2) rock_skilled.SetActive(true);
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        //Jump
        if(Input.GetButtonDown("Jump"))
                Jump();
        wallJumpCooldown += Time.deltaTime;

        //Stop speed when no input
         if(Input.GetButtonUp("Horizontal")){
            //rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            rigid.velocity = new Vector2(0, rigid.velocity.y);
         }

        //Direction change
        if(wallJumpCooldown > 0.7f) {
            if(horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if(horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        //walk animation setting
        if(Mathf.Abs(rigid.velocity.x)< 0.3 || isGliding)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

        if(health.hurt && cnt_hurt_frame < 100) {
            anim.SetTrigger("hurt");
            cnt_hurt_frame++;
        }
        else if(!health.hurt) cnt_hurt_frame = 0;

        if(!isGrounded() && Input.GetKey(KeyCode.F)) {
            anim.SetBool("isGliding", true);
            if(!isGliding) VelocityZero();
            isGliding = true;
            rigid.gravityScale = 0.3f;
        }
        else {
            anim.SetBool("isGliding", false);
            isGliding = false;
            rigid.gravityScale = 4;
        }

        if(wallJumpCooldown > 0.7f) {
            //Move by Control
            float h = Input.GetAxisRaw("Horizontal");
            if(h != 0)
                rigid.velocity = new Vector2(maxSpeed*h, rigid.velocity.y);
            anim.SetBool("isWalljumping", false);
        }

        //Maxspeed control
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //Landing Platform
        if(rigid.velocity.y<0){
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
            if(isGrounded()){
                anim.SetBool("isJumping", false);
            }
        }
    }

    private void Jump() {
        if(isGrounded()) {
            rigid.AddForce(Vector2.up * jumPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        else if(onWall() && !isGrounded()) {
            anim.SetBool("isWalljumping", true);
            wallJumpCooldown = 0;
            VelocityZero();
            rigid.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*20, 20);
            transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            anim.SetBool("isJumping", true);
        }
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        if(wallJumpCooldown > 0.7f) {
            //Move by Control
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h * 4, ForceMode2D.Impulse);
            anim.SetBool("isWalljumping", false);
        }

        //Maxspeed control
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //Landing Platform
        if(rigid.velocity.y<0){
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
            if(isGrounded()){
                anim.SetBool("isJumping", false);
            }
        }

    }*/
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy"){
            //Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y){
                OnAttack(collision.transform);
            }else{
                health.TakeDamage(1);
                OnDamaged(collision.transform.position);
            }
        }
        if(collision.gameObject.tag == "Spike"){
            health.TakeDamage(1);
            OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Item"){
            gameManager.cnt_dotory++;
            //Point Earn
            bool isBronze =  collision.gameObject.name.Contains("Bronze");
            bool isSilver =  collision.gameObject.name.Contains("Silver");
            bool isGold =  collision.gameObject.name.Contains("Gold");
            if(isBronze)
                gameManager.stagePoint += 50;
            else if(isSilver)
                gameManager.stagePoint += 100;
            else if(isGold)
                gameManager.stagePoint += 300;
            //Deactivate Item
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Finish" && gameManager.cnt_dotory == 3){
            //Finish -> to Next Stage
            gameManager.NextStage();
        }
    }

    void OnDamaged(Vector2 targetPos){
        //Change Layer
        gameObject.layer = 11;
        //View Alpha 피격시
        spriteRenderer.color = new Color(1,1,1,0.4f);
        //Reaction Force
        int direction = transform.position.x-targetPos.x > 0 ? 1: -1;
        rigid.AddForce(new Vector2(direction,1)*7, ForceMode2D.Impulse);

        Invoke("OffDamaged", 1.5f);
    }

    void OffDamaged(){
        health.hurt = false;
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1,1,1,1);
    }

    void OnAttack(Transform enemy){
        //Point
        gameManager.stagePoint += 100;
        //Reaction Force
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //Enemy die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.health.TakeDamage(100);
        enemyMove.OnDamaged();
    }

    public void VelocityZero(){
        rigid.velocity = Vector2.zero;
    }

    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.3f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack() {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}