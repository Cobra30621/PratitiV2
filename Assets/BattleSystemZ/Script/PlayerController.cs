using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRig2D;
    private Rigidbody2D enemyRig2D;

    private int amountOfJump = 2;
    [SerializeField]
    private int amountOfJumpLeft;

    private float movementInputDirection;
    private float moveSpeed = 8.0f;
    public float jumpForce = 6.0f;
    public float jumpTime;
    private float jumpTimeLeft;
    public float groundCheckRadius;
    public float airAttackCheckRadius;
    public float leftAttackCheckRadius;
    public float rightAttackCheckRadius;
    public float upAttackCheckRadius;
    public float downAttackCheckRadius;
    [SerializeField]
    public float dashTime = 0.2f;
    [SerializeField]
    public float dashSpeed = 10.0f;
    public float distanceBetweenImages = 0.3f;
    public float dashCoolDown = 0.5f;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;

    [SerializeField]
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isOnEnemy;
    [SerializeField]
    public bool canMove = true;
    [SerializeField]
    private bool canFlip = true;
    [SerializeField]
    private bool canJump;
    private bool isJumping;
    private bool isTouchingRightWall = false;
    private bool isTouchingLeftWall = false;
    public bool isAttacking = false;
    public bool isHitting = false;
    public bool isDashing;
    bool isCreateTrailEffetcs = false;
    private int wantMoveDirection = 1;

    [SerializeField]
    Vector3 originPosition;
    public Transform groundCheck;
    public Transform airAttackCheck;
    public Transform leftAttackCheck;
    public Transform rightAttackCheck;
    public Transform upAttackCheck;
    public Transform downAttackCheck;

    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;

    [SerializeField]
    List<GameObject> BodyParts;

    public Animator animator;

    [SerializeField]
    GameObject TrailEffectPrefab;
    [SerializeField]
    GameObject Tutorial;

    [SerializeField]
    BoxCollider2D playerBox2D;

    [SerializeField]
    KeyCode keyCode_left;
    [SerializeField]
    KeyCode keyCode_right;
    [SerializeField]
    KeyCode keyCode_up;
    [SerializeField]
    KeyCode keyCode_down;
    [SerializeField]
    KeyCode keyCode_attack01;
    [SerializeField]
    KeyCode keyCode_attack02;
    [SerializeField]
    KeyCode keyCode_attack03;
    [SerializeField]
    KeyCode keyCode_jump01;
    [SerializeField]
    KeyCode keyCode_jump02;
    [SerializeField]
    KeyCode keyCode_specialAttack01;
    [SerializeField]
    KeyCode keyCode_specialAttack02;
    [SerializeField]
    KeyCode keyCode_dash01;
    [SerializeField]
    KeyCode keyCode_dash02;

    // ---------------------System Methods---------------------

    void Start()
    {
        playerRig2D = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            BattlePratitiData.player_hp = BattlePratitiData.player_maxHp;
            BattlePratitiData.enemy_hp = BattlePratitiData.enemy_maxHp;
            BattlePratitiData.player_energy = 0;
            BattlePratitiData.enemy_energy = 0;
            this.transform.position = originPosition;
        }
        CheckInput();
        UpdateDirection();
        CheckIfCanJump();
        CheckDash();
    }

    private void FixedUpdate()
    {
        Move();
        CheckSurroudings();
        GroundDrag();
        // CheckAttack();
        if(!isAttacking)
        {
            isHitting = false;
            //playerRig2D.gravityScale = 2.5f;
        }
    }

    private void UpdateDirection()
    {
        if (isFacingRight && movementInputDirection < 0 && !isAttacking)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0 && !isAttacking)
        {
            Flip();
        }
    }

    // 畫判定框
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(airAttackCheck.position, airAttackCheckRadius);
        Gizmos.DrawWireSphere(leftAttackCheck.position, leftAttackCheckRadius);
        Gizmos.DrawWireSphere(rightAttackCheck.position, rightAttackCheckRadius);
        Gizmos.DrawWireSphere(upAttackCheck.position, upAttackCheckRadius);
        Gizmos.DrawWireSphere(downAttackCheck.position, downAttackCheckRadius);
    }*/
    void GroundDrag()
    {
        if (isGrounded && !isDashing)
        {
            playerRig2D.velocity = Vector2.up * playerRig2D.velocity.y;
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "LeftWall")
        {
            isTouchingLeftWall = true;
        }
        else if (col.gameObject.name == "RightWall")
        {
            isTouchingRightWall = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "LeftWall")
        {
            isTouchingLeftWall = true;
        }
        else if (col.gameObject.name == "RightWall")
        {
            isTouchingRightWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "LeftWall")
        {
            isTouchingLeftWall = false;
        }
        else if (col.gameObject.name == "RightWall")
        {
            isTouchingRightWall = false;
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (col.gameObject.name == "Enemy")
        {
            isOnEnemy = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
        if (col.gameObject.name == "Enemy")
        {
            isOnEnemy = true;
        }
        if (col.gameObject.name == "LeftWall")
        {
            isTouchingLeftWall = false;
        }
        else if (col.gameObject.name == "RightWall")
        {
            isTouchingRightWall = false;
            print(11111);
        }
    }

    // ---------------------Checking Methods---------------------

    private void CheckInput()
    {
        //movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(keyCode_left) || Input.GetKey(keyCode_right))
        {
            if (Input.GetKey(keyCode_left))
            {
                movementInputDirection = -1;
            }
            if (Input.GetKey(keyCode_right))
            {
                movementInputDirection = 1;
            }
        }
        else
        {
            movementInputDirection = 0;
        }

        if (Input.GetKeyDown(keyCode_jump01) || Input.GetKeyDown(keyCode_jump02))
        {
            if (canJump)
            {
                isJumping = true;
                jumpTimeLeft = jumpTime;
                Jump(7f);
                amountOfJumpLeft--;
            }
        }

        // 小跳與長跳
        if (Input.GetKey(keyCode_jump01) || Input.GetKey(keyCode_jump02))
        {
            if (canJump)
            {
                if (jumpTimeLeft > 0)
                {
                    Jump(7);
                    jumpTimeLeft -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }

                if (Input.GetKeyUp(keyCode_jump01) || Input.GetKeyUp(keyCode_jump02))
                {
                    isJumping = false;
                }
            }
        }

        // 攻擊按鍵判定
        if (!isAttacking && !isDashing)
        {
            if (Input.GetKeyDown(keyCode_attack01) || Input.GetKeyDown(keyCode_attack02) || Input.GetMouseButtonDown(0))
            {
                canMove = false;
                if (amountOfJumpLeft < 2)
                {
                    canMove = true;
                    if (this.transform.position.y >= -1.5f)
                    {
                        animator.SetTrigger("AirAttack");
                    }
                }
                else if (Input.GetKey(keyCode_specialAttack01) || Input.GetKey(keyCode_specialAttack02))
                {
                    if (Input.GetKey(keyCode_up))
                    {
                        StartCoroutine(SpecialAttackRotate(200));
                    }
                    else if (Input.GetKey(keyCode_down))
                    {
                        StartCoroutine(SpecialAttackRotate(-500));
                    }
                    else if (Input.GetKey(keyCode_right) || Input.GetKey(keyCode_left))
                    {
                        StartCoroutine(SpecialAttackRotate(-300));
                    }

                    canFlip = false;
                    amountOfJumpLeft = 0;
                    playerRig2D.gravityScale = 0;
                    animator.SetTrigger("SpecialAttack");
                    StartCoroutine(SpecialAttackStopCheck());

                }
                else if (Input.GetKey(keyCode_up))
                {
                    animator.SetTrigger("UpAttack");
                }
                else if (amountOfJumpLeft == 2)
                {
                    canMove = true;
                    animator.SetTrigger("Attack");
                }
            }
        }
        if (amountOfJumpLeft < amountOfJump)
        {
            canFlip = false;
        }
        else
        {
            canFlip = true;
        }


        // 攻擊按鍵判定
        // if(isJumping && Input.GetKeyDown(KeyCode.L)) {
        //     if(!isAttacking) {
        //         isAttacking = true;
        //         animator.SetBool("isAirAttacking", true);
        //         playerRig2D.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        //     }
        // } else if(Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.K)) {
        //     if(!isAttacking) {
        //         isAttacking = true;
        //         animator.SetBool("isLeftAttacking", true);
        //     }
        // } else if(Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.K)) {
        //     if(!isAttacking) {
        //         isAttacking = true;
        //         animator.SetBool("isRightAttacking", true);
        //     }            
        // } else if(Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.K)) {
        //     if(!isAttacking) {
        //         isAttacking = true;
        //         animator.SetBool("isUpAttacking", true);
        //     }            
        // } else if(Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.K)) {
        //     if(!isAttacking) {
        //         isAttacking = true;
        //         animator.SetBool("isDownAttacking", true);
        //     }            
        // }

        //閃避按鍵判定
        if (Input.GetKeyDown(keyCode_dash01) || Input.GetKeyDown(keyCode_dash02))
        {
            if (!isAttacking && Time.time >= (lastDash + dashCoolDown))
            {
                Dash();
            }
        }

        if (Input.GetKey(keyCode_right))
        {
            wantMoveDirection = 1;
        }
        else if (Input.GetKey(keyCode_left))
        {
            wantMoveDirection = -1;
        }
    }


    private void CheckSurroudings()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && playerRig2D.velocity.y <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack")) 
        {
            print("TouchGround");
            amountOfJumpLeft = amountOfJump;
        }

        if (amountOfJumpLeft > 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void CheckDash()
    {
        if (isDashing && !isAttacking)
        {
            if(!isCreateTrailEffetcs)
            {
                StartCoroutine(CreateDashTrailEffect());

                canFlip = true;
            }
            if (dashTimeLeft > 0)
            {
                canMove = false;

                float faceTo = isFacingRight == true ? 1 : -1;
                Debug.Log(faceTo);

                playerBox2D.isTrigger = true;
                playerRig2D.velocity = new Vector2(dashSpeed * wantMoveDirection, 0.0f);
                dashTimeLeft -= Time.deltaTime;


                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    //PlayerAfterImagePool.Instance.GetFromPool();
                    //lastImageXpos = transform.position.x;
                }

            }

            if (dashTimeLeft <= 0 || (isTouchingRightWall || isTouchingLeftWall))
            {
                print(1231);
                isDashing = false;
                canMove = true;
                canFlip = true;
                playerBox2D.isTrigger = false;
                playerRig2D.velocity = new Vector2(0, 0.0f);
            }

        }
    }

    //攻擊判定

    /*
     public void CheckAttack()
    {
        if (isAttacking)
        {
            if (animator.GetBool("isAirAttacking"))
            {
                Collider2D[] enemyList = Physics2D.OverlapCircleAll(airAttackCheck.position, airAttackCheckRadius, whatIsEnemy);
                Damage(enemyList, 1.0f, 12.0f);
            }
            else if (animator.GetBool("isLeftAttacking"))
            {
                Collider2D[] enemyList = Physics2D.OverlapCircleAll(leftAttackCheck.position, leftAttackCheckRadius, whatIsEnemy);
                Damage(enemyList, -3.0f, 6.0f);
            }
            else if (animator.GetBool("isRightAttacking"))
            {
                Collider2D[] enemyList = Physics2D.OverlapCircleAll(rightAttackCheck.position, rightAttackCheckRadius, whatIsEnemy);
                Damage(enemyList, 3.0f, 6.0f);
            }
            else if (animator.GetBool("isUpAttacking"))
            {
                Collider2D[] enemyList = Physics2D.OverlapCircleAll(upAttackCheck.position, upAttackCheckRadius, whatIsEnemy);
                Damage(enemyList, 0.0f, 12.0f);
            }
            else if (animator.GetBool("isDownAttacking"))
            {
                Collider2D[] enemyList = Physics2D.OverlapCircleAll(downAttackCheck.position, downAttackCheckRadius, whatIsEnemy);
                Damage(enemyList, 1.0f, 6.0f);
            }
        }
    }
    */

    // ---------------------Applying Methods---------------------

    // 傷害
    // private void Damage(Collider2D[] enemyList, float xVelocity, float yVelocity) {
    //     if(enemyList.Length != 0) {
    //         foreach(Collider2D enemy in enemyList) {
    //             Rigidbody2D enemyRigidbody = enemy.transform.GetComponent<Rigidbody2D>();
    //             enemyRigidbody.velocity = new Vector2(xVelocity, yVelocity);
    //         }
    //     }

    /*
    private void Damage(Collider2D[] enemyList, float xVelocity, float yVelocity)
    {
        if (enemyList.Length != 0)
        {
            foreach (Collider2D enemy in enemyList)
            {
                Rigidbody2D enemyRigidbody = enemy.transform.GetComponent<Rigidbody2D>();
                enemyRigidbody.velocity = new Vector2(xVelocity, yVelocity);
            }
        }
    }
    */
    public void Damage(float xVelocity, float yVelocity)
    {
        //enemyRigidbody.velocity = new Vector2(xVelocity, yVelocity);
    }
    private void Dash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        //PlayerAfterImagePool.Instance.GetFromPool();
        //lastImageXpos = this.transform.position.x;

    }

    private void Move()
    {
        if (canMove)
        {
            if (!((movementInputDirection > 0 && isTouchingRightWall) || (movementInputDirection < 0 && isTouchingLeftWall)))
            {
                this.transform.position += Vector3.right * 0.01f * moveSpeed * movementInputDirection;
            }
        }
        else if (!isDashing)
        {
            print("StopMoving");
            //playerRig2D.velocity = new Vector2(0, playerRig2D.velocity.y);
        }
    }

    private void Flip()
    {
        if (!isAttacking & canFlip)
        {
            isFacingRight = !isFacingRight;
            //transform.Rotate(0, 180, 0, 0);
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y);
        }
    }

    private void Jump(float jumpForce)
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            playerRig2D.velocity = new Vector2(playerRig2D.velocity.x, jumpForce);
        }
    }

    // 完成攻擊動畫
    // public void FinishAttaking() {
    //     Debug.Log("Finished!");
    //     isAttacking = false;
    //     playerRig2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    //     animator.SetBool("isAirAttacking", false);
    //     animator.SetBool("isLeftAttacking", false);
    //     animator.SetBool("isRightAttacking", false);
    //     animator.SetBool("isUpAttacking", false);
    //     animator.SetBool("isDownAttacking", false);
    // }


    public IEnumerator StartHurt(float i)
    {
        //StartCoroutine(StartWiggle(0.2f, 10, 15));
        BodyChangeColor(Color.red);
        yield return new WaitForSeconds(0.5f);
        BodyChangeColor(Color.white);
    }
    void BodyChangeColor(Color color)
    {
        foreach (GameObject part in BodyParts)
        {
            part.GetComponent<SpriteRenderer>().color = color;
        }
    }
    public IEnumerator StartWiggle(float freeze_time, float x_force, float y_force)
    {
        x_force *= 1;
        y_force *= 1;
        playerRig2D.velocity = Vector3.zero;
        canJump = false;
        canMove = false;
        canFlip = false;
        yield return new WaitForSeconds(freeze_time);
        playerRig2D.velocity = new Vector3(x_force, y_force);
        yield return new WaitForSeconds(0.15f);
        canJump = true;
        canMove = true;
        canFlip = true;
        playerRig2D.velocity = new Vector3(x_force/3, y_force/4);
    }
    IEnumerator SpecialAttackStopCheck()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.77f);
        transform.rotation = new Quaternion(0, 180, 0, 0);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        isAttacking = false;
        canFlip = true;
        amountOfJumpLeft = amountOfJump;
        canJump = true;
    }
    IEnumerator SpecialAttackRotate(int rotation)
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.17f);
        transform.Rotate(0, 0, rotation * transform.localScale.x, 0);
    }
    IEnumerator CreateDashTrailEffect()
    {
        int i = 0, times = 10;
        GameObject obj;
        isCreateTrailEffetcs = true;
        while(i<times)
        {
            obj = Instantiate(TrailEffectPrefab, this.transform.position - 0.0f * Vector3.right, Quaternion.Euler(0, 0, 0));
            obj.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y);
            StartCoroutine(TrailEffectsControl(obj));
            yield return new WaitForSeconds(0.03f);
            i++;
        }
        isCreateTrailEffetcs = false;
    }
    IEnumerator TrailEffectsControl(GameObject obj)
    {
        SpriteRenderer obj_sprite = obj.GetComponent<SpriteRenderer>();
        while (obj_sprite.color.a > 0)
        {
            obj_sprite.color -= 0.4f * Color.black;
            obj.transform.localScale -= 0.01f * Vector3.up;
            if (obj.transform.localScale.x > 0) 
            {
                obj.transform.localScale -= 0.01f * Vector3.right;
            }
            else
            {
                obj.transform.localScale += 0.01f * Vector3.right;
            }
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(obj);
    }

}

