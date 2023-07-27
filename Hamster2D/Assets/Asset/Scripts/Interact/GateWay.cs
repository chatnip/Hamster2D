using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateWay : Interactable
{
    [SerializeField] GameManager GameManager;

    public override string GetDescription()
    {
        return "[E]�� ���� ���� ����ϱ�";
    }

    public override void Interact()
    {
        if(GameManager.keyValue.Value > 0)
        {
            GameManager.keyValue.Value -= 1;
            this.gameObject.SetActive(false);
        }
    }
}
