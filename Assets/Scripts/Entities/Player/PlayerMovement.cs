using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private float horizontal;
    [SerializeField] private float speed = 10f;
    public event Action OnPlayerRotation;
    public bool isFacingRight = true;
    
    void Start()
    {
        rbSprite = GetComponent<SpriteRenderer>();
        OnPlayerRotation += Camera.main.GetComponent<CameraRotate>().Rotate;
    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        rbSprite.flipX = !rbSprite.flipX;
        isFacingRight = !isFacingRight;

        OnPlayerRotation?.Invoke();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
