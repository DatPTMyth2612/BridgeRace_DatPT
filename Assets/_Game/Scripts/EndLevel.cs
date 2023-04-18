using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject Lose;
    public UnityAction<Vector3> OnEndLevelAction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            other.GetComponent<Character>().isWin = true;
            OnEndLevelAction?.Invoke(transform.position);
            other.transform.position = transform.position;
            other.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (other.name == "Player")
            {
                Win.SetActive(true);
            }
            else
            {
                Lose.SetActive(true);
            }
        }
    }
}
