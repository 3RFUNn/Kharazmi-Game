using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TailBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshPro tailText;
    public float delayTime = 1.0f;  
    public Queue<Vector3> positionHistory;  
    private float timeStep;
    bool isInit;

    public void Init()
    {
        positionHistory = new Queue<Vector3>();
        positionHistory.Enqueue(transform.position);
        timeStep = Time.fixedDeltaTime;
        isInit = true;
    }

    void FixedUpdate()
    {
        if (!isInit) return;
        if (!PlayerManager.Instance.IsMoving) return;
        positionHistory.Enqueue(transform.position);
        if (positionHistory.Count > delayTime / timeStep)
        {
            positionHistory.Dequeue();
        }
    }
    public void SetText(string text)
    {
        tailText.text = text;
    }
}
