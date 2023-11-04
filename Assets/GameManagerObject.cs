using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerObject : MonoBehaviour
{

    private int inputArrowKey = -1;


    [SerializeField] float arrowKeyCoolDownMax = 1.0f;
   private float arrowKeyCoolDown = 1.0f;

    [SerializeField]  private bool canInputArrowKey = true;

    private GoalScript goal = null;

    [SerializeField] private GameObject SceneChangeDark = null;

    [SerializeField] private bool isSceneChange = false;

    private float darkT = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.FindWithTag("Goal").GetComponent<GoalScript>();

        SceneChangeDark = GameObject.FindWithTag("Dark");
    }

    // Update is called once per frame
    void Update()
    {

        ArrowKeyInputUpdate();

        if (goal != null )
        {
            if (goal.GetIsClear() == true)
            {
                isSceneChange = false;
                SceneChanger(goal.GetNextStage() );
            }
        }

        SceneChange();
    }

    public int GetInputArrowKey()
    {
        return inputArrowKey;
    }

    public void SetInputArrowKey(int num)
    {
       inputArrowKey = num;
    }

    private void ArrowKeyInputUpdate()
    {
        if (canInputArrowKey == true)
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
    }

    private void SceneChanger(int stageNum)
    {

        string sceneName = null;

        if (stageNum == 1)
        {
            sceneName = "Stage01";
        }
        else if (stageNum == 2)
        {
            sceneName = "Stage02";
        }
        else if (stageNum == 3)
        {
            sceneName = "Stage03";
        }
        else if (stageNum == 4)
        {
            sceneName = "Stage04";
        }
        else if (stageNum == 5)
        {
            sceneName = "Stage05";
        }
        else if (stageNum == 6)
        {
            sceneName = "Stage00";
        }

        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }

    }

    private void SceneChange()
    {
        if (isSceneChange == true)
        {
            DOTween.To(() => darkT, (value) => darkT = value, 1.0f, 10.0f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                isSceneChange = false;
            });
        }

         SceneChangeDark.SetActive(isSceneChange);
        
    }
}
