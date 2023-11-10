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

    [SerializeField] private GoalScript goal = null;

    [SerializeField] private PlayerScript player = null;

    [SerializeField] private GameObject SceneChangeDark = null;

    [SerializeField] private bool isSceneChange = false;

    private float darkT = 0.0f;

    private string nextSceneName = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        goal = GameObject.FindWithTag("Goal").GetComponent<GoalScript>();

        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();

        ArrowKeyInputUpdate();

        if (goal != null )
        {
            if (goal.GetIsClear() == true)
            {

                isSceneChange = false;
                SceneChanger(goal.GetNextStage() );
            }


        }

        SceneChange(nextSceneName);
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
            isSceneChange = true;

            player.SetIsClear(true);
        }


        nextSceneName = sceneName;

    }

    private void SceneChange(string sceneName)
    {
        if (sceneName != null)
        {

            if (isSceneChange == true)
            {

                SceneChangeDark.SetActive(true);
                DOTween.To(() => darkT, (value) => darkT = value, 1.0f, 1.0f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    SceneManager.LoadScene(sceneName);
                    DOTween.To(() => darkT, (value) => darkT = value, 0.0f, 1.0f).SetEase(Ease.OutBack).SetDelay(0.2f).OnComplete(() =>
                    {
                        player.SetIsClear(false);
                        darkT = 0.0f;
                        SceneChangeDark.SetActive(false);
                    });
                });
            }

            isSceneChange = false;
        }


        float size = darkT;

        SceneChangeDark.transform.localScale = new Vector3(size, 1.0f,1.0f);


    }
}
