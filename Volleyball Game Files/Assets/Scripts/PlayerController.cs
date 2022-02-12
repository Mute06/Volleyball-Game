using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    private CharacterSwitcher characterSwitcher;

    [SerializeField] private float moveSpeed;
    public float jumpForce;
    [SerializeField] float additionalGravityNormal, additionalGravityFalling =  2f;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    private bool isGrounded;

    private void Start()
    {
        characterSwitcher = GetComponent<CharacterSwitcher>();
    }



    // Update is called once per frame
    void Update()
    {
        // Jump
        if (isGrounded && InputManager.Instance.GetJump)
        {
            characterSwitcher.CurrentRb.AddForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
        }
        

    }

    private void FixedUpdate()
    {
        // Checking if grounded
        isGrounded = Physics2D.OverlapCircle(characterSwitcher.currentCharactersGroundCheck.position, checkRadius, groundLayer);

        // Adjusting horizontal velocity
        characterSwitcher.CurrentRb.velocity = new Vector2(InputManager.Instance.HorizontalInput * moveSpeed, characterSwitcher.CurrentRb.velocity.y);
        
        //If falling down add additional gravity
        if (!isGrounded)
        {
            float totalAdditionalGravity = additionalGravityNormal;
            
            // Ýncrease additional gravity if falling down
            if (characterSwitcher.CurrentRb.velocity.y < -0.1f)
            {
                totalAdditionalGravity = additionalGravityFalling;
            }
            characterSwitcher.CurrentRb.AddForce(Vector2.down * totalAdditionalGravity, ForceMode2D.Impulse);
        }
    }

}
