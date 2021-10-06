using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField]
    [Header("Forces")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Grounded Information")]
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Battle Statistics")]
    public int battleRate;


    //Private
    private Rigidbody2D rb;
    private float Direction;

    //States
    private bool isFacingRight = true;
    private bool isJumping;
    private bool isGrounded;
    private bool isWalking;
    

    //Encounter
    private bool GenerateEncounter = false;
    private bool EncounterAvailable = true;
    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        InputHandler();

        // Facing
        if(Direction > 0 && !isFacingRight) {
            Flip();
        }
        else if(Direction < 0 && isFacingRight) {
            Flip();
        }



        //Check for Ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f,groundMask);

    }

    //Physics Handling
    void FixedUpdate()
    {
        //Movement
        Movement();

        //Encounter
        EncounterRNG();
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.name == "Props")
        {
            GenerateEncounter = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "Props")
        {
            EncounterAvailable = true;
        }
    }

    private void EncounterRNG()
    {
        if (GenerateEncounter == true && isWalking == true && EncounterAvailable == true)
        {
            if(Random.Range(0,100) <= battleRate)
            {
                //Debug.Log("Battle!");
                SceneManager.LoadSceneAsync("BattleScene");
                GenerateEncounter = false;
                EncounterAvailable = false;
            }
        }
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f,180.0f,0.0f);
    }



    private void InputHandler()
    {
        Direction = Input.GetAxis("Horizontal");
        if(Direction != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    private void Movement()
    {
        //Move
        rb.velocity = new Vector2(Direction * moveSpeed, rb.velocity.y);

        //Jumping
        if(isJumping && isGrounded)
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
        }
        isJumping = false;
    }



}
