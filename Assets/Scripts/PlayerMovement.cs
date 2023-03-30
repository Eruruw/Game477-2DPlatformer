using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    HPController HPCon;
    AudioSource whipit;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Ammo;
    public float ammo = 0f;
    private float hp = 20f;
    private GameObject attackArea = default;
    private float attackCD = 0.25f;
    private float attackTime = 0f;
    private float horizontal;
    private bool doubleJump;
    private bool isSprinting;
    private bool isAttacking = false;
    private bool isSliding = false;
    private bool isFacingRight = true;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpingPower = 18f;
    [SerializeField] private float slideSpeed = 600f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ProjectileBehavior projectilePrefab;
    [SerializeField] private ProjectileBehavior launchableProjectilePrefab;
    [SerializeField] private Transform launchOffset;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D regularColl;
    [SerializeField] private BoxCollider2D slideColl;

    private void Start() {
        attackArea = transform.GetChild(0).gameObject;
        HPCon = GetComponent<HPController>();
        whipit = GetComponent<AudioSource>();
        Health.text = hp.ToString();
    }

    private void Update()
    {
        Ammo.text = ammo.ToString();

        if (HPCon.IsStillKickinIt)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (IsGrounded() && !Input.GetKey(KeyCode.Z))
            {
                doubleJump = false;
            }
            if (Input.GetKeyDown(KeyCode.Z) && !Input.GetKey(KeyCode.DownArrow))
            {
                if (IsGrounded() || doubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    doubleJump = !doubleJump;
                }
            }
            if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.X) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || isSprinting))
            {
                NeutralAttack();
            }
            if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.DownArrow) || isSprinting) && (ammo >= 2))
            {
                Instantiate(launchableProjectilePrefab, launchOffset.position, transform.rotation);
                ammo -= 2;
            }
            if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.UpArrow) || isSprinting) && (ammo >= 1))
            {
                Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
                ammo -= 1;
            }

            if (IsGrounded() && !isAttacking && !isSliding && Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))
            {
                Slide();
            }

            if (IsGrounded() && !isSliding && Input.GetKey(KeyCode.A))
            {
                Backdash();
            }

            if (!isAttacking && Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }

            if (isAttacking)
            {
                attackTime += Time.deltaTime;
                if (attackTime >= attackCD)
                {
                    attackTime = 0;
                    isAttacking = false;
                    attackArea.SetActive(isAttacking);
                    //here
                    whipit.PlayOneShot(whipit.clip, 1f);
                }
            }

            Flip();
        }
        else
        {
            moveSpeed = 0f;
        }
    }

    private void FixedUpdate() {
        if (!isSliding) {
            if (!isSprinting) {
                rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            }
            else {
                rb.velocity = new Vector2(horizontal * moveSpeed * 1.5f, rb.velocity.y);
            }
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        if ((isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) && !isSliding) {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void NeutralAttack() {
        isAttacking = true;
        attackArea.SetActive(isAttacking);
    }

    private void Slide() {
        isSliding = true;
        regularColl.enabled = false;
        slideColl.enabled = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (isFacingRight) {
            rb.AddForce(Vector2.right * slideSpeed);
        }
        else {
            rb.AddForce(Vector2.left * slideSpeed);
        }
        StartCoroutine("stopSlide");
    }

    IEnumerator stopSlide() {
        yield return new WaitForSeconds(0.5f);
        isSliding = false;
        regularColl.enabled = true;
        slideColl.enabled = false;
    }

    private void Backdash() {
        isSliding = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (isFacingRight) {
            rb.AddForce(Vector2.left * (slideSpeed + 100f));
        }
        else {
            rb.AddForce(Vector2.right * (slideSpeed + 100f));
        }
        StartCoroutine("stopDash");
    }

    IEnumerator stopDash() {
        yield return new WaitForSeconds(0.5f);
        isSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Ouch");
            HPCon.TakeDamageFromWeapon(Weapon.Enemy);
            hp -= 1f;
            Health.text = hp.ToString();
        }
    }
}