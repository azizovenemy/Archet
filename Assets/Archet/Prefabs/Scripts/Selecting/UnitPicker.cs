using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitPicker : MonoBehaviour
{
    public event UnityAction<Unit[]> OnUnitsPicked;

    [SerializeField] private RectDrawer _rectDrawer;

    private Unit[] _units;

    void Start()
    {
        _units = FindObjectsOfType<Unit>();
        _rectDrawer.OnEndOfDraw += PickUnits;
    }

    private void PickUnits(Rect rect)
    {
        List<Unit> result = new List<Unit>();

        foreach(var unit in _units)
        {
            Vector2 unitCoords = GetUnitsScreenPosition(unit);

            if (rect.Contains(unitCoords))
            {
                result.Add(unit);
            }
        }

        OnUnitsPicked?.Invoke(result.ToArray());   
    }

    private Vector2 GetUnitsScreenPosition(Unit unit)
    {
        var unitOnCameraPosition = Camera.main.WorldToScreenPoint(unit.transform.position);

        return new Vector2(unitOnCameraPosition.x, Screen.height - unitOnCameraPosition.y);
    }
}
