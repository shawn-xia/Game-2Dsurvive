using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingEnemy : MonoBehaviour
{

    public Transform target;

    public Transform myTransform;

    public Vector2 playerPosition;
    public Vector2 enemyPosition;

    [SerializeField]
    float speed;

    Rigidbody2D enemyRigid;

    //public Vector2 Position;

    private void Awake()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        //Position = GameObject.Find("chasingEnemy").transform.position;;
    }


    private void Update()
    {
        //enemyRigid = GetComponent<Rigidbody2D>();
        //transform.LookAt(target);
        //transform.Translate(Vector3.forward * 4 * Time.deltaTime);
        //enemyPosition = GameObject.Find("chasingEnemy").transform.position;
        playerPosition = GameObject.Find("Player").transform.position;

        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), playerPosition, speed * Time.deltaTime);

    }

    //IEnumerator Wait(){
    //    yield return new WaitForSeconds(1);
    //}

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Vector3 moveDirection = enemyRigid.transform.position - collision.transform.position;
            enemyRigid.AddForce(moveDirection.normalized * 500f);
            yield return new WaitForSeconds(1);
            enemyRigid.AddForce(-moveDirection.normalized * 500f);


        }
    }

}
