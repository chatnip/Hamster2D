using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [Header("*Components")]
    [SerializeField] DialogManager DialogManager;

    public GameObject scanObject;
    public bool isAction;
    public bool isTalking;

    public void TalkAction(GameObject scanObj) // 대화하기
    {
        scanObject = scanObj;
        if(DialogManager.isScripting != true)
        {
            NPC npc = scanObject.GetComponent<NPC>();
            ScenarioBase scenario = npc.scenario;
            DialogManager.StartCoroutine(DialogManager.DialogTexting(scenario));
        }
    }

    public void GetAction(GameObject scanObj) // 해바라기씨 수급
    {
        scanObject = scanObj;
        // 해바라기씨 늘어나기
    }

    public void ChestAction(GameObject scanObj) // 상자 열기
    {
        scanObject = scanObj;
        Chest chest = scanObject.GetComponent<Chest>();
        
    }
}
