using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] GameManager GameManager;

    public override string GetDescription()
    {
        return "[E]를 꾹 눌러 상자 열기";
    }

    // 나중에 아이템 넣어놓기
    public override void Interact()
    {
        GameManager.keyValue.Value += 1;
        gameObject.SetActive(false);
    }
}
