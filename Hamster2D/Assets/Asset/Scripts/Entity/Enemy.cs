using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Enemy : MonoBehaviour
{
    [Header("*Components")]
    [SerializeField] GameManager GameManager;
    [SerializeField] PlayerController PlayerController;
    [SerializeField] ObjectPooling ObjectPooling;

    [Header("*Enemy")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject health;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator enemyAnimator;

    [Header("*Stat")]
    [SerializeField] protected float enemySpeed;
    [SerializeField] public ReactiveProperty<EnemyBase> enemyBase = new ReactiveProperty<EnemyBase>();

    [HideInInspector] public bool isKey;

    IntReactiveProperty CurrentHP = new IntReactiveProperty();
    private float distance;

    delegate void Moving();
    Moving moving;

    public Sprite GetSprite { get { return enemyBase.Value.EnemySprite; } }
    public int GetMaxHP { get { return enemyBase.Value.MaxHp; } }
    public float GetMoveSpeed { get { return enemyBase.Value.MoveSpeed; } }
    public float GetDistance { get { return enemyBase.Value.Distance; } }

    private void Awake()
    {
        SetStat();

        CurrentHP
            .Subscribe(hp =>
            {
                if (hp > 0)
                {
                    SetHPbar(hp);
                    return;
                }
                SetHPbar(0);
                EnemyDie();
            });
    }

    private void FixedUpdate()
    {
        if(moving != null)
        {
            moving();
        }
    }

    private void OnEnable()
    {
        SetStat();

    }

    private void SetStat()
    {
        CurrentHP.Value = GetMaxHP;
    }

    protected virtual void SetHPbar(int currentHP)
    {
        float HpBar_X_Scale = (float)currentHP / GetMaxHP;
        HpBar_X_Scale = Mathf.Clamp(HpBar_X_Scale * 0.1f, 0, 1);
        health.transform.localScale = new Vector3(HpBar_X_Scale, 0.2f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            int damage = PlayerController.damage.Value;
            CurrentHP.Value -= damage;
        }
    }
    public void EnemyDie()
    {
        ObjectPooling.EnemyObjectPick(this);
        moving -= new Moving(Track);
        moving -= new Moving(Teleport);
        if (isKey == true)
        {
            GameManager.keyValue.Value += 1;
            isKey = false;
        }
    }

    public void SelectType(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Track:
                enemyAnimator.SetInteger("EnemyType", 0);
                moving += new Moving(Track);
                break;
            case EnemyType.Teleport:
                enemyAnimator.SetInteger("EnemyType", 1);
                moving += new Moving(Teleport);
                break;
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }

    private void Track()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 enemyDir = target.transform.position - transform.position;
        enemyDir.Normalize();

        if(enemyDir.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (distance < GetDistance)
        {
            transform.Translate(enemyDir * GetMoveSpeed * Time.deltaTime);
        }
    }

    private void Teleport()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        float rand = Random.Range(-2, 2);

        if(distance < GetDistance)
        {
            transform.localPosition += new Vector3(rand, rand, 0);
        }
    }
}

[System.Serializable]
public enum EnemyType
{
    Track,
    Teleport
}
