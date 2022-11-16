using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private Vector2 newVehicleVelocityOnReach = new Vector2(0f, 0f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vehicle otherVehicleComp = collision.GetComponent<Vehicle>();
        if (otherVehicleComp != null)
        {
            otherVehicleComp.EnableVehicleHardTurn(newVehicleVelocityOnReach);
            otherVehicleComp.ChangeToNextNodePath();
        }
    }
}
