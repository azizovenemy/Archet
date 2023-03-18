using UnityEngine;

public delegate void ObjectDetectedHandler(GameObject source, GameObject detectedObject);

public interface IDetector
{
    event ObjectDetectedHandler OnGameObjectDetectedEvent;
    event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    void Detect(IDetectableObject detectableObject);

    void ReleaseDetection(IDetectableObject detectableObject);
}
