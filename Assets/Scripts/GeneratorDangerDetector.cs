using UnityEngine;
using System;

public class GeneratorDangerDetector : MonoBehaviour
{
    [SerializeField] private Vector3[] gridPositions = new Vector3[4];
    [SerializeField] private float dangerDetectionRange;
    [SerializeField] private int dangerDetectionOrigin;
    [SerializeField] LayerMask dangerDetectionMask;
    bool generatorInDanger = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int positionCount = GridManager.current.GetRows();
        for (int i = 0; i < positionCount; i++)
        {
            gridPositions[i] = GridManager.current.GridPositionToWorldPosition(new Vector2(dangerDetectionOrigin, i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool dangerDetected = false;
        for (int i = 0; i < gridPositions.Length; i++)
        {
            Vector3 rayOriginPosition = gridPositions[i];
            print(rayOriginPosition + ":" + i);
            Debug.DrawLine(rayOriginPosition, rayOriginPosition + Vector3.right * dangerDetectionRange, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(rayOriginPosition, Vector3.right, out hit, dangerDetectionRange, dangerDetectionMask))
            {
                dangerDetected = true;
            }
        }
        if (!generatorInDanger && dangerDetected)
        {
            generatorInDanger = true;
            EventHandler.current.GeneratorDanger(true);
        }
        if (generatorInDanger && !dangerDetected)
        {
            //generatorInDanger = false;
            //EventHandler.current.GeneratorDanger(false);
        }

    }
}
