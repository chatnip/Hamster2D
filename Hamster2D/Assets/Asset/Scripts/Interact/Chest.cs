using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] GameManager GameManager;

    public override string GetDescription()
    {
        return "[E]�� �� ���� ���� ����";
    }

    // ���߿� ������ �־����
    public override void Interact()
    {
        GameManager.keyValue.Value += 1;
        gameObject.SetActive(false);
    }
}
