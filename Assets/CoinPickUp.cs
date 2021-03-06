using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(10);    
     }
    
}
