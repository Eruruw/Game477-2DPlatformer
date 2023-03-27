using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    Vector3 OriginalPosition;
    Vector3 PatrolBound_Left;
    Vector3 PatrolBound_Right;

    Transform Transform;
    Vector3 EnemyPosition;

    bool isMovingLeft;
    bool isMovingRight;

    bool StartRight;
    bool StartLeft;


    // Start is called before the first frame update
    void Start()
    {
        Transform = gameObject.GetComponent<Transform>();
        EnemyPosition = Transform.position;
        SetPatrolBounds();
        DebugLogs();
        isMovingLeft = true;
    }

    void DebugLogs()
    {
        Debug.Log("Original position (x,y,z):");
        Debug.Log(OriginalPosition.x);
        Debug.Log(OriginalPosition.y);
        Debug.Log(OriginalPosition.z);
        Debug.Log("Left patrol bound (x,y,z):");
        Debug.Log(PatrolBound_Left.x);
        Debug.Log(PatrolBound_Left.y);
        Debug.Log(PatrolBound_Left.z);
        Debug.Log("Right patrol bound (x,y,z):");
        Debug.Log(PatrolBound_Right.x);
        Debug.Log(PatrolBound_Right.y);
        Debug.Log(PatrolBound_Right.z);
    }

    void SetPatrolBounds()
    {
        OriginalPosition = Transform.position;
        PatrolBound_Left = PatrolBound_Right = OriginalPosition;
        PatrolBound_Left.x = OriginalPosition.x - 3;
        PatrolBound_Right.x = OriginalPosition.x + 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingLeft)
        {
            EnemyPosition = Transform.position;
            while (EnemyPosition.x > PatrolBound_Left.x)
            {
                //EnemyPosition.x = Transform.position.x - 0.1;
                Transform.position = new Vector3((float)(Transform.position.x - 0.1), Transform.position.y, Transform.position.z);
            }
            isMovingLeft = !isMovingLeft;
            isMovingRight = true;
        }
        else if (isMovingRight)
        {
            while(Transform.position.x < PatrolBound_Right.x)
            {
                Transform.position = new Vector3((float)(Transform.position.x + 0.1), Transform.position.y, Transform.position.z);
            }
            isMovingRight = !isMovingRight;
            isMovingLeft = true;
        }
    }
}
