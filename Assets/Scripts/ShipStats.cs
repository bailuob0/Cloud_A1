using System;
using UnityEngine;

[CreateAssetMenu]
public class ShipStats :ScriptableObject
{
    public float MoveSpeed = 10f;
    public float FireRate =0.25f;
    public float BulletSpeed = 20f;
    public bool hasCat = false;

    public void Reset()
    {
        MoveSpeed = 10f;
        FireRate =0.25f;
        BulletSpeed = 20f;
        hasCat = false;
    }
}