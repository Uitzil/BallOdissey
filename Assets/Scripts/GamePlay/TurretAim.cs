using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public GameObject Ball = null;
    [SerializeField] private Transform turretPivot = null;
    [SerializeField] private float maxDistance = 5f;

    private void Update()
    {
        if (Ball == null) return;

        var direction = Ball.transform.position - transform.parent.position;

        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        turretPivot.rotation = targetRotation;

        var distance = Vector3.Distance(Ball.transform.position, transform.parent.position);

        if (Mathf.Abs(distance)> maxDistance)
        {
            Ball = null;
        }


    }

}
