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

    public void TalkAction(GameObject scanObj) // ��ȭ�ϱ�
    {
        scanObject = scanObj;
        if(DialogManager.isScripting != true)
        {
            NPC npc = scanObject.GetComponent<NPC>();
            ScenarioBase scenario = npc.scenario;
            DialogManager.StartCoroutine(DialogManager.DialogTexting(scenario));
        }
    }

    public void GetAction(GameObject scanObj) // �عٶ�⾾ ����
    {
        scanObject = scanObj;
        // �عٶ�⾾ �þ��
    }

    public void ChestAction(GameObject scanObj) // ���� ����
    {
        scanObject = scanObj;
        Chest chest = scanObject.GetComponent<Chest>();
        
    }
}
