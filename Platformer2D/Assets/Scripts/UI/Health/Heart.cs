using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
