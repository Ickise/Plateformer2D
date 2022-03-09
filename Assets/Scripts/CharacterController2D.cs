using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    float moveSpeed, jumpForce;
    public Transform groundPoint;
    public LayerMask whatIsGround;
    bool isGrounded;
    // public Animator anim;
    float inputX;
    Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);
        _rb.velocity = new Vector2(inputX * moveSpeed, _rb.velocity.y);

        if (_rb.velocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }
        else if (_rb.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("death"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {

        if (context.performed && isGrounded)
        {
            _rb.velocity += new Vector2(_rb.velocity.x, jumpForce);
        }
    }
}