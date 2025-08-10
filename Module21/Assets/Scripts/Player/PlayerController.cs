using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndline = "Endline";
    public string tagToCheckCoin = "Coin";

    public GameObject startScreen;
    public GameObject endScreen;

    public bool invincible = false;

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    //privates
    private Vector3 _pos;
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        //ResetPosition();
        ResetSpeed();
    }

    void Update()
    {
        if (!_canRun)
        {
            return;
        }

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isPlayerCollider = false;

        if (collision.transform.tag == tagToCheckEnemy)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.thisCollider.name.Equals("PlayerContainer"))
                {
                    isPlayerCollider = true;
                }
            }

            if (isPlayerCollider && !invincible) {
                EndGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndline)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    public void StartToRun()
    {
        startScreen.SetActive(false);
        _canRun = true;
    }

    #region POWER UPS

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvincible(bool b = true)
    {
        invincible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        //var p = transform.position;
        //p.y = _startPosition.y + amount;
        //transform.position = p;

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(float animationDuration)
    {
        //var p = transform.position;
        //p.y = _startPosition.y;
        //transform.position = p;

        transform.DOMoveY(_startPosition.y, animationDuration);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }

    #endregion
}
