using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerLevel Level;
    public PlayerGold Gold;
    public PlayerStatus Status;
    public PlayerController Controller;

    void Awake()
    {
        Level = GetComponent<PlayerLevel>();
        Gold = GetComponent<PlayerGold>();
        Status = GetComponent<PlayerStatus>();
        Controller = GetComponent<PlayerController>();
    }
}
