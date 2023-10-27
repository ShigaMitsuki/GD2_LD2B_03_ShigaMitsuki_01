using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManagerObject : MonoBehaviour
{

    private int inputArrowKey = -1;


    [SerializeField] float arrowKeyCoolDownMax = 1.0f;
    [SerializeField] private float arrowKeyCoolDown = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        if (inputArrowKey == -1)
        {
            if (arrowKeyCoolDown <= 0.0f)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    inputArrowKey = 0;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    inputArrowKey = 1;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    inputArrowKey = 2;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    inputArrowKey = 3;
                }
            }
            else
            {
                arrowKeyCoolDown -= Time.deltaTime;
            }
        }
        else
        {
            arrowKeyCoolDown = arrowKeyCoolDownMax;
        }
    }


    public int GetInputArrowKey()
    {
        return inputArrowKey;
    }

    public void SetInputArrowKey(int num)
    {
       inputArrowKey = num;
    }
}
