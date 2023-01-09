using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    public Animator animatorEnemy;
    private bool IsFollow = false;
    // Start is called before the first frame update
    void Start()
    {
        animatorEnemy = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Player.position, transform.position);
        // print(dist);
        if (dist < 4)
        {
            enemy.SetDestination(Player.position);
            if (IsFollow != true)
            {
             //   animatorEnemy.SetBool("IsWalk", true);
                IsFollow = true;

            }
        }
    }
}
