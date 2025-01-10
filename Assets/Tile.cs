using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    private bool tileInPlayerRange = false;
    private bool tileInBotRange = false;
    private bool hasLineOfSight = false;
    public bool canSeePlayer = false;
    public bool canSeeBot = false;
    private Color color;
    private GameObject playerBody;
    private GameObject botBody;
    //help from https://www.youtube.com/watch?v=kkAjpQAM-jE
    // and https://www.youtube.com/watch?v=xDg2pxqJHq4&list=LL&index=1 
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player");
        botBody = GameObject.FindGameObjectWithTag("Bot");
    }

    // Update is called once per frame
    void Update()
    {
        if (_renderer.color != color && !canSeePlayer && !canSeeBot) {
            _renderer.color = color;
        }
    }

    public void Init(bool isOffset) {
       if (isOffset) {
        _renderer.color = _offsetColor;
       } else {
        _renderer.color = _baseColor;
       }
       color = _renderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDetectionRange"))
        {
            tileInPlayerRange = true;
        } else if (other.CompareTag("BotDetectionRange")) {
            tileInBotRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDetectionRange"))
        {
           tileInPlayerRange = false;
           canSeePlayer = false;
        } else if (other.CompareTag("BotDetectionRange")) {
            tileInBotRange = false;
            canSeeBot = false;
        }
    }

    private void FixedUpdate()
    {
        if (tileInPlayerRange) {
            RayToEntityDetection("Player", playerBody, ref canSeePlayer);
        }
        if (tileInBotRange) {
            RayToEntityDetection("Bot", botBody, ref canSeeBot);
        }
    }

    private void RayToEntityDetection(string tag, GameObject entity, ref bool canSeeEntity) {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, entity.transform.position - transform.position);
            if (ray.collider != null)
            {

                Debug.Log(ray.collider);
                hasLineOfSight = ray.collider.CompareTag(tag);
                if(hasLineOfSight)
                {
                    _renderer.color = Color.grey;
                    canSeeEntity = true;
                    Debug.DrawRay(transform.position, entity.transform.position - transform.position, Color.green);
                } 
                else
                {
                    canSeeEntity = false;
                    Debug.DrawRay(transform.position, entity.transform.position - transform.position, Color.red);
                }
            }
    }
}
