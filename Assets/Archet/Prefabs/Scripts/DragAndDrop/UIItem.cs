using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private Canvas _mainCanvas;
    private RectTransform _rectTransform;

    private void Start()
    {
        _mainCanvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTransform = _rectTransform.parent;
        slotTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
}
