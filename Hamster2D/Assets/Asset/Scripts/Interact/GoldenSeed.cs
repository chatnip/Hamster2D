using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenSeed : Interactable
{
    [SerializeField] GameObject gameClearWindow;
    public override string GetDescription()
    {
        return "[E]를 눌러 줍기";
    }

    public override void Interact()
    {
        gameClearWindow.SetActive(true);
    }
}
