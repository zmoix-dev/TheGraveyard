using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmunitionType type;
    [SerializeField] int amount;
    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            FindObjectOfType<Ammunition>().Increase(type, amount);
            Destroy(gameObject);
        }
    }
}
