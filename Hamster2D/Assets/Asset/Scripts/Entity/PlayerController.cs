using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
    [Header("*Components")]
    [SerializeField] DialogManager DialogManager;
    [SerializeField] InteractManager InteractManager;
    [SerializeField] ObjectPooling ObjectPooling;
    [SerializeField] GameSystem GameSystem;
    [SerializeField] GameManager GameManager;

    [Header("*Player")]
    [SerializeField] Rigidbody2D playerRigid;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject gameOverWindow;
    [HideInInspector] public Vector2 playerDir;
    [HideInInspector] public Vector3 mouseDir;
    float angleValue;


    [Header("*Stat")]
    [SerializeField] float playerSpeed;
    public IntReactiveProperty damage = new IntReactiveProperty();
    [SerializeField] int maxHp;
    [SerializeField] int maxSeed;
    IntReactiveProperty CurrentHP = new IntReactiveProperty();

    ReactiveProperty<GameObject> scanObject = new ReactiveProperty<GameObject>();
    GameObject tempObject;
    public bool isAction;
    bool isHorizonMove;
    public Vector2 rayDir;

    private void Awake()
    {
        CurrentHP.Value = maxHp;
        GameManager.seedValue.Value = maxSeed;

        CurrentHP
            .Subscribe(hp =>
            {
                if (hp > 0)
                {
                    SetHPbar(hp);
                    return;
                }
                SetHPbar(0);
                gameOverWindow.SetActive(true);
            });

        GameManager.seedValue
            .Where(x => x > maxSeed)
            .Subscribe(x =>
            {
                GameManager.seedValue.Value = maxSeed;
            });
    }
    private void SetHPbar(int currentHP)
    {
        float HpBar_X_Scale = (float)currentHP / maxHp;
        HpBar_X_Scale = Mathf.Clamp(HpBar_X_Scale, 0, 1);
        GameSystem.health.fillAmount = HpBar_X_Scale;
    }

    private void Move() // 플레이어 이동
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(moveX == 1 )
        {
            rayDir = Vector2.right;
            spriteRenderer.flipX = true;
        }
        else if(moveX == -1)
        {
            rayDir = Vector2.left;
            spriteRenderer.flipX = false;
        }
        else if(moveY == 1)
        {
            rayDir = Vector2.up;
        }
        else if(moveY == -1)
        {
            rayDir = Vector2.down;
        }

        playerDir = new Vector2(moveX, moveY) * playerSpeed * Time.deltaTime;
        if(DialogManager.isScripting != true)
        {
            transform.Translate(playerDir);
        }
    }

    private void Mouse()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angleValue = Mathf.Atan2(mouseDir.y - transform.position.y, mouseDir.x - transform.position.x) * Mathf.Rad2Deg;
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (GameManager.seedValue.Value > 0)
        {
            GameManager.seedValue.Value -= 1;
            Bullet bullet = ObjectPooling.BulletObjectPool();
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = Quaternion.AngleAxis(angleValue - 90, Vector3.forward);
            bullet.tag = "PlayerBullet";
            bullet.targetVector = mouseDir;
            bullet.BulletMove();
        }
    }

    private void Eat()
    {
        if(Input.GetMouseButtonDown(1))
        {
            CurrentHP.Value += 1;
            GameManager.seedValue.Value -= 1;
        }
    }

    private void Update()
    {
        Mouse();
        Eat();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CurrentHP.Value -= 1;
        }
    }
}
