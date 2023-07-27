using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField] ObjectPooling ObjectPooling;
    [SerializeField] PlayerController PlayerController;
    [SerializeField] float bulletSpeed;
    Vector2 bulletVector;
    [SerializeField] Rigidbody2D rigid;
    [HideInInspector] public Vector3 targetVector;

    private void OnEnable()
    {
        StartCoroutine(BulletTimer());
    }
    private IEnumerator BulletTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
        BulletPick();
        yield break;
    }

    public void BulletMove()
    {
        bulletVector = targetVector - transform.position;
        bulletVector.Normalize();
        rigid.velocity = bulletVector * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            BulletPick();
        }
    }

    private void BulletPick()
    {
        ObjectPooling.BulletObjectPick(this);
    }
}
