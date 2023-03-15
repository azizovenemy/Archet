using UnityEngine;

public class DetectableObject : MonoBehaviour, IDetectableObject
{
    public event ObjectDetectedHandler OnGameObjectDetectEvent;
    public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    public void Detected(GameObject detectionSource)
    {
          OnGameObjectDetectEvent?.Invoke(detectionSource, this.gameObject);
    }

    public void DetectionReleased(GameObject detectionSource)
    {
        OnGameObjectDetectionReleasedEvent?.Invoke(detectionSource, this.gameObject);
    }
}
