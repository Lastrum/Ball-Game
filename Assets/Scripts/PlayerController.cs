using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float speed = 0;
    public float distance;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject crown;
    public Animation anim;
    
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        crown.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;
        
        //SetCountText();
        winTextObject.SetActive(false);
    }

    void OnFire(InputValue movementValue)
    {
        anim.Play("Frog");
    }
    
    void OnJump(InputValue movementValue)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distance);
        Debug.DrawRay(transform.position, Vector3.down * (distance) , Color.red);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (other.CompareTag("Crown"))
        {
            other.gameObject.SetActive(false);
            crown.SetActive(true);
        }
    }
    
}
