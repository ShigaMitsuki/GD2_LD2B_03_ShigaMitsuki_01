using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PanelScript : MonoBehaviour
{

    [SerializeField] private Vector3 rotation;
    private Vector3 preRotation;

    [SerializeField] private float rotateD = 2.0f;
    private float rotateT = 0.0f;

    private bool isRotating = false;
    private int rotateType = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotation = this.transform.eulerAngles;
        preRotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        rotation = this.transform.eulerAngles;

        if (isRotating == false)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                RotateStart(0);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                RotateStart(1);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                RotateStart(2);
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                RotateStart(3);
            }
        }
        else
        {

            rotateT += Time.deltaTime;
            rotateT = Mathf.Clamp(rotateT, 0.0f, rotateD);

            float t = rotateT / rotateD;

            float ta = t;
            Vector3 rotationA = preRotation;
            switch (rotateType)
            {
                case 0:

                    rotationA = preRotation;
                    rotationA.y = rotationA.y + 180.0f * ta;

                    rotation = rotationA;

                    break;
                case 1:

                     rotationA = preRotation;
                    rotationA.x = rotationA.x + 180.0f * ta;

                    rotation = rotationA;

                    break;
                case 2:

                     rotationA = preRotation;
                    rotationA.y = rotationA.y - 180.0f * ta;

                    rotation = rotationA;

                    break;
                case 3:

                     rotationA = preRotation;
                    rotationA.x = rotationA.x - 180.0f * ta;

                    rotation = rotationA;

                    break;
            }

            if(ta >= 1.0f)
            {
                isRotating = false;
            }
        }

        if (isRotating == false)
        {
            if (rotation.x >= 360)
            {
                rotation.x -= 360;
            }
            else if (rotation.x <= -360)
            {
                rotation.x += 360;
            }

            if (rotation.y >= 360)
            {
                rotation.y -= 360;
            }
            else if (rotation.y <= -360)
            {
                rotation.y += 360;
            }
        }

        this.transform.eulerAngles = rotation;
    }

    private void RotateStart(int rotatetype)
    {
        isRotating = true;
        preRotation = rotation;
        rotateType = rotatetype;
        rotateT = 0.0f;
    } 
}
