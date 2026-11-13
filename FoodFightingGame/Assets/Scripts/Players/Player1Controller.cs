using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public int health = 5;
    public int maxHealth = 5;
    public int filledWeaponSlots = 0;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool attacking = false;

    [Header("References")]
    private Rigidbody2D rb;
    private PlayerInput inputComponent;
    private Vector2 moveInput = Vector2.zero;
    private InputAction moveAction;
    private InputAction attackAction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputComponent = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (inputComponent != null && inputComponent.actions != null)
        {
            moveAction = inputComponent.actions.FindAction("Move");
            attackAction = inputComponent.actions.FindAction("Attack");

            if (moveAction != null) { moveAction.performed += Move; moveAction.canceled += Move; }
            //if (attackAction != null) { attackAction.performed += Attack; attackAction.canceled += Attack; }
        }
    }

    private void OnDisable()
    {
        if (moveAction != null) { moveAction.performed -= Move; moveAction.canceled -= Move; }
        //if (attackAction != null) { attackAction.performed -= Attack; attackAction.canceled -= Attack; }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
    /*
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        Vector2 lookDir = mousePos - transform.position;
        rb.MoveRotation(Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f);
        Transform activeWeapon = transform.GetChild(0);
        if (activeWeapon.CompareTag("MeleeWeapon"))
        {
            currentMeleeWeapon = activeWeapon.GetComponent<MeleeWeapons>();
        }
        if (activeWeapon.CompareTag("RangedWeapon"))
        {

            if (currentRangedWeapon != null && currentRangedWeapon.holdToAttack && attacking)
            {
                currentRangedWeapon.Fire();
            }
        }
    }
    private void Attack(InputAction.CallbackContext context)
    {
        if (currentRangedWeapon)
        {
            if (currentRangedWeapon.holdToAttack)
                attacking = context.ReadValueAsButton();
            else if (context.performed)
                currentRangedWeapon.Fire();
        }
        else if (currentMeleeWeapon && context.performed && currentMeleeWeapon.canAttack)
        {
            currentMeleeWeapon.StartAttack();
        }
    }
    */
    private void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
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