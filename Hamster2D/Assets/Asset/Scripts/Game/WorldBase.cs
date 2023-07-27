using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "Hamster/Create New World")]

public class WorldBase : ScriptableObject
{
    [SerializeField] GameObject worldObject;
    [SerializeField] List<EnemyRespawn> enemyRespawns;
    public GameObject WorldObject
    {
        get { return worldObject; }
    }
    public List<EnemyRespawn> EnemyRespawns
    {
        get { return enemyRespawns; }
    }
}

[System.Serializable]
public class EnemyRespawn
{
    [SerializeField] EnemyGenerator enemyGenerator;
    [SerializeField] Vector2 genVector;
    public EnemyGenerator EnemyGenerator
    {
        get { return enemyGenerator; }
    }
    public Vector2 GenVector
    {
        get { return genVector; }
    }
}