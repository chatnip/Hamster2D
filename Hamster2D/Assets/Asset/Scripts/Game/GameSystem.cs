using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameSystem : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("*HUD")]
    [SerializeField] public Image health;
    [SerializeField] Text bulletText;
    [SerializeField] Text KeyText;

    private void Awake()
    {

        GameManager.seedValue
            .Subscribe(x =>
            {
                bulletText.text = "Seed : " + x.ToString();
            });

        GameManager.keyValue
            .Subscribe(x =>
            {
                KeyText.text = "Key : " + x.ToString();
            });
    }

}
