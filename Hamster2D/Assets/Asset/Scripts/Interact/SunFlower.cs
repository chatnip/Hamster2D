using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Interactable
{
    [SerializeField] GameManager GameManager;

    public override string GetDescription()
    {
        return "[E]를 꾹 눌러 해바라기씨 얻기";
    }

    public override void Interact()
    {
        GameManager.seedValue.Value += 10;
    }
}
