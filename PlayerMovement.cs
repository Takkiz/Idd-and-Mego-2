using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour{

    private PlayerInput controller;
    private Rigidbody2D rb;
    private BoxCollider2D cl;
    [Header("Speed Stuff")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float max = 14f;
    [Header("LayerTypes")]
    [Header("LayerTypes")]
    [SerializeField] private LayerMask ground;

    public float Speed { get => speed; set => speed = value; }

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    // Start is called before the first frame update
    private void Start(){

        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<BoxCollider2D>();
        controller = gameObject.GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void onJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered; //triggered on frame

    }


    // Update is called once per frame
    private void Update(){

        float dirX = movementInput.x;
        float dirY = movementInput.y;
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);


        if (jumped && onGround()){

            rb.velocity = new Vector2(rb.velocity.x, (dirY * (Speed / 2)) + 12f);
        }
    }
    private bool onGround(){

        return Physics2D.BoxCast(cl.bounds.center, cl.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}
