using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultProjectileTest : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float launchForce = 10f;
    public float launchAngle = 45f;
    public Transform playerPosition;
    public void LaunchProjectile(Vector3 targetPosition)
    {
        // Calculate the direction of the launch
        Vector3 launchDirection = targetPosition - transform.position;
        launchDirection.y = 0f;
        launchDirection.Normalize();

        // Calculate the velocity needed to hit the target position
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        float yVelocity = distanceToTarget * Mathf.Tan(launchAngle * Mathf.Deg2Rad) / (launchForce * 0.5f);
        float xzVelocity = Mathf.Sqrt(launchForce * launchForce - yVelocity * yVelocity);
        Vector3 launchVelocity = launchDirection * xzVelocity + Vector3.up * yVelocity;

        // Instantiate and launch the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = launchVelocity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LaunchProjectile(playerPosition.position);
        }
    }
}
