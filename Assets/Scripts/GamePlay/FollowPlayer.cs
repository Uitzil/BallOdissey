using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Target;

    public float speed = 5f;
    public float minDistance = 0.2f;
    public float smoothFactor = 0.5f;

    private Vector3 originalPosition;



    private void OnEnable()
    {
        transform.position= Vector3.Slerp(transform.position,Target.position,smoothFactor);
        //Vector3  Target  = TargetObj.position;

    }

    public void Update()
    {
        StartCoroutine(MoveToPlayer());



    }

    private IEnumerator MoveToPlayer()
    {
        var distance = Vector3.Distance(transform.position, Target.position);

        while (Mathf.Abs(distance)> minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * speed/1000);
            //distance = Vector3.Distance = Vector3.Distance(transform.position, Target);
            yield return null;


        }



    }
}
