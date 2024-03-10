using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput; // Move the declaration here


    private void Awake() 
    {
        // Grabs references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        // Flip player horizontally
        if(horizontalInput > 0.01f)
            transform.localScale = new Vector3(-3, 3, 3);
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(3, 3, 3);

        // Jump
        if(Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed/2);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    } 

    public bool canAttack()
    {
        return horizontalInput == 0 && grounded;
    }
}
