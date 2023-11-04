using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerGeneratorScript : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool Loaded { get; set; }

    [SerializeField]
    GameObject[] gameManagerPrefabs = null;

    void Awake()
    {
        //���łɃ��[�h�ς݂Ȃ�A��d�ɍ쐬���Ȃ�
        if (Loaded) return;

        Loaded = true;

        //�v���n�u���C���X�^���X�����āADon'tDestroyOnLoad
        foreach (var prefab in gameManagerPrefabs)
        {
            GameObject go = Instantiate(prefab);
            DontDestroyOnLoad(go);
        }
    }
}
