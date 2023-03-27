using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask layer;

    private Ray ray;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireRay();
        }
    }

    private void FireRay()
    {
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layer))
        {
            OnShoot(hit);
        }
    }

    private void OnShoot(RaycastHit hitInfo)
    {
        IEnemy enemy = hitInfo.collider.gameObject.transform.GetComponentInParent<NPC>();
        enemy.HitEnemy(damage);
    }
}
