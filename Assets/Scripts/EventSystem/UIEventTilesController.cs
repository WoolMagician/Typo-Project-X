using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIEventTilesController : MonoBehaviour
{
    public class UIEventData
    {
        public GameObject eventParent;
        public List<UIEventTile> letterTiles = new List<UIEventTile>();

        public void WiggleTiles(bool flag)
        {
            foreach (UIEventTile letterTile in letterTiles)
            {
                letterTile.SetWiggling(flag);
            }
        }
    }

    private const char _rowSplitCharacter = '|';

    public Transform eventCameraTransform;
    public Vector3 cameraOffset = Vector3.zero;

    [SerializeField] private UIEventTile _eventLetterPrefab;
    [SerializeField] private EventDataSO _clearedEventData;
    [SerializeField] private EventLetterPoolSO _eventLetterPool;

    [Header("Listen to")]
    [SerializeField] private EventSystemEventChannelSO _channelUIEventOccurr = default;
    [SerializeField] private EventSystemEventChannelSO _channelUIEventClear = default;

    void Start()
    {
        eventCameraTransform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) - cameraOffset;
        eventCameraTransform.LookAt(this.transform);

        //Initialize event letter pool with maximum 95 letters
        // this is calculated to be the maximum letters in Tomba events
        _eventLetterPool.SetParent(this.transform);
        _eventLetterPool.Prewarm(95);
    }

    private void OnEnable()
    {
        _channelUIEventOccurr.OnEventRaised += HandleEventOccurr;
        _channelUIEventClear.OnEventRaised += HandleEventClear;
    }

    private void OnDisable()
    {
        _channelUIEventOccurr.OnEventRaised -= HandleEventOccurr;
        _channelUIEventClear.OnEventRaised -= HandleEventClear;
    }

    private void HandleEventOccurr(EventDataSO @event)
    {
        this.ShowEventOccurrTiles(@event);
    }

    private void HandleEventClear(EventDataSO @event)
    {
        this.ShowEventClearTiles(@event);
    }

    // Update is called once per frame
    void Update()
    {
        eventCameraTransform.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) - cameraOffset;
    }

    private UIEventData ShowEventTiles(EventDataSO @event)
    {
        UIEventData uiEventData = this.GetTilesFromEvent(@event);
        uiEventData.eventParent.SetActive(true);
        uiEventData.eventParent.transform.localPosition = new Vector3(-7, 0.25f, 0);
        uiEventData.WiggleTiles(true);
        return uiEventData;
    }

    private void ShowEventOccurrTiles(EventDataSO @event)
    {
        UIEventData uiEventData = this.ShowEventTiles(@event);

        //Scaling animation
        DOTween.Sequence().
                Append(uiEventData.eventParent.transform.DOScale(Vector3.zero, 0f)).
                Append(uiEventData.eventParent.transform.DOScale(Vector3.one, 0.55f).
                                             SetEase(Ease.OutSine)).SetAutoKill(true).
                Play();

        //Position animation
        DOTween.Sequence().
                Append(uiEventData.eventParent.transform.DOMove(this.transform.position, 0.6f).SetAutoKill(true).
                                             SetEase(Ease.OutSine)).
                AppendInterval(4f).
                Append(uiEventData.eventParent.transform.DOMoveX(Vector3.right.x * 20f, 1f).SetEase(Ease.Linear)).
                OnComplete(() => { DestroyEvent(uiEventData); }).SetAutoKill(true).
                Play();
    }

    private void ShowEventClearTiles(EventDataSO @event)
    {
        UIEventData uiEventData = this.ShowEventTiles(@event);

        //Scaling animation
        DOTween.Sequence().
                Append(uiEventData.eventParent.transform.DOScale(Vector3.zero, 0f)).
                Append(uiEventData.eventParent.transform.DOScale(Vector3.one, 0.55f).
                                               SetEase(Ease.OutSine)).SetAutoKill(true).
                Play();

        //Position animation
        DOTween.Sequence().
                Append(uiEventData.eventParent.transform.DOMove(this.transform.position, 0.6f).SetAutoKill(true).
                                             SetEase(Ease.OutSine)).
                Play();

        //Blend event shown with Cleared! tiles
        this.BlendEventToCleared(uiEventData);
    }

    void BlendEventToCleared(UIEventData @event)
    {
        UIEventData uiClearData = this.GetTilesFromEvent(_clearedEventData);

        //Set still letters for clear tiles
        uiClearData.WiggleTiles(false);

        //Cycle first event letters, we will need to animate them
        foreach (UIEventTile letter in @event.letterTiles)
        {
            Vector3 closestLetterPosition = GetClosestLetter(uiClearData.letterTiles, letter.transform.localPosition);
            Sequence moveToClosestLetterSeq = DOTween.Sequence();

            //Wait for event to be shown on screen
            moveToClosestLetterSeq.AppendInterval(2f);

            //Move letters to closest
            moveToClosestLetterSeq.Append(letter.transform.DOLocalMove(closestLetterPosition, 0.7f));
            moveToClosestLetterSeq.Join(letter.transform.DOLocalRotate(new Vector3(180, 0, 0), 0.7f)).OnComplete(() => {
                DestroyEvent(@event);
                uiClearData.eventParent.SetActive(true);

                DOTween.Sequence().AppendInterval(2f).OnComplete(() =>
                {
                    foreach (UIEventTile item in uiClearData.letterTiles)
                    {
                        item.CanFall(true);
                    }
                }).SetAutoKill(true).Play();

                DOTween.Sequence().AppendInterval(4f).OnComplete(() =>
                {
                    DestroyEvent(uiClearData);
                }).SetAutoKill(true).Play();
            }).SetAutoKill(true).Play();
        }
    }

    Vector3 GetClosestLetter(List<UIEventTile> letters, Vector3 fromThis)
    {
        Vector3 tMin = Vector3.zero;
        Vector3 currentPos = fromThis;
        float minDist = Mathf.Infinity;

        foreach (UIEventTile letter in letters)
        {
            float dist = Vector3.Distance(letter.transform.localPosition, currentPos);
            if (dist < minDist)
            {
                tMin = letter.transform.localPosition;
                minDist = dist;
            }
        }
        return tMin;
    }

    UIEventData GetTilesFromEvent(EventDataSO @event)
    {
        Vector3 letterPosition = Vector3.zero;

        UIEventData uiEventData = new UIEventData();
        uiEventData.eventParent = new GameObject($"UIEvent[{@event.ID}]");
        uiEventData.eventParent.SetActive(false);
        uiEventData.eventParent.transform.SetParent(this.transform, false);
        uiEventData.eventParent.transform.localPosition = Vector3.zero;
        uiEventData.eventParent.transform.localRotation = Quaternion.identity;
        uiEventData.eventParent.transform.forward = this.transform.forward;

        //Retrieve event rows
        string[] eventNameRows = @event.Name.GetLocalizedString().Split(_rowSplitCharacter);

        //If event description is empty then skip
        if (eventNameRows.Length == 0) return null;

        //Cycle rows
        foreach (string row in eventNameRows)
        {
            int currentRowIndex = Array.IndexOf(eventNameRows, row);

            //Calculate a centered Y offset to center event on screen
            letterPosition.y = (float)(Math.Floor((float)(eventNameRows.Length + 1 / 2))) - currentRowIndex - (currentRowIndex * 0.33f);

            //Cycle letters
            for (int currentLetterIndex = 0; currentLetterIndex < row.Length; currentLetterIndex++)
            {
                char letter = row[currentLetterIndex];

                //If letter is not a space, draw it on screen
                if (letter.ToString() != " ")
                {
                    //Get letter object from pool
                    UIEventTile eventLetter = Instantiate(_eventLetterPrefab);
                    //EventLetter eventLetter = _eventLetterPool.Request();

                    //Be sure to set the event parent
                    eventLetter.transform.SetParent(uiEventData.eventParent.transform, false);

                    //Calculate letter horizontal offset
                    letterPosition.x = (float)(Math.Floor((float)(row.Length / 2))) - currentLetterIndex;

                    //Update letter settings
                    eventLetter.SetLetter(letter);
                    eventLetter.name = letter.ToString();
                    eventLetter.transform.localPosition = letterPosition;
                    uiEventData.letterTiles.Add(eventLetter);
                }
            }
        }
        return uiEventData;
    }

    private void DestroyEvent(UIEventData eventData)
    {
        foreach (UIEventTile item in eventData.letterTiles)
        {
            item.CanFall(false);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            //_eventLetterPool.Return(item);
            Destroy(item.gameObject);
        }
        Destroy(eventData.eventParent);
        eventData = null;
    }
}