using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using DG.Tweening;
using System;

public class PanelScript : MonoBehaviour
{

    [SerializeField]  private float rotCount = 0;
    [SerializeField] private float preRotCount = 0;

    [SerializeField] private Vector3 rotation;

    [SerializeField] private int zCheck = 1;

    private bool isRotating = false;
    private int rotateType = 0;

    private GameManagerObject gameManager;


    // Start is called before the first frame update
    void Start()
    {
        rotation = this.transform.eulerAngles;

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerObject>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.R) && isRotating == false)
        {

            rotation.x = 0.0f;
            rotation.y = 0.0f;

        }

            if (isRotating == false)
        {
            int inputArrowKey = gameManager.GetInputArrowKey();

            if (inputArrowKey != -1)
            {
                isRotating = true;
                //preRotation = rotation;
                rotateType = inputArrowKey;

                rotCount = 0.0f;
                //rotCount++;

                DOTween.To(() => rotCount, (value) => rotCount = value, 180.0f, 1.0f).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    rotation.x = (int)Math.Round(rotation.x);
                    rotation.y = (int)Math.Round(rotation.y);

                    rotCount = 0.0f;
                    preRotCount = 0.0f;
                    isRotating = false;
                    gameManager.SetInputArrowKey(-1);
                });
            }
        }
        else
        {

            //if(rotCount > 179)
            //{
            //    rotCount = 180.0f;
            //}

            float rotPlus = rotCount - preRotCount;

            zCheck = 1;

            if (this.transform.localRotation.y != 0)
            {
                zCheck = -1;
            }

            switch (rotateType)
            {
                   case 0:

                    rotation.y += rotPlus;
                    break;
                case 1:

                    rotation.x -= rotPlus * zCheck;
                    break;
                case 2:

                    rotation.y -= rotPlus;
                    break;
                case 3:

                    rotation.x += rotPlus * zCheck;
                    break;
            }

            //Debug.Log(rotCount - preRotCount);
            preRotCount = rotCount;
        }


        if(rotation.x >= 360)
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

        

        this.transform.eulerAngles = rotation;
    }

    private void RotateStart(int rotatetype)
    {
        

    } 
}
