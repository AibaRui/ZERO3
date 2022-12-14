using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingFirePrefab : MonoBehaviour
{
    GameObject _enemy;
    [SerializeField] float _speed = 3;

    [Tooltip("持続時間")]
    [SerializeField] float _destroyTimeLimit = 3;
    float _destroyTimeCount = 0;

    Vector3 _velo;
    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _enemy = FindObjectOfType<TargetSystem>()._targetEnemy.gameObject;
        float _ve = _enemy.transform.position.x - transform.position.x;
        if (_ve < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    /// <summary>弾を敵に追尾させる</summary>
    private void FixedUpdate()
    {
        if (FindObjectOfType<TargetSystem>()._targetEnemy == _enemy && FindObjectOfType<TargetSystem>()._targetEnemy != null)
        {
            _enemy = FindObjectOfType<TargetSystem>()._targetEnemy.gameObject;
            _velo = _enemy.transform.position - this.gameObject.transform.position;

            _rb.velocity = _velo.normalized * _speed;
        }
        else
        {
            _rb.velocity = _rb.velocity;
        }


    }
    /// <summary>弾の生存時間のカウント</summary>
    private void Update()
    {

        _destroyTimeCount += Time.deltaTime;

        if (_destroyTimeCount > _destroyTimeLimit)
        {
            Destroy(gameObject);
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}