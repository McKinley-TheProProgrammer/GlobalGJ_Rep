using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerBoneTrigger : MonoBehaviour
{

    [SerializeField] 
    private IntVariable collectablesFound;

    [SerializeField] 
    private BoolVariable goalReached;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            collectablesFound.Value += 1;
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Goal"))
        {
            goalReached.Value = true;
            other.gameObject.SetActive(false);
        }
    }
}
