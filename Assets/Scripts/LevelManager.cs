using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    public float autoLoadNextlevelAfter;

    private void Start()
    {
        if(autoLoadNextlevelAfter == 0)
        {

        }
        else
        {
            Invoke("LoadNextLevel", autoLoadNextlevelAfter);
        }
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadNextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex +1);
    }

}
