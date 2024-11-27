using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable() // Use OnEnable to be robust against subscriptions
    {
        BulletPool.AlertBot += AlertBot;
    }

    void OnDisable() // Always unsubscribe when the object is destroyed or disabled
    {
        BulletPool.AlertBot -= AlertBot;
    }

    private void AlertBot(GameObject newTarget)
    {
        if (target == null) {
            target = newTarget;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            Debug.Log(target.transform.position.x + " " +  target.transform.position.y);
            if (!target.activeInHierarchy) {
                target = null;
                Debug.Log("target gone");
            }
        }
    }
}
