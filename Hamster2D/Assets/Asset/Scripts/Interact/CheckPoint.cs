using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Interactable
{
    [SerializeField] GameManager GameManager;
    [SerializeField] Transform player;
    [SerializeField] string worldName;

    public override string GetDescription()
    {
        return "[E]�� ���� �����ϱ�";
    }

    public override void Interact()
    {
        GameManager.WorldText.text = worldName;
        GameManager.playerVector = player.transform.position;
        GameManager.Save();
    }
}
