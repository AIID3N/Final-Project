using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{




 private void OnCollisionEnter(Collision other) {
    if(other.collider.CompareTag("Player")){
      
       ChangeScene(SceneManager.GetActiveScene().name);
    }
 }

 public void ChangeScene(string sceneCurrent){


    switch (sceneCurrent){

        case "Level1":
        SceneManager.LoadScene("Level2");
        break;
        case "Level2":
        SceneManager.LoadScene("Level3");
        break;
              
    }
 }



}
