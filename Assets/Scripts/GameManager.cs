using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameIsPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPause()
    {
        if(gameIsPlaying)
        {
            PauseFlight();
        }
        else
        {
            ResumeFlight();
        }
    }

    private void PauseFlight()
    {
        //Pause simulation
        Time.timeScale = 0;
        //Flag used by all GO to know if they should update
        gameIsPlaying = false;
    }

    private void ResumeFlight()
    {
        //Unpause simulation
        Time.timeScale = 1;
        //Flag used by all GO to know if they should update
        gameIsPlaying = true;
    }

    public void ResetFlight()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeFlight();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
