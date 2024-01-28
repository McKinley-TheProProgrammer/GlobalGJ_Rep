using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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

    private Vector2 playerStartPos;
    [SerializeField] private Transform mainPlayer;

    [SerializeField]
    private List<CollectableGeneric> _collectableGenerics;
    
    private void Start()
    {
        playerStartPos = mainPlayer.position;
        StartCoroutine(CheckEndGame());
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator CheckEndGame()
    {
        yield return new WaitUntil(() => goalReached.Value || numberOfCollectables.Value >= maxCollectables);
        Debug.Log("You Win!");
        retryCanvas.gameObject.SetActive(true);
        InputManager.Instance.Pause = true;
        
    }
    
    public void StartGame()
    {
        InputManager.Instance.Pause = false;
    }
    
    public void EndGame()
    {
        retryCanvas.gameObject.SetActive(false);
        gameStarted.Value = false;
        numberOfCollectables.Value = 0;
        goalReached.Value = false;

        InputManager.Instance.Pause = false;
        
        mainPlayer.position = playerStartPos;
        _collectableGenerics.ForEach(x => x.gameObject.SetActive(true));
        StartCoroutine(CheckEndGame());
    }
    
}
