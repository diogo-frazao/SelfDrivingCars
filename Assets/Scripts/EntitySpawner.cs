using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private Vehicle m_VehiclePrefab;

    private List<Vehicle> m_StoredVehicles = new List<Vehicle>();

    [SerializeField]
    private float m_defaultSpeed = 0.1f;
    [SerializeField]
    private float m_defaultSteerForce = 0.002f;

    public float DefaultSpeed { get =>m_defaultSpeed; }
    public float DefaultSteer { get =>m_defaultSteerForce; }

    public float MaxSpeed
    {
        get => m_MaxSpeed;
        set
        {
            m_MaxSpeed= value;
            UpdateVehicles();
        }
    }

    public float MaxSteer
    {
        get => m_MaxSteeringForce;
        set
        {
            m_MaxSteeringForce = value;
            UpdateVehicles();
        }
    }

    float m_MaxSteeringForce;
    float m_MaxSpeed;

    private void Start()
    {
        SetDefaultValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(1))
        {
            Vehicle v = Instantiate(m_VehiclePrefab, GetMousePosition(), Quaternion.identity);
            m_StoredVehicles.Add(v);
            v.MaxSpeed = m_MaxSpeed;
            v.MaxSteeringForce = m_MaxSteeringForce;
        }
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }

    void UpdateVehicles()
    {
        foreach ( Vehicle v in m_StoredVehicles)
        {
            v.MaxSpeed = m_MaxSpeed;
            v.MaxSteeringForce = m_MaxSteeringForce;
        }
    }

    public void SetDefaultValues()
    {
        m_MaxSpeed = m_defaultSpeed;
        m_MaxSteeringForce = m_defaultSteerForce;
        UpdateVehicles();
    }

    public void DestroyAllCars()
    {
        foreach (Vehicle v in m_StoredVehicles)
        {
            Destroy(v.gameObject);
        }
        m_StoredVehicles.Clear();
    }
}
