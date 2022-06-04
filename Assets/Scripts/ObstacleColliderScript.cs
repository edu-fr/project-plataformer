using System;
using System.Collections;
using System.Collections.Generic;
using ProjectPlataformer;
using TMPro;
using UnityEngine;

public class ObstacleColliderScript : MonoBehaviour
{
    private player PlayerRef;
    [SerializeField] private TextMeshProUGUI scoreValueRef;

    private void Awake()
    {
        PlayerRef = GetComponentInParent<player>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            PlayerRef.speed = 0;
            print("Game over!");
            return;
        }

        if (col.gameObject.CompareTag("Item"))
        {
            if (col.gameObject.GetComponent<ItemScript>().itemType == ItemScript.ItemType.Donut)
            {
                IncreaseScore(50);
            }
            else
            {
                print("ON FIRE!!");
            }
        }
    }
    
    public void IncreaseScore(int value)
    { 
        scoreValueRef.SetText((int.Parse(scoreValueRef.text) + value).ToString());
    }
}
