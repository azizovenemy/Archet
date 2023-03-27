using UnityEngine;

namespace Timer
{
    public class TimerExample : MonoBehaviour
    {
        [SerializeField] private TimerType _timerType;
        [SerializeField] private float _timerSeconds;

        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer(_timerType, _timerSeconds);
            _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
            _timer.OnTimerFinishedEvent += OnTimerFinished;
        }

        private void OnDestroy()
        {
            _timer.OnTimerValueChangedEvent -= OnTimerValueChanged;
            _timer.OnTimerFinishedEvent -= OnTimerFinished;
        }

        private void OnTimerFinished()
        {
            Debug.Log("Timer finished");
        }

        private void OnTimerValueChanged(float remainingSeconds)
        {
            Debug.Log($"Timer ticked. Remaining seconds: ({remainingSeconds})");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                StartTimerClicked();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                PuseTimerClicked();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                StopTimerClicked();
            }
        }

        private void StartTimerClicked()
        {
            _timer.Start();
        }

        private void PuseTimerClicked()
        {
            if (_timer.isPaused)
                _timer.Unpause();
            else
                _timer.Pause();
        }

        private void StopTimerClicked()
        {
            _timer.Stop();
        }
    }
}

