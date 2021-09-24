using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBarBehaviour : MonoBehaviour
{
    public Vector3 offset;
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(transform.parent.parent.position + offset);
    }
}
