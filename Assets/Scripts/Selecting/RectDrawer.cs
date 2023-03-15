using UnityEngine;
using UnityEngine.Events;

public class RectDrawer : MonoBehaviour
{
    public event UnityAction<Rect> OnEndOfDraw;

    [SerializeField] private GUISkin _skin;

    private bool _isDrawing;
    private Vector2 _startDrawingPosition, _endDrawingPosition;
    private Rect _currentRect;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startDrawingPosition = Input.mousePosition;
            _isDrawing = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDrawing = false;
            OnEndOfDraw?.Invoke(_currentRect);
        }
    }

    private void OnGUI()
    {
        GUI.skin = _skin;

        if (_isDrawing)
        {
            _endDrawingPosition = Input.mousePosition;
            if (_startDrawingPosition == _endDrawingPosition)
            {
                return;
            }

            _currentRect = new Rect(Mathf.Min(_endDrawingPosition.x, _startDrawingPosition.x),
                Screen.height - Mathf.Min(_endDrawingPosition.y, _startDrawingPosition.y),
                Mathf.Max(_endDrawingPosition.x, _startDrawingPosition.x) - Mathf.Min(_endDrawingPosition.x, _startDrawingPosition.x),
                Mathf.Max(_endDrawingPosition.y, _startDrawingPosition.y) - Mathf.Min(_endDrawingPosition.y, _startDrawingPosition.y)
                );

            GUI.Box(_currentRect, "");
        }
    }
}
