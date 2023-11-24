using System;

[Serializable]
public struct ShipStats
{
    public static ShipStats Default = new()
    {
        MoveSpeed = 10,
        FireRate = 0.25f,
        BulletSpeed = 20,
        hasCat = false
    };

    public float MoveSpeed;
    public float FireRate;
    public float BulletSpeed;
    public bool hasCat;
}