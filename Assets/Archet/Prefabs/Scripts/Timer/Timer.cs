using System;
using UnityEngine;

namespace Timer
{
    public class Timer
    {
        public event Action<float> OnTimerValueChangedEvent;
        public event Action OnTimerFinishedEvent;

        public TimerType timerType { get; private set; }
        public float remainSeconds { get; private set; }
        public bool isPaused { get; private set; }

        public Timer(TimerType type)
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

        public void Start()
        {
            if (remainSeconds == 0)
            {
                Debug.LogError("TIMER: You are trying start timer with remaining seconds equals 0");
                OnTimerFinishedEvent?.Invoke();
            }

            isPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(remainSeconds);
        }

        public void Start(float seconds)
        {
            SetTime(seconds);
            Start();
        }

        public void Pause()
        {
            isPaused = true;
            Unsubscribe();
            OnTimerValueChangedEvent?.Invoke(remainSeconds);
        }

        public void Unpause()
        {
            isPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(remainSeconds);
        }

        public void Stop()
        {
            Unsubscribe();
            remainSeconds = 0;

            OnTimerValueChangedEvent?.Invoke(remainSeconds);
            OnTimerFinishedEvent?.Invoke();
        }

        private void Subscribe()
        {
            switch (timerType)
            {
                case TimerType.UpdateTick:
                    TimeInvoker.instance.OnUpdateTimeTickedEvent += OnUpdateTick;
                    break;
                case TimerType.UpdateTickUnscaled:
                    TimeInvoker.instance.OnUpdateTimeUnscaledTickedEvent += OnUpdateTick;
                    break;
                case TimerType.UpdateSecondTick:
                    TimeInvoker.instance.OnOneSecondTickedEvent += OnOneSecondTick;
                    break;
                case TimerType.UpdateSecondTickUnscaled:
                    TimeInvoker.instance.OnOneSecondUnscaledTickedEvent += OnOneSecondTick;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (timerType)
            {
                case TimerType.UpdateTick:
                    TimeInvoker.instance.OnUpdateTimeTickedEvent -= OnUpdateTick;
                    break;
                case TimerType.UpdateTickUnscaled:
                    TimeInvoker.instance.OnUpdateTimeUnscaledTickedEvent -= OnUpdateTick;
                    break;
                case TimerType.UpdateSecondTick:
                    TimeInvoker.instance.OnOneSecondTickedEvent -= OnOneSecondTick;
                    break;
                case TimerType.UpdateSecondTickUnscaled:
                    TimeInvoker.instance.OnOneSecondUnscaledTickedEvent -= OnOneSecondTick;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnUpdateTick(float deltaTime)
        {
            if (isPaused == true)
                return;

            remainSeconds -= deltaTime;
            CheckFinish();
        }

        private void OnOneSecondTick()
        {
            if (isPaused == true)
                return;

            remainSeconds -= 1f;
            CheckFinish();
        }

        private void CheckFinish()
        {
            if (remainSeconds <= 0)
                Stop();
            else
                OnTimerValueChangedEvent?.Invoke(remainSeconds);
        }
    }
}
