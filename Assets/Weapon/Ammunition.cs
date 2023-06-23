using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    [SerializeField] AmmunitionSlot[] slots;

    [System.Serializable]
    private class AmmunitionSlot {
        public AmmunitionType type;
        public int amount;
    }

    public int GetAmount(AmmunitionType type) {
        return slots[(int)type].amount;
    }

    public void Consume(AmmunitionType type) {
        slots[(int)type].amount--;
    }

    public void Increase(AmmunitionType type, int amount) {
        slots[(int)type].amount += amount;
        Debug.Log($"Added {amount} to {type.ToString()}");
    }
}
