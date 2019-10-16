using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float runSpeed, jumpForce;
    private float jumpHeight = -4f;
    private float moveInput;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool facingRight = true;

    private Vector3 range;

    void Awake () {
        myBody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    void FixedUpdate () {
        Movement ();
        CheckCollisionForJump ();
    }

    void Movement () {
        moveInput = Input.GetAxisRaw ("Horizontal") * runSpeed;
        // código para o personagem não atacar no momento em que está 
        // if (anim.GetBool("SwordAttack")) moveInput = 0;
        // moveInput com valores negativos não irá funcionar a animação de correr, logo utilizar a função Mathf
        anim.SetFloat ("Speed", Mathf.Abs (moveInput));
        myBody.velocity = new Vector2 (moveInput, myBody.velocity.y);
        // Quanto mais pressionado o teclado, pulo mais alto
        if (Input.GetKeyUp (KeyCode.Space)) {
            if (myBody.velocity.y > 0) {
                myBody.velocity = new Vector2 (myBody.velocity.x, myBody.velocity.y * jumpHeight);
            }
        }
        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight) {
            Flip ();
        }

        if (myBody.velocity.y < 0) {
            anim.SetBool ("Fall", true);
        } else {
            anim.SetBool ("Fall", false);
        }
    }

    void CheckCollisionForJump () {
        Collider2D bottomHit = Physics2D.OverlapBox (groundCheck.position, range, 0, groundLayer);
        if (bottomHit != null) {
            if (bottomHit.gameObject.tag == "Ground" && Input.GetKeyDown (KeyCode.Space)) {
                myBody.velocity = new Vector2 (myBody.velocity.x, jumpForce);
                anim.SetBool ("Jump", true);
            } else {
                anim.SetBool ("Jump", false);
            }
        }
    }

    void Flip () {
        facingRight = !facingRight;
        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }
}