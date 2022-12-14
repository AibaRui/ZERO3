using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownAttack : MonoBehaviour
{

    [SerializeField] AttackCloseController _attackCloseController;

    [Header("攻撃時の移動スピード")]
    [Tooltip("攻撃時の移動スピード")] [SerializeField] float _attackSpeed = 3;


    [Header("降下攻撃のエフェクト")]
    [Tooltip("攻撃時の移動スピード")] [SerializeField] GameObject _downAttackEffect;
    AudioSource _aud;
    [SerializeField] AudioClip audioClip;

    bool _isDownNow = false;
    bool _isGround = false;
    Animator _anim;
    [SerializeField] Animator _weaponAnim;
    Rigidbody _rb;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _anim = gameObject.GetComponent<Animator>();
        _weaponAnim = _weaponAnim.GetComponent<Animator>();
        _aud = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (_attackCloseController._closeAttack && _attackCloseController._pushdKey == AttackCloseController.PushdKey.DownAttack)
        {
            Effect();
        }

    }

    public void Attack()
    {
        _anim.Play("P_AttackDown");
        _isDownNow = true;
        _attackCloseController.airTime = 0;
        _attackCloseController._downSpeed = false;

        _rb.AddForce(-1 * transform.up * _attackSpeed, ForceMode.Impulse);
    }

    public void Effect()
    {
        if (_isDownNow && _isGround)
        {
            _aud.PlayOneShot(audioClip);
            StartCoroutine(CoolTime());
            var effect = Instantiate(_downAttackEffect); //エフェクトを出す
            effect.transform.position = transform.position;
            _isDownNow = false;
            _weaponAnim.Play("DownAttack");

        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(1);
        _attackCloseController._closeAttack = false;
        _attackCloseController._isAttackNow = false;
    }

    public void MoveEnd()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = true;

            if (_isDownNow)         //降下攻撃のエフェクト
            {
                _aud.PlayOneShot(audioClip);
                _weaponAnim.Play("DownAttack");
                // okAttack = false;
                var effect = Instantiate(_downAttackEffect); //エフェクトを出す
                effect.transform.position = transform.position;
                _isDownNow = false;
                _attackCloseController._closeAttack = false;
                _attackCloseController._isAttackNow = false;
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = false;
        }
    }
}
