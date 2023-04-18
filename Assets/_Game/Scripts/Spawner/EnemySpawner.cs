using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private ColorData colorData;
    [SerializeField] private float distanceBetweenCharacter;
    int i = 0;
    private List<int> characterIndexes = new List<int>();
    private void Start()
    {
        characterIndexes.Add(player.GetComponent<Player>().randomColorInt);
        while (characterIndexes.Count < 4 && i < 3)
        {
            int enemyIndex = Random.Range(0, 4);
            if (characterIndexes.Contains(enemyIndex)) continue;
            GameObject enemy = Instantiate(enemyPrefab, transform.position + new Vector3(i * distanceBetweenCharacter, 1f, 0f), Quaternion.identity);
            enemy.GetComponent<Enemy>().ChangeColor(enemyIndex);
            characterIndexes.Add(enemyIndex);
            i++;
        }
    }
}
