using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;
    
    [SerializeField] 
    private CrowdController _crowdController;

    [SerializeField] 
    private BoolVariable gameStarted;

    
    public void StartGame()
    {
        _inputManager.Pause = false;

    }
    
    
    
}
