using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    float moveSpeed, jumpForce;
    public Transform groundPoint;
    public LayerMask whatIsGround;
    bool isGrounded;
    public Text Compteur;
   
    float inputX;
    Rigidbody2D _rb;

    bool WaitJump;

    public Animator anim;

    float CoinCounter;
    public GameObject Tonneau;

    public AudioSource CoinSound;
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
            transform.localScale = new Vector3(0.5235f, 0.5235f, 0.5235f);
        }
        else if (_rb.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-0.5235f, 0.5235f, 0.5235f);
        }

        if (_rb.velocity.x == 0f)
        {
            anim.SetBool("Walking", false);
        }

        if (isGrounded && WaitJump)
        {
            anim.SetBool("Jump", false);
        }

        if(CoinCounter >= 5)
        {
            Tonneau.SetActive(true);
        }

        Compteur.text = CoinCounter + "" ;
    }
    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        anim.SetBool("Walking", true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("death"))
        {
            SceneManager.LoadScene("Scenes/Game");
        }

        if (other.tag == ("Coin"))
        {
            CoinSound.Play();
            Destroy(other.gameObject);
            CoinCounter += 1;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {

        if (context.performed && isGrounded)
        {
            StartCoroutine(Waiter());
            anim.SetBool("Jump", true);
            _rb.velocity += new Vector2(_rb.velocity.x, jumpForce);
        }
    }

    IEnumerator Waiter()
    {
        WaitJump = false;
        yield return new WaitForSeconds(1);
        WaitJump = true;
    }
}