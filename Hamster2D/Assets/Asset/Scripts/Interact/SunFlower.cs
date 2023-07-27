using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Interactable
{
    [SerializeField] GameManager GameManager;

    public override string GetDescription()
    {
        return "[E]�� �� ���� �عٶ�⾾ ���";
    }

    public override void Interact()
    {
        GameManager.seedValue.Value += 10;
    }
}
