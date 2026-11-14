using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{    
    [Header("References")]
    private Rigidbody2D rb;
    private PlayerInput inputComponent;
    /*
    private Transform Attacker;
    private MeleeAttacks AttackMelee;
    private RangedAttacks AttackRanged;
    */
    private Vector2 moveInput = Vector2.zero;
    public LayerMask ground;

    [Space]
    [Header("Meta")]
    public bool canJump;
    public bool canAttack;
    [Space]
    [Header("Player Settings")]
    public int health;
    public int maxHealth;
    public float groundCheckRange;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private bool attacking = false;

    [Space]
    [Header("Rays")]
    private RaycastHit2D OnGround;

    [Space]
    [Header("Modifications")]
    public float speedMult;
    public float attackSpeedMult;
    public float jumpHeightMult;
    


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputComponent = GetComponent<PlayerInput>();
        //Attacker = transform.GetChild(0);

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
        //Collect Inputs Then Set Rotation Then Set Movement Then Perform Attacks Then Set CoolDowns

        //Collect Inputs
        rb.linearVelocityX = moveInput.x * speed * speedMult;
    }
    /*
    private void Attack(InputAction.CallbackContext context)
    {
        if (AttackRanged)
        {
            if (AttackRanged.holdToAttack)
                attacking = context.ReadValueAsButton();
            else if (context.performed)
                AttackRanged.Fire();
        }
        else if (AttackMelee && context.performed && AttackMelee.canAttack)
        {
            AttackMelee.StartAttack();
        }
    }
    */

    public void PressA(InputAction.CallbackContext context)
    {
        Debug.Log("Read A Input");
        if (!attacking)
        {
            if (rb.linearVelocity.magnitude < 1)
            {

            }
        }
    }
    public void PressB(InputAction.CallbackContext context)
    {
        Debug.Log("Read B Input");
        if (!attacking)
        {
            if (rb.linearVelocity.magnitude < 1)
            {

            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Read Jump Input");
        if (OnGround)
        rb.AddForceY(jumpForce * jumpHeightMult,ForceMode2D.Impulse);
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