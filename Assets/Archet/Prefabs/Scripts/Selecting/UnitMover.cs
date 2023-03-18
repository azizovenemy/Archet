using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private UnitPicker _picker;
    [SerializeField] private GameObject _cube;

    private Unit[] _pickedUnits;
    private Vector3 _target;

    private void Start()
    {
        _picker.OnUnitsPicked += UnitPickHandler;
        _cube.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTarget();
        }
    }

    private bool HasUnitsReachDestination()
    {
        foreach(var unit in _pickedUnits)
        {
            if(unit.HasReachedDestination() == false)
            {
                return false;
            }
        }

        return true;
    }

    private void UnitPickHandler(Unit[] units)
    {
        if (IsUnitsListEmpty() == false){
            foreach(var unit in _pickedUnits)
            {
                unit.OnStop();
            }
        }

        _pickedUnits = units;
    }

    private void SetTarget()
    {
        if (IsUnitsListEmpty() == true)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            _target = hit.point;
            _cube.transform.position = new Vector3(hit.point.x, _cube.transform.position.y, hit.point.z);
            _cube.SetActive(true);
            MoveToTarget();
            StopAllCoroutines();
            StartCoroutine(TurnCubeOffOnEndOfWay());
        }
    }

    private IEnumerator TurnCubeOffOnEndOfWay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        while (IsUnitsListEmpty() == false)
        {
            if (HasUnitsReachDestination())
            {
                _cube.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }

    private void MoveToTarget()
    {
        if (IsUnitsListEmpty() == true)
            return;

        foreach(var unit in _pickedUnits)
        {
            unit.MoveTo(_target);
        }
    }

    private bool IsUnitsListEmpty() => _pickedUnits == null || _pickedUnits.Length == 0;
}