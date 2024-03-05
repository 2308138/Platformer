using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class COOLDOWN
{
    public enum Progress
    {
        Ready,
        Started,
        InProgress,
        Finished
    }

    public Progress CurrentProgress = Progress.Ready;

    public float timeLeft { get { return currentDuration; } }
    public bool isOnCooldown { get { return _isOnCooldown; } }

    public float duration = 1F;
    public float currentDuration = 0F;

    private bool _isOnCooldown = false;
    private Coroutine _coroutine;

    public void StartCooldown()
    {
        if (CurrentProgress is Progress.Started or Progress.InProgress)
            return;
        _coroutine = COROUTINEHOST.Instance.StartCoroutine(DoCooldown());
    }

    public void StopCooldown()
    {
        if (_coroutine != null)
        {
            COROUTINEHOST.Instance.StopCoroutine(_coroutine);

            currentDuration = 0F;
            _isOnCooldown = false;
            CurrentProgress = Progress.Ready;
        }
    }

    IEnumerator DoCooldown()
    {
        CurrentProgress = Progress.Started;
        currentDuration = duration;
        _isOnCooldown = true;

        while (currentDuration > 0F)
        {
            currentDuration -= Time.deltaTime;
            CurrentProgress = Progress.InProgress;

            yield return null;
        }

        currentDuration = 0F;
        _isOnCooldown = false;

        CurrentProgress = Progress.Finished;
    }
}