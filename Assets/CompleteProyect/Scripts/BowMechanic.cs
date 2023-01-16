using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMechanic : MonoBehaviour
{
    public GameObject cross;
    public GameObject arrow;
    public bool isInstantiateArrow = true;
    public static BowMechanic instance;



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

        public void ArrowShoot()
    {
        if (isInstantiateArrow)
        {
            print("Esta disprando muchas flechas " + isInstantiateArrow);
            isInstantiateArrow = false;
            print("Esta disprando muchas flechas " + isInstantiateArrow);

            GameObject newArrow = Instantiate(arrow, cross.transform.position, arrow.transform.rotation);
            newArrow.transform.rotation = cross.transform.rotation;
              // newArrow.GetComponent<Rigidbody>().AddForce(Vector3.forward *-1, ForceMode.Impulse);
           newArrow.transform.position = cross.transform.position;
           newArrow.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 4000f);
            Destroy(newArrow, 4f);
        }
        else
        {

            Invoke("CanInstantiateArrow", 1.2f);
        }

    }

    public void CanInstantiateArrow()
    {
        isInstantiateArrow = true;
    }


}
