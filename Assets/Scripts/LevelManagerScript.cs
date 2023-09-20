using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PauseScript pause;
    public Animator fadeScreen;
    public float transitionTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        pause = GetComponent<PauseScript>();
        //fadeScreen = GameObject.Find("FadeScreen").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Changelevel(int levelNumber)
    {
        pause.paused = false;
        //fadeScreen.SetTrigger("ChangeLevel");
        StartCoroutine(NewLevel(levelNumber));
    }

    public IEnumerator NewLevel(int levelNumber)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelNumber);

    }

    public void QuitGame()
    {
        Application.Quit();
    }


}