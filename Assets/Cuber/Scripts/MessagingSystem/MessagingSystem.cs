using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void MessageHandlerDelegate(Message message);

public class MessagingSystem : MonoBehaviour
{
    #region Fields
    public Dictionary<string, List<MessageHandlerDelegate>> _listenerDict = new Dictionary<string, List<MessageHandlerDelegate>>();
    private Queue<Message> _messageQueue = new Queue<Message>();
    private const int _maxQueueProcessingTime = 16667;
    private System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

    [Header("Debug")]
    [SerializeField] private int _delegateID;
    [SerializeField] private string _delegateName;
    [SerializeField] private bool _performedCheck;
    #endregion

    #region Singleton
    public static MessagingSystem Instance
    {
        get; private set;
    }
    #endregion

    #region Controls
    private void Awake()
    {
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public bool AttachListener(System.Type type, MessageHandlerDelegate handler)
    {
        if (type == null)
        {
            Debug.Log("MessagingSystem: AttachListener failed due to having no " +
                        "message type specified");
            return false;
        }

        string msgType = type.Name;
        if (!_listenerDict.ContainsKey(msgType))
            _listenerDict.Add(msgType, new List<MessageHandlerDelegate>());

        List<MessageHandlerDelegate> listenerList = _listenerDict[msgType];
        if (listenerList.Contains(handler))
            return false; // listener already in list

        listenerList.Add(handler);
        return true;
    }

    public bool DetachListener(System.Type type, MessageHandlerDelegate handler)
    {
        if (type == null)
        {
            Debug.Log("MessagingSystem: DetachListener failed due to having no " +
                        "message type specified");
            return false;
        }

        string msgType = type.Name;

        if (!_listenerDict.ContainsKey(type.Name))
            return false;

        List<MessageHandlerDelegate> listenerList = _listenerDict[msgType];
        if (!listenerList.Contains(handler))
            return false;

        listenerList.Remove(handler);
        return true;
    }

    public bool QueueMessage(Message msg)
    {
        if (!_listenerDict.ContainsKey(msg.GetType().ToString()))
            return false;

        _messageQueue.Enqueue(msg);
        return true;
    }

    public bool TriggerMessage(Message msg)
    {
        string msgType = msg.GetType().ToString();
        if (!_listenerDict.ContainsKey(msgType))
        {
            Debug.Log($"MessagingSystem: Message \"{msgType}\" has no listeners!");
            return false; // no listeners for message so ignore it
        }

        List<MessageHandlerDelegate> listenerList = _listenerDict[msgType];

        for (int i = 0; i < listenerList.Count; ++i)
        {
            try
            {
                listenerList[i](msg);
            }
            catch
            {
                Debug.LogError($"MessagingSystem Failed to send delegete #{i} at {msgType} to {_listenerDict[msgType][i].Target}", this);
                _listenerDict[msgType].RemoveAt(i);
                i--;
            }
        }
        return true;
    }

    private void Update()
    {
        timer.Start();
        while (_messageQueue.Count > 0)
        {
            if (_maxQueueProcessingTime > 0.0f)
            {
                if (timer.Elapsed.Milliseconds > _maxQueueProcessingTime)
                {
                    timer.Stop();
                    return;
                }
            }

            Message msg = _messageQueue.Dequeue();
            if (!TriggerMessage(msg))
                Debug.Log($"Error when processing message: {msg.GetType().ToString()}");
        }

        if(_performedCheck)
        {
            _performedCheck = false;

            List<MessageHandlerDelegate> listenerList = _listenerDict[_delegateName];

            Debug.Log($"MesSys - {_delegateName} - delegate[{_delegateID}].Target = {listenerList[_delegateID].Target}", this); 
        }
    }
    #endregion
}