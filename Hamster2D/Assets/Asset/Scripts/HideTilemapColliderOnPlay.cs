using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideTilemapColliderOnPlay : MonoBehaviour
{

    [SerializeField] TilemapRenderer tilemapRenderer;

    void Start()
    {
        tilemapRenderer.enabled = false;
    }
}
