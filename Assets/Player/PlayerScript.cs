using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private bool isStop = false;

    private Rigidbody rb;

    private Vector3 totalVelocity = Vector3.zero;
    private Vector3 preTotalVelocity = Vector3.zero;

    [SerializeField] float walkSpeed = 3.0f;

    private GameManagerObject gameManager;

    [SerializeField]  private bool inLadder = false;
    [SerializeField] private bool isGrabLadder = false;
    [SerializeField] private bool inGate = false;

    private GameObject nearLadder = null;

    [SerializeField] private GameObject tester;

    private Animator anim = null;

    private bool isClear = false;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerObject>();
 
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int inputArrowKey = gameManager.GetInputArrowKey();

        if (inputArrowKey == -1 )
        {
            
                isStop = isClear;
        }
        else
        {
            isStop = true;
        }




        if (isStop == true)
        {
            rb.isKinematic = true;
        }
        else
        {
             if (rb.isKinematic == false)
            {
                totalVelocity = rb.velocity;
            }
            rb.isKinematic = false;

           
        }
          if (isStop == false )
        {


            totalVelocity.x = 0;
                 anim.SetBool("isWalk",false);


            if(isGrabLadder == true)
            {
                canMove = true;
            }

            if (canMove == true)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    totalVelocity.x = walkSpeed;
                    anim.SetBool("isWalk", true);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    totalVelocity.x = -walkSpeed;
                    anim.SetBool("isWalk", true);
                }
            }

            if (inLadder == true)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    isGrabLadder = true;
                  
                }
            }
            else
            {
                isGrabLadder = false;
                rb.useGravity = true;
            }

            if (isGrabLadder == true)
            {
                rb.useGravity = false;
                totalVelocity.y = 0;
            }


                if (isGrabLadder == true)
            {
                if (nearLadder != null)
                {

                    Vector3 ladderPos = nearLadder.transform.position;
                    Vector3 playerPos = this.transform.position;

                    float height = nearLadder.GetComponent<Renderer>().bounds.size.y;

                    ladderPos.y = ladderPos.y + height * 0.5f ;

                    if (playerPos .y < ladderPos.y) {
                        if (Input.GetKey(KeyCode.W))
                        {
                            totalVelocity.y = walkSpeed;
                            //tester.transform.position = ladderPos;
                        }
                    }
                }
            }
            else if (inGate == true)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Vector3 pos = this.transform.position;
                    if (pos.z == 0)
                    {
                        pos.z = 1;
                    }
                    else
                    {
                        pos.z = 0;
                    }
                    this.transform.position = pos;
                }
            }

            

            rb.velocity = totalVelocity;

        }

          if(this.transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
         

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ladder")
        {
            inLadder = true;
            nearLadder = other.gameObject;
        }
        else if (other.tag == "Gate")
        {
            inGate = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ladder")
        {
            inLadder = true;
            nearLadder = other.gameObject;
        }
        else if (other.tag == "Gate")
        {
            inGate = true;
        }

        
    }

    private void OnCollisionStay(Collision collision)
    { 
            
        if (collision.gameObject.tag == "Stage")
        {

            ContactPoint[] contacts = collision.contacts;

            Vector3 otherNormal = contacts[0].normal;

            Vector3 upVector = new Vector3(0, 1, 0);

            float dotUN = Vector3.Dot(upVector,otherNormal);

            float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;


            if(dotDeg <= 45) {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
               

            Debug.Log(canMove);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            canMove = false;
        }
    }

        private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            inLadder = false;
            nearLadder = null;

            canMove = false;
            if (isGrabLadder == true)
            {

                totalVelocity.y = 0;
                rb.velocity = totalVelocity;
            }
        }
        
        if (other.tag == "Gate")
        {
            inGate = false;
        }
    }

    public void SetIsClear(bool isClear_)
    {
        isClear = isClear_;
    }

}
