using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    
    [SerializeField] 
    private CrowdController _crowdController;
    [SerializeField] 
    private Canvas retryCanvas;
    
    
    [SerializeField] 
    private BoolVariable gameStarted;

    [SerializeField] 
    private IntReference numberOfCollectables;

    [SerializeField] 
    private int maxCollectables = 5;
    
    [SerializeField] 
    private BoolVariable goalReached;
    
    public void StartGame()
    {
        _inputManager.Pause = false;
    }


    private void Start()
    {
        StartCoroutine(CheckEndGame());
    }

    IEnumerator CheckEndGame()
    {
        yield return new WaitUntil(() => goalReached.Value || numberOfCollectables.Value >= maxCollectables);
        Debug.Log("You Win!");
        retryCanvas.gameObject.SetActive(true);
        
    }

    public void EndGame()
    {
        _inputManager.Pause = true;
        gameStarted.Value = false;
        numberOfCollectables.Value = 0;
        goalReached.Value = false;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
