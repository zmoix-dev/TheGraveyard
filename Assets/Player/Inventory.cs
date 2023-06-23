using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int keys = 0;
    public int Keys { get { return keys; }}

    public void AddKey() {
        keys++;
    }
}
