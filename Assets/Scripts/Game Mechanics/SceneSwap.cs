using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{

    public LevelWin LevelWinInstance;
    public PlayerMovement ResetPosition;

    //GameObject Flag = GameObject.Find ("Flag");

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        LevelWinInstance = FindObjectOfType<LevelWin>();
    }

    public bool Running = false;
    public void NextLevel()
    {
        if(!Running){
            //LevelWin.WarpToLevel levelWin = Flag.GetComponent<LevelWin>();
            Debug.Log(LevelWinInstance.WarpToLevel);
            StartCoroutine("WinTimer");
        }
    }

    IEnumerator WinTimer()
    {
        Running = true; 
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level " + LevelWinInstance.WarpToLevel);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Level " + LevelWinInstance.WarpToLevel);
        ResetPosition.OnSceneLoaded();
        LevelWinInstance = FindObjectOfType<LevelWin>();
        Running = false;
        StopCoroutine("WinTimer");
    }
}
