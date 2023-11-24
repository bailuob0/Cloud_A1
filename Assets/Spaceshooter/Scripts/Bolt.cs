using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour {

    Rigidbody rbBolt;

    [SerializeField]
    ShipStats shipStats;

    [HideInInspector]
    public float speed;

    private void Start() {
        speed = shipStats.BulletSpeed;
        rbBolt = GetComponent<Rigidbody>();
        rbBolt.velocity = transform.forward * speed;
    }
}
