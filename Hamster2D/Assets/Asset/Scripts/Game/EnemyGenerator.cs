using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] PlayerController PlayerController;
    [SerializeField] ObjectPooling ObjectPooling;
    [SerializeField] CircleCollider2D area;
    [SerializeField] EnemyType enemyType;
    [SerializeField] EnemyBase enemyBase;
    [SerializeField] int count;
    [SerializeField] bool isActive;
    [SerializeField] bool isPuzzle;

    private void Start()
    {
        isActive = false;
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;
        float size = area.radius;

        //x, yÃà ·£´ý ÁÂÇ¥ ¾ò±â
        float posX = basePosition.x + Random.Range(-size, size);
        float posY = basePosition.y + Random.Range(-size, size);

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    private void SpawnEnemy()
    {
        for(int i = 0; i < count; i++)
        {
            Debug.Log("spawn");
            Enemy enemy = ObjectPooling.EnemyObjectPool();
            enemy.SelectType(enemyType);
            enemy.enemyBase.Value = enemyBase;
            enemy.transform.position = GetRandomPosition();
            if(i == 0 && isPuzzle)
            {
                enemy.isKey = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && isActive == false)
        {
            SpawnEnemy();
            isActive = true;
        }
    }
}
