using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    private float horizontal;
    [SerializeField] private float speed = 10f;
    void Start()
    {
        

    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
