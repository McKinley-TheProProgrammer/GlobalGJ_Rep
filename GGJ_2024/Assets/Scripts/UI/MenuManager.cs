using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _timelineAsset;

    [SerializeField]
    private UnityEvent OnTimelineStarted;

    [SerializeField] 
    private RectTransform remoteTransform;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        RemoteUp();
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => InputManager.Instance.MouseLeftBtnAction.triggered);
        PlayTimeline();
        InputManager.Instance.Pause = true;
    }

    public void RemoteUp()
    {
        remoteTransform.DOAnchorPos(Vector2.zero, .7f).From(Vector2.down * 150);
    }
    public void RemoteBack()
    {
        remoteTransform.DOAnchorPos(Vector2.down * 200, .7f);
    }

    public void PlayTimeline()
    {
        _timelineAsset.Play();
        OnTimelineStarted?.Invoke();
    }
}
