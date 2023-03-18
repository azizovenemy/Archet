using UnityEngine;

[RequireComponent(typeof(DetectableObject))]
public class DetectableObjectReactionColor : MonoBehaviour
{
    [SerializeField] private Color _colorReaction = Color.white;

    private IDetectableObject _detectableObject;
    private Color _colorByDefault;
    private Material _material;

    private void Awake()
    {
        _detectableObject = GetComponent<IDetectableObject>();

        var myRenderer = GetComponentInChildren<MeshRenderer>();

        _material = myRenderer.material;
    }

    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectEvent += OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent += OnGameObjectDetectionReleased;
    }

    private void OnDisable()
    {
        _detectableObject.OnGameObjectDetectEvent -= OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent -= OnGameObjectDetectionReleased;
    }

    private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
    {
        SetColor(_colorReaction);
    }

    private void OnGameObjectDetectionReleased(GameObject source, GameObject detectedObject)
    {
        SetColor(_colorByDefault);
    }

    private void SetColor(Color color)
    {
        _material.SetColor("_BaseColor", color);
    }
}
