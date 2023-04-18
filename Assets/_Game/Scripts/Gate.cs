using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject closegate;
    [SerializeField] private BrickSpawner currentBrickSpawner;
    [SerializeField] private BrickSpawner previousBrickSpawner;
    [SerializeField] private int currentGround;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character playerScript = other.GetComponent<Character>();
            playerScript.currentGround = currentGround;
            Enemy enemyScript = other.GetComponent<Enemy>();
            if (other.name == "Player")
            {
                Invoke(nameof(CloseGate), 0.5f);
            }

            if (enemyScript != null)
            {
                enemyScript.currentGround = currentGround;
                enemyScript.SwitchState(enemyScript.SeekBrickState);
            }
            foreach (GameObject brick in currentBrickSpawner.bricks)
            {
                if (brick.GetComponent<Brick>().brickColor == other.GetComponent<Character>().characterColor)
                {
                    brick.SetActive(true);
                }
            }
            foreach (GameObject brick in previousBrickSpawner.bricks)
            {
                if (brick.GetComponent<Brick>().brickColor == other.GetComponent<Character>().characterColor)
                {
                    brick.SetActive(false);
                }
            }
        }
    }
    private void CloseGate()
    {
        closegate.SetActive(true);
    }
}
