using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InterfacePlayer : MonoBehaviour
{
    public Image lifeImage;

    public bool IsPause;
    public GameObject pausePanel;
    public static InterfacePlayer instance;



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


    // Start is called before the first frame update
    void Start()
    {
        IsPause = true;
    }

  

    public void LessLifeImage()
    {
        lifeImage.fillAmount -= 1f * Time.deltaTime;
        if (lifeImage.fillAmount == 0)
        {
          SceneManager.LoadScene("GameOver");
        }
    }

    public void fireLessLifeImage(){
          lifeImage.fillAmount -= 0.1f * Time.deltaTime;
        if (lifeImage.fillAmount == 0)
        {
          SceneManager.LoadScene("GameOver");
        }
    } 

    public void PauseInterface()
    {
        if (IsPause)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            IsPause = false;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            IsPause = true;
        }
    }





}
