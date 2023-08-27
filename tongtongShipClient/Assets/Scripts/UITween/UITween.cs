using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UITween : MonoBehaviour
{
    private Sequence _sequence;

    private Action _actionEvent = null;

    private Vector3 _initialPosition;

    #region Pop Action.
    [field: SerializeField]
    private Vector3 defaultSize = Vector3.one;
    [field: SerializeField]
    private Vector3 pop_Action_TO = new Vector3(0.3f, 0.3f, 0.3f);
    [field: SerializeField]
    private float pop_Action_Duration = 0.2f;
    [field: SerializeField]
    private int pop_Action_Vibrato = 0;
    #endregion

    [field: SerializeField]
    private MainButtonManager _mainButtonManager;

    private void OnDisable()
    {
        QuitAllActions();
    }

    public void SinglePlayPoping()
    {
        SetActionEvent(_mainButtonManager.SinglePlayBtn);
        Poping();
    }

    public void MultiPlayPoping()
    {
        _mainButtonManager.MultiPlayBtn();
        Poping();
    }

    public void StorePoping()
    {
        SetActionEvent(_mainButtonManager.StoreBtn);
        Poping();
    }

    public void StatisticsPoping()
    {
        _mainButtonManager.StatisticsBtn();
        Poping();
    }

    public void CreditPoping()
    {
        _mainButtonManager.CreditsBtn();
        Poping();
    }

    public void ExitPoping()
    {
        SetActionEvent(_mainButtonManager.EndBtn);
        Poping();
    }

    public void Poping()
    {
        QuitAllActions();

        transform.localScale = defaultSize; // Do not make UI getting bigger.

        _sequence = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(transform.DOPunchScale(pop_Action_TO, pop_Action_Duration, pop_Action_Vibrato).SetEase(Ease.OutQuad))
        .OnComplete(() =>
        {
            if (_actionEvent != null) _actionEvent();
        });

        _sequence.Restart();
    }

    private void QuitAllActions()
    {
        _sequence.Pause();
        _sequence.Kill();
    }

    public void SetActionEvent(Action action)
    {
        _actionEvent = action;
    }

}
