using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action<float> OnTimerValueChangedEvent;
    public event Action OnTimerFinishedEvent;

    public TimerType timerType { get; private set; }
    public float remainSeconds { get; private set; }
    public bool isPaused { get; private set; }

    public Timer (TimerType type)
    {
        timerType = type;
    }

    public Timer(TimerType type, float seconds)
    {
        timerType = type;
        SetTime(seconds);
    }

    public void SetTime(float seconds)
    {
        remainSeconds = seconds;
        OnTimerValueChangedEvent?.Invoke(remainSeconds);
    }

    public void Start() { }
    public void Start(float seconds) 
    {
        if(remainSeconds == 0)
        {
            Debug.LogError("TIMER: You are trying start timer with remaining seconds equals 0");
            OnTimerFinishedEvent?.Invoke();
        }

        isPaused = false;
    }
}
