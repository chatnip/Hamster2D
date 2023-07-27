using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenSeed : Interactable
{
    [SerializeField] GameObject gameClearWindow;
    public override string GetDescription()
    {
        return "[E]�� ���� �ݱ�";
    }

    public override void Interact()
    {
        gameClearWindow.SetActive(true);
    }
}
