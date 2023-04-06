using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public GameObject characterbrickPrefab;
    public Transform brickHolder;
    public float speed = 10.0f;
    public ColorData.ColorType characterColor;
    public LayerMask stairLayer;
    public ColorData colorData;
    public List<GameObject> collectedBrick = new List<GameObject>();
    public int currentGround = 1;
    public bool gameEnd;


    private List<Transform> playerBricks = new List<Transform>();


    private void Start()
    {
        OnInit();
        gameEnd = false;
    }
    public virtual void OnInit()
    {
        // ChangeColor();
    }
    public void ReturnBrick()
    {
        collectedBrick[collectedBrick.Count - 1].SetActive(true);
        collectedBrick.RemoveAt(collectedBrick.Count - 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            BuildStair(other);
        }
        else if (other.CompareTag("Brick"))
        {
            if (this.characterColor == other.GetComponent<Brick>().brickColor)
            {
                AddBrick(other);
            }
        }

    }
    public void BuildStair(Collider stair)
    {
        Stair stairScript = stair.gameObject.GetComponent<Stair>();
        Material stairMaterial = stair.gameObject.GetComponent<MeshRenderer>().material;
        if (playerBricks.Count > 0  && (stairScript.stairColor != characterColor || stairMaterial == stairScript.defaultMaterial))
        {
            stair.gameObject.GetComponent<Stair>().ChangeColor(characterColor);
            stair.gameObject.GetComponent<MeshRenderer>().enabled = true;
            RemoveBrick();
            ReturnBrick();
        }
    }
    public void AddBrick(Collider other)
    {
        collectedBrick.Add(other.gameObject);
        other.gameObject.SetActive(false);
        int index = playerBricks.Count;
        characterbrickPrefab.GetComponent<MeshRenderer>().material = colorData.GetColor(characterColor);
        Transform playerBrick = Instantiate(characterbrickPrefab.transform, brickHolder);
        playerBrick.localPosition = Vector3.back * 0.4f + index * 0.5f * Vector3.up;
        playerBricks.Add(playerBrick);
    }
    public void RemoveBrick()
    {
        int index = playerBricks.Count - 1;
        if (index >= 0)
        {
            Transform playerBrick = playerBricks[index];
            playerBricks.Remove(playerBrick);
            Destroy(playerBrick.gameObject);
        }
    }
    public void ClearBrick()
    {
        foreach(Transform playerBrick in playerBricks)
        {
            Destroy(playerBrick.gameObject);
        }
        playerBricks.Clear();   
        collectedBrick.Clear();
    }
    public void EndGame(Vector3 endLevelPosition)
    {
        ClearBrick();
        gameEnd = true;
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        if (GetComponent<CharacterController>() != null)
        {
            GetComponent<CharacterController>().enabled = false;
        }
    }
    public void ChangeColor(int colorNum)
    {
        ColorData.ColorType color = (ColorData.ColorType)colorNum;
        GetComponent<MeshRenderer>().material = colorData.GetColor(color);
        characterColor = color;
    }
}
