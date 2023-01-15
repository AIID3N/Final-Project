using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderGiantEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float launchForce = 10f;
    public float launchAngle = 45f;
    public Transform player;
    public Transform cross;
    public Image lifeEnemy;
    public Animator animatorEnemy;
   public GameObject doorObject;
    public bool IsAtackPlayer= true;
    public bool IsDead = false;



    private void Start()
    {
        StartCoroutine("AtackPlayer");
    }

    private void Update() {
float dist = Vector3.Distance(player.position, transform.position);
        print(dist);
      
/*
        if(dist <33){
        launchForce = 15;
        launchAngle = 20f;
        }

        if(dist < 20){
            launchForce = 10;
        }
        if(dist < 15){
            launchForce = 8;
        }
        if(dist< 10){
          launchForce = 5;
        launchAngle = 10f;  
        }

        */
    }


    public void LaunchProjectile(Vector3 targetPosition)
    {
        // Calculate the direction of the launch
        Vector3 launchDirection = targetPosition - cross.transform.position;
        launchDirection.y = 0f;
        launchDirection.Normalize();

        // Calculate the velocity needed to hit the target position
        float distanceToTarget = Vector3.Distance(cross.transform.position, targetPosition);
        float yVelocity = distanceToTarget * Mathf.Tan(launchAngle * Mathf.Deg2Rad) / (launchForce * 0.5f);
        float xzVelocity = Mathf.Sqrt(launchForce * launchForce - yVelocity * yVelocity);
        Vector3 launchVelocity = launchDirection * xzVelocity + Vector3.up * yVelocity;
        print(Mathf.Sqrt(launchForce * launchForce - yVelocity * yVelocity));
        // Instantiate and launch the projectile
        GameObject projectile = Instantiate(projectilePrefab, cross.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = launchVelocity;
        Destroy(projectile, 3f);
    }

   

    public IEnumerator AtackPlayer()
    {
        while (IsAtackPlayer && !IsDead)
        {
            animatorEnemy.SetTrigger("attack1");
            yield return new WaitForSeconds(1f);
            LaunchProjectile(player.position);
            yield return new WaitForSeconds(1f);
            animatorEnemy.SetTrigger("idle");
        }

    }


   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Sword"))
        {
           // gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(7f, 0, 7f), ForceMode.Impulse);        
           
            lifeEnemy.fillAmount -= 10f * Time.deltaTime;
            if (lifeEnemy.fillAmount <= 0)
            {
               IsDead = true;
                animatorEnemy.SetTrigger("death");
                IsAtackPlayer = false;
                StopCoroutine("AtackPlayer");
              //  Destroy(gameObject,3f);
                doorObject.GetComponent<BoxCollider>().enabled = true;

            }

        }

        if (collision.collider.CompareTag("Arrow"))
        {
           // gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(7f, 0, 7f), ForceMode.Impulse);
           // print(lifeEnemy);
            lifeEnemy.fillAmount -= 10f * Time.deltaTime;

            if (lifeEnemy.fillAmount <= 0)
            {

                 IsDead = true;
                StopCoroutine("AtackPlayer");

                animatorEnemy.SetTrigger("death");
                doorObject.GetComponent<BoxCollider>().enabled = true;

             //   Destroy(gameObject,3f);
            }
        }
    }


}
