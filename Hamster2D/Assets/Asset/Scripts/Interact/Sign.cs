using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    [SerializeField] DialogManager DialogManager;
    public ScenarioBase scenario;

    void UpdateDialog()
    {
        DialogManager.StartCoroutine(DialogManager.DialogTexting(scenario));
    }

    public override string GetDescription()
    {
        return "[E]를 눌러 보기";
    }

    public override void Interact()
    {
        if(!DialogManager.isScripting)
        {
            UpdateDialog();
        }
    }
}
