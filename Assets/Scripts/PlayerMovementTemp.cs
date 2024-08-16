using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementTemp : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the player moves
    public float jumpForce = 10f; // Force applied when the player jumps
    public Transform groundCheck; // Transform used to check if the player is on the ground
    public LayerMask groundLayer; // Layer mask to determine what is considered ground

    public GameObject playerCamera;

    private Rigidbody2D rb;
    private bool isGrounded;

    [SerializeField]
    private bool quantumLevel;

    public TMP_Text levelValue;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Get horizontal input
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Move the player
        if (quantumLevel)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y) * -1;
        }
        else 
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        

        // Jump if the player is on the ground and the jump button is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            changeDimension();
        }
    }

    private void changeDimension() 
    {
        quantumLevel = !quantumLevel;
        if (!quantumLevel)
        {
            playerCamera.transform.position = new Vector3(0, 0, -10);
            playerCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
            levelValue.text = "Non-Quantum";
        }
        else
        {
            playerCamera.transform.position = new Vector3(0, 0, 15);
            playerCamera.transform.rotation = Quaternion.Euler(0, 180, 0);
            levelValue.text = "Quantum";
        }
    }
}
