using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 4f;
    [SerializeField] private float m_MaxSteeringForce = 4f;
    [SerializeField] private Sprite[] m_VehicleSprites;
    private Vector2 m_Acceleration = Vector2.zero;
    private Vector2 m_Velocity = Vector2.zero;

    private int m_CurrentNodeFollowing = 0;
    private int m_NextNodeFollowing = 0;

    private bool m_IsDoingHardTurn = false;
    private Vector2 m_HardTurnVector = Vector2.zero;

    private void DisableHardTurn() => m_IsDoingHardTurn = false;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = m_VehicleSprites[Random.Range(0, m_VehicleSprites.Length)];
    }

    private void Start()
    {
        m_Acceleration = new Vector2( 0.5f, 0.5f);

        m_CurrentNodeFollowing = 0;
        m_NextNodeFollowing = 1;
    }

    void FixedUpdate()
    {
        if (m_IsDoingHardTurn)
        {
            m_Acceleration = Vector2.Lerp(m_Acceleration, m_HardTurnVector, m_MaxSpeed);
        }

        else
        {
            FollowPath(NodesPath.Instance.Nodes[m_CurrentNodeFollowing].transform.position,
                NodesPath.Instance.Nodes[m_NextNodeFollowing].transform.position);
        }

        //Seek(GetMousePosition());

        m_Velocity += m_Acceleration;
        m_Velocity = Vector2.ClampMagnitude(m_Velocity, m_MaxSpeed);
        transform.position += (Vector3) m_Velocity;

        transform.up = m_Velocity;

        m_Acceleration = Vector2.zero;
    }

    private void FollowPath(Vector2 pathStart, Vector2 pathEnd)
    {
        // Predict future location
        Vector2 predictedLocation = transform.position + (Vector3) (m_Velocity.normalized * 2f);
        Debug.DrawLine(transform.position, predictedLocation, Color.blue);

        Vector2 closestPointInPath = ScalarProjection(predictedLocation, pathStart, pathEnd);
        Debug.DrawLine(predictedLocation, closestPointInPath, Color.black);

        if (Vector2.Distance(predictedLocation, closestPointInPath) > NodesPath.Instance.Radius)
        {
            Seek(closestPointInPath);
        }
    }

    public void ChangeToNextNodePath()
    {
        m_CurrentNodeFollowing = m_NextNodeFollowing;
        m_NextNodeFollowing = NodesPath.Instance.GetNextNodeIndex(m_CurrentNodeFollowing);
    }

    private Vector2 ScalarProjection(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        Vector2 ap = point - lineStart;
        Vector2 ab = (lineEnd - lineStart).normalized;

        // Scale ab by dot product
        float dot = Vector2.Dot(ap, ab);
        ab *= dot;

        Vector2 closestPointInLine = lineStart + ab;
        return closestPointInLine;
    }

    public void EnableVehicleHardTurn(Vector2 acceleration)
    {
        m_IsDoingHardTurn = true;
        m_HardTurnVector = acceleration;
        Invoke(nameof(DisableHardTurn), 0.3f);
    }

    private void Seek(Vector3 targetPosition)
    {
        Vector2 desiredVelocity = (targetPosition - transform.position).normalized * m_MaxSpeed;
        Vector2 steeringForce = desiredVelocity - m_Velocity;
        steeringForce = Vector2.ClampMagnitude(steeringForce, m_MaxSteeringForce);
        ApplyForce(steeringForce);
    }

    private void ApplyForce(Vector2 amount)
    {
        m_Acceleration += amount;
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }
}
