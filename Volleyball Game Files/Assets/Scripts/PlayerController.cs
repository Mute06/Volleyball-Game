using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    private CharacterSwitcher characterSwitcher;

    [SerializeField] private float moveSpeed;
    public float jumpForce;
    [SerializeField] float additionalGravity =  10f;
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

        if (isGrounded && InputManager.Instance.GetJump)
        {
            characterSwitcher.CurrentRb.AddForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
            Debug.Log("Jumped");
        }
        

    }

    private void FixedUpdate()
    {
        // Checking if grounded
        isGrounded = Physics2D.OverlapCircle(characterSwitcher.currentCharactersGroundCheck.position, checkRadius, groundLayer);

        // Adjusting horizontal velocity
        characterSwitcher.CurrentRb.velocity = new Vector2(InputManager.Instance.HorizontalInput * moveSpeed, characterSwitcher.CurrentRb.velocity.y);
        
        //If falling down
        if (characterSwitcher.CurrentRb.velocity.y < -0.1f || characterSwitcher.CurrentRb.velocity.y > 0.1f)
        {
            Debug.Log("Falling");
            characterSwitcher.CurrentRb.AddForce(Vector2.down * additionalGravity);
        }
    }

}
