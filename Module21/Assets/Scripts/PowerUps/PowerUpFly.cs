using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUpFly : PowerUpBase
{
    [Header("Power Up Height")]
    public float amountHeight = 2;
    public float animationDuration = .1f;
    public DG.Tweening.Ease ease = DG.Tweening.Ease.OutBack;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(amountHeight, duration, animationDuration, ease);
        PlayerController.Instance.SetPowerUpText("Fly");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetHeight(animationDuration);
        PlayerController.Instance.SetPowerUpText("");
    }
}
