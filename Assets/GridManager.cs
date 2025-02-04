using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //from https://www.youtube.com/watch?v=kkAjpQAM-jE
    [SerializeField] private int _width, _height;
    [SerializeField] private Transform _origin;

    [SerializeField] private GameObject _tilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
      _tilePrefab = Resources.Load<GameObject>("Tile");
      MakeGrid();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeGrid() {
        Tile tileScript = _tilePrefab.GetComponent<Tile>();
        for (float x = _origin.transform.position.x - _width/2; x < 0.5 + (_width + _origin.transform.position.x)/2; x++) {
            for (float y = _origin.transform.position.y - _height/2; y < (_height + _origin.transform.position.y)/2; y++) {
                var spawnedTile = Instantiate(tileScript, new Vector2(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }  
    }
}
