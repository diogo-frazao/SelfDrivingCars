using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private Vehicle m_VehiclePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0))
        {
            Instantiate(m_VehiclePrefab, GetMousePosition(), Quaternion.identity);
        }
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }
}
