using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float movementSpeed;
    private float storedSpeed;

    private bool facingRight;

    private bool attack;

    private Animator myAnimator;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    public Vector3 respawnPoint;

    [SerializeField]
    public bool OnTree;

    public bool Visible;

    GameObject lefttree;


	// Use this for initialization
	void Start () {
        storedSpeed = movementSpeed;
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        Visible = true;
        lefttree = GameObject.Find("Tree_3");




	}


    void Update()
    {
        HandleInput();
    }


    // Update is called once per frame
    void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log(horizontal);

        isGrounded =  IsGrounded();

        HandleMovement(horizontal,vertical);

        //Debug.Log(jump);

        Flip(horizontal);

        HandleAttacks();

        ResetValue();
	}


    private void HandleMovement(float horizontal, float vertical){

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, 400));
        }

        if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }

        if(OnTree){
            myRigidbody.velocity = new Vector2(0, vertical * movementSpeed);
        }

        GameObject tree = GameObject.Find("Tree_3");
        if(Visible){
            lefttree.SetActive(true);
        }else{
            lefttree.SetActive(false);
        }


        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

    }

    private void Flip( float horizontal){
        if(horizontal > 0 && !facingRight || horizontal <0 && facingRight ){
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void HandleAttacks(){
        
            
        if(attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            myAnimator.SetTrigger("attack");
        }
    }



    private void HandleInput(){
        if(Input.GetKeyDown(KeyCode.Space)){
            jump = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            attack = true;
        }

        if(Input.GetKeyDown(KeyCode.V)){
            Visible = !Visible;
        }


    }

    private void ResetValue(){
        attack = false;
        jump = false;
    }


    private bool IsGrounded(){
        if(myRigidbody.velocity.y<=0){
            foreach(Transform point in groundPoints){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++){
                    if(colliders[i].gameObject != gameObject){
                        return true;
                    }
                }
            }

            //return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDetector"){
            transform.position = respawnPoint;
        }

        if (collision.tag == "chasingEnemy")
        {
            transform.position = respawnPoint;
        }


        if(collision.tag == "Tree"){
            OnTree = true;
            Physics2D.IgnoreCollision(GameObject.Find("Tree_3").GetComponent<BoxCollider2D>(), GameObject.Find("Player").GetComponent<BoxCollider2D>(), true);
        }

        if (collision.tag == "Brige")
        {
            GameObject bridge = GameObject.Find("Bridge");
            bridge.transform.position = new Vector3(9,-3,0);
            Quaternion newr = Quaternion.Euler(0, 0, -90f);
            bridge.transform.rotation = Quaternion.Slerp(bridge.transform.rotation, newr, 1f);
        }

        if (collision.tag == "Bush")
        {
            movementSpeed = 1;
            //Debug.Log(movementSpeed);
           
        }




    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tree")
        {
            OnTree = false;
            Physics2D.IgnoreCollision(GameObject.Find("Tree_3").GetComponent<BoxCollider2D>(), GameObject.Find("Player").GetComponent<BoxCollider2D>(), false);
        }

        if (collision.tag == "Bush")
        {
            movementSpeed = storedSpeed;

        }
    }


}
