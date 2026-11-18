using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(PlayerInput))]
public class Player1Controller : MonoBehaviour
{    
    [Header("References")]
    private Rigidbody2D rb;
    private PlayerInput inputComponent;
    
    private Transform Attacker;
    private MeleeAttacks AttackMelee;
    
    private Vector2 moveInput = Vector2.zero;
    [SerializeField] private LayerMask ground;

    [Space]
    [Header("Meta")]
    [SerializeField] private bool canJump;
    [SerializeField] private bool canAttack;
    [SerializeField] private float canAttackCooldowwn;
    private bool facingRight;

    [Space]
    [Header("Character Settings")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float groundCheckRange;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;

    [Space]
    [Header("Rays")]
    private RaycastHit2D OnGround;

    [Space]
    [Header("Modifications")]
    public float speedMult;
    public float attackSpeedMult;
    public float jumpHeightMult;

    [Space]
    [Header("Attacks")]
    private float Attack1cooldown;
    private  float Attack2cooldown;
    private  float Attack3cooldown;
    private  float Attack4cooldown;
    private  float Attack5cooldown;
    private  float Attack6cooldown;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputComponent = GetComponent<PlayerInput>();
        Attacker = transform.GetChild(0);
        AttackMelee = Attacker.GetComponent<MeleeAttacks>();
    }


    void FixedUpdate()
    {
        OnGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRange, ground);
        if (OnGround)
        {
            canJump = true;
        }
        else
            canJump = false;
    }
    
    void Update()
    {
        //Collect Inputs Then Set Rotation Then Set Movement

        //Collect Inputs

        //Set Rotation
        RotationFlipController();
        //ApplyMovement

        rb.linearVelocityX = moveInput.x * speed * speedMult;
        

    }
    private void RotationFlipController()
    {
        if(rb.linearVelocity.normalized.x < 0 && facingRight)
        {
            FlipRotation();
        }
        else if (rb.linearVelocity.normalized.x > 0 && !facingRight)
        {
            FlipRotation();
        }
    }
    private void FlipRotation()
    {
        //Switcher
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    #region Attacks
    /*
Standing Still (Lb) (Held): (Block) Block is Omani-Directional. Shield Loses health over time and loses health when being hit. If shield breaks Player will be stunned. Block does not stop grabs. Block will regain health over time when not in use.

(Rb): (Grab) Grabs the opponent stopping them from moving. (Can Grab opponent even if they are Blocking)

While Grabbing (A): (Toss): Throw opponent in a chosen direction potentially as a combo starter.
(If thrown into a wall or the ground the opponent will take impact damage. If thrown into the air or forwards the opponent will be unable to move for a very short period.)

On Ground (Y): (Jump) performs a jump.
(If Moving horizontally keep that momentum through the jump.)

No Directional Input (A): (Jab): attacks with characters range short duration melee Attack.

Running On Ground(Left/Right Input) (A): (Running Jab): Same as a jab but goes slightly farther and costs more frames.

On Ground Aim Up (A): (Upward Jab): Attacks Upwards with characters range

In Air No Horizontal Direction (A): (air kick) attacks with character range short duration aerial Attack.
(Up Or Down Inputs will make the jab diagonal)

In Air Moving Horizontal + Falling(A): (Falling kick) attacks diagonally downwards in faced direction

In Air Moving Horizontal + Rising(A): (Forward Air Kick) Attacks forward with more range costing more frames than non directional.

In Air Moving Horizontal Aim Up (A): (Upward Air attack) Kicks Upward
 
In Air Moving Horizontal Aim Down (A) (Downward Air Attack) Kicks Down quickly

     */
    public void PressLb(InputAction.CallbackContext context)
    {
        Debug.Log("Read Lb Input");
    }    
    public void PressRb(InputAction.CallbackContext context)
    {

    }
    public void PressA(InputAction.CallbackContext context)
    {
        if (canAttack)
        {
            Debug.Log("Read A Input");

            canAttack = false;
            if (OnGround)
            {
                if (rb.linearVelocityX < 1)
                {
                    Debug.Log("OnGroundNotMovingAttackRead");
                    OnGroundNoMovementA();
                }
                if (rb.linearVelocityX >= 1)
                {
                    Debug.Log("OnGroundMovingAttackRead");
                    OnGroundNoMovementA();
                }
            }
            else if(!OnGround)
            {
                if (rb.linearVelocityX < 1)
                {

                }
            }
        }
    }    
    public void PressB(InputAction.CallbackContext context)
    {
        if (canAttack)
        {
            if (rb.linearVelocityX < 1)
            {

            }
            if(rb.linearVelocityX >= 1)
            {
                
            }
        }
    }


    private void OnGroundNoMovementA()
    {
        Debug.Log("Attack OnGroundNoMovementA Performed");
        canAttackCooldowwn = Attack1cooldown;
        StartAttackCooldown();
    }
    private void OnGroundMoveingA()
    {
        Debug.Log("Attack OnGroundMovingA Performed");
        canAttackCooldowwn = Attack2cooldown;
        StartAttackCooldown();
    }

    #endregion Attacks
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Read Jump Input");

        if (OnGround)
        {
            Debug.Log("Performed Jump");
            rb.AddForceY(jumpForce * jumpHeightMult, ForceMode2D.Impulse);
        }
    }




    private void StartAttackCooldown()
    {
        if (canAttackCooldowwn > 0)
            Invoke(nameof(EndAttackCooldown), canAttackCooldowwn);
    }
    private void EndAttackCooldown()
    {
        canAttackCooldowwn = 0;
        canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2AttackZone"))
        {
            health--;
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health--;
        }
    }
}