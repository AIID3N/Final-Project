using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{
    public GameObject swordObject;
    public GameObject bowObject;

    public bool isChange = true;


    public static CollisionPlayer instance;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //   DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {

            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(20f, 0, 20f), ForceMode.Impulse);
            InterfacePlayer.instance.LessLifeImage();

        }
        if (collision.transform.CompareTag("SwordGrab"))
        {
            swordObject.SetActive(true);
            collision.gameObject.SetActive(false);
        }
        if (collision.transform.CompareTag("BowGrab"))
        {
            bowObject.SetActive(true);
            collision.gameObject.SetActive(false);

        }
    }


    public void ChangeWeapon()
    {
        if (isChange)
        {
            swordObject.SetActive(false);
            bowObject.SetActive(true);
            isChange = false;
        }
        else
        {
            swordObject.SetActive(true);
            bowObject.SetActive(false);
            isChange = true;
        }
    }

}
