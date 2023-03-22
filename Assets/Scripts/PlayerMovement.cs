﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool doubleJump;
    private bool isSprinting;
    private bool isSliding = false;
    private bool isFacingRight = true;

    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpingPower = 18f;
    [SerializeField] private float slideSpeed = 600f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D regularColl;
    [SerializeField] private BoxCollider2D slideColl;

    private void Update()
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

        if (Input.GetKeyDown(KeyCode.X) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || isSprinting)) {
            Debug.Log("Neutral Attack");
        }
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.DownArrow) || isSprinting)) {
            Debug.Log("Up Attack");
        }
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.UpArrow) || isSprinting)) {
            Debug.Log("Down Attack");
        }

        if (IsGrounded() && !isSliding && Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z)) {
            Slide();
        }

        if (IsGrounded() && !isSliding && Input.GetKey(KeyCode.A))
        {
            Backdash();
        }

        if (Input.GetKey(KeyCode.S)) {
            isSprinting = true;
        }
        else {
            isSprinting = false;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (!isSliding) {
            if (!isSprinting) {
                rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            }
            else {
                rb.velocity = new Vector2(horizontal * moveSpeed * 1.5f, rb.velocity.y);
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
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
        yield return new WaitForSeconds(0.4f);
        isSliding = false;
        regularColl.enabled = true;
        slideColl.enabled = false;
    }

    private void Backdash() {
        isSliding = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (isFacingRight) {
            rb.AddForce(Vector2.left * slideSpeed);
        }
        else {
            rb.AddForce(Vector2.right * slideSpeed);
        }
        StartCoroutine("stopDash");
    }

    IEnumerator stopDash() {
        yield return new WaitForSeconds(0.4f);
        isSliding = false;
    }
}