using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] DialogManager DialogManager;
    public ScenarioBase scenario;

    void UpdateDialog()
    {
        DialogManager.StartCoroutine(DialogManager.DialogTexting(scenario));
    }

    public override string GetDescription()
    {
        return "[E]�� ���� ��ȭ�ϱ�";
    }

    public override void Interact()
    {
        if(!DialogManager.isScripting)
        {
            UpdateDialog();
        }
    }
}
