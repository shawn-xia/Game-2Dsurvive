using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingEnemy : MonoBehaviour
{

    public Transform target;

    public Transform myTransform;

    public Vector2 playerPosition;

    //public Vector2 Position;

    private void Awake()
    {

        //Position = GameObject.Find("chasingEnemy").transform.position;;
    }


    private void Update()
    {

        //transform.LookAt(target);
        //transform.Translate(Vector3.forward * 4 * Time.deltaTime);å
        playerPosition = GameObject.Find("Player").transform.position;
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), playerPosition, 3 * Time.deltaTime);

    }

}
