using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BotTargeting : MonoBehaviour
{
    private GameObject target;
    private Queue<GameObject> targetQueue = new Queue<GameObject>();

    public static event Action<GameObject> GiveBotNewTarget;

    void OnEnable()
    {
        BulletPool.AlertBot += AlertBot;
    }

    void OnDisable()
    {
        BulletPool.AlertBot -= AlertBot;
    }

    private void AlertBot(GameObject newTarget)
    {
        targetQueue.Enqueue(newTarget);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && targetQueue.Count > 0) {
            target = targetQueue.Peek();
        }
        if (target != null) {
            if (!target.activeInHierarchy) {
                targetQueue.Dequeue();
                if (targetQueue.Count > 0) {
                    target = targetQueue.Peek();
                } else {
                    target = null;
                }
            }
        }
        GiveBotNewTarget?.Invoke(target);
    }
}
