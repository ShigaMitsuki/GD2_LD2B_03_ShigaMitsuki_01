using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{

    private bool  isClear = false;
    [SerializeField] private int nextSage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {

            isClear = true;

        }
    }

    public bool GetIsClear()
    {
        if (isClear == true)
        {
            isClear = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetNextStage()
    {
        return nextSage;
    }
}
