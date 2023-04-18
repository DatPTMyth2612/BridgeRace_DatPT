using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickSpawner : MonoBehaviour   
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private int spaceBetweenBrick;
    [SerializeField] private bool enableOnStart;
    public List<GameObject> bricks = new List<GameObject>();

    private void Start() 
    {
        for (int i = 0; i < column; i += spaceBetweenBrick)
        {
            for (int j = 0; j < row; j += spaceBetweenBrick)
            {
                GameObject brick = Instantiate(brickPrefab, new Vector3(transform.position.x + i - 18, transform.position.y + 0.75f, transform.position.z + j - 18), Quaternion.identity, transform);
                if (!enableOnStart)
                {
                    brick.SetActive(false);
                }
                bricks.Add(brick);
            }
        }
    }
}
