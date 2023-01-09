using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestDamage : MonoBehaviour
{
    public int lifeEnemyTest;

    private void Start()
    {
        lifeEnemyTest = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sword"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(7f,0,7f), ForceMode.Impulse);
            print(lifeEnemyTest);
            lifeEnemyTest = lifeEnemyTest - 10;
            if (lifeEnemyTest < 0 )
            {
                Destroy(gameObject);
            }
            
        }

        if (collision.collider.CompareTag("Arrow"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(7f, 0, 7f), ForceMode.Impulse);
            print(lifeEnemyTest);
            lifeEnemyTest = lifeEnemyTest - 10;
            if (lifeEnemyTest < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
