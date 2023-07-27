using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Hamster/Create New Enemy")]
public class EnemyBase : ScriptableObject
{
    [Header("*Stat Setting")]
    [SerializeField] Sprite enemySprite;
    [SerializeField] int maxHp;
    [SerializeField] float moveSpeed;
    [SerializeField] float distance;

    #region Stat Setting
    public Sprite EnemySprite
    {
        get { return enemySprite; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }
    public float Distance
    {
        get { return distance; }
    }

    #endregion
}