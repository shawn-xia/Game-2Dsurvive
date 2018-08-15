using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingEnemy : MonoBehaviour {

    public Transform target;

    public Transform myTransform;

    private void Update()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.forward * 5 * Time.deltaTime);
    }

}
