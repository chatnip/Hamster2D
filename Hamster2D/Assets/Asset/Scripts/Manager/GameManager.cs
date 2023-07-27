using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameManager : Manager<GameManager>
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject SaveText;
    [HideInInspector] public Vector3 playerVector;
    [HideInInspector] public IntReactiveProperty seedValue = new IntReactiveProperty();
    [HideInInspector] public IntReactiveProperty keyValue = new IntReactiveProperty();
    public Text WorldText;

    public void Save()
    {
        PlayerPrefs.SetInt("Seed", seedValue.Value);
        PlayerPrefs.SetInt("Key", keyValue.Value);
        PlayerPrefs.SetFloat("VectorX", player.transform.localPosition.x);
        PlayerPrefs.SetFloat("VectorX", player.transform.localPosition.y);
        PlayerPrefs.SetString("World", WorldText.text);
        StartCoroutine(PopupText());
    }

    public void Load()
    {
        seedValue.Value = PlayerPrefs.GetInt("Seed");
        keyValue.Value = PlayerPrefs.GetInt("Key");
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("VectorX"), PlayerPrefs.GetFloat("VectorY"), 0);
        WorldText.text = PlayerPrefs.GetString("World");
    }

    public void SaveReset()
    {
        seedValue.Value = 30;
        keyValue.Value = 0;
        player.transform.position = new Vector3(0.4f, 1.3f, 0);
        WorldText.text = "µµ≈‰∏Æ Ω£";
    }

    public IEnumerator PopupText()
    {
        SaveText.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        SaveText.SetActive(false);
        yield break;
    }
}
