using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerObject>();
    }

    // Update is called once per frame
    void Update()
    {
        int inputArrowKey = gameManager.GetInputArrowKey();

        if (inputArrowKey == -1)
        {
            isStop = false;
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

          if (isStop == false)
        {


            totalVelocity.x = 0;

            if (Input.GetKey(KeyCode.D))
            {
                totalVelocity.x = walkSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                totalVelocity.x = -walkSpeed;
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
        else if(other.tag == "Gate")
        {
            inGate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            inLadder = false;
            nearLadder = null;
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

}
