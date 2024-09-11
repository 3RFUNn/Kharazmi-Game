using DG.Tweening;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    [SerializeField] TailBehaviour currentSegment;
    [SerializeField] TailBehaviour SegmentPrefab;
    public float MoveSpeed = 5f;
    public int InitialSize = 4;
    Vector2 direction;
    Vector2 prevDirection;
    public List<TailBehaviour> segments;
    bool canMove;
    [HideInInspector]
    public bool IsMoving;
    public string KeyString;
    int score;
    public void Init(string key)
    {
        score = 0;
        SetString(key);
        canMove = true;
        segments = new List<TailBehaviour>
        {
            currentSegment
        };
        currentSegment.Init();
        for (int i = 1; i < InitialSize; i++)
        {
            Grow();
        }
        PlayerEdgeMovement.Instance.Init();
    }

    void Update()
    {
        if (!canMove) return;
        if (Input.GetKey(KeyCode.W) && prevDirection != Vector2.down)
        {
            direction = Vector2.up;
            prevDirection=direction;
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.S) && prevDirection != Vector2.up)
        {
            direction = Vector2.down;
            prevDirection = direction;
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.A) && prevDirection != Vector2.right)
        {
            direction = Vector2.left;
            prevDirection = direction;
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.D) && prevDirection != Vector2.left)
        {
            direction = Vector2.right;
            prevDirection = direction;
            IsMoving = true;
        }
        else
        {
            direction = Vector2.zero;
            IsMoving = false;
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        if (!IsMoving) return;
        for (int i = segments.Count - 1; i > 0; i--)
        {
            if (segments[i - 1].positionHistory.Count > 0)
            {
                var targetPos= segments[i - 1].positionHistory.Peek();
                Vector3 dir = targetPos - segments[i].transform.position;
                segments[i].transform.position = targetPos;
                LookToDirection(segments[i].transform, dir);
            }
        }
        LookToDirection(transform, direction);
        transform.position = new Vector3(
            transform.position.x + direction.x * MoveSpeed * Time.fixedDeltaTime,
            transform.position.y + direction.y * MoveSpeed * Time.fixedDeltaTime,
            0.0f
        );
    }
    void LookToDirection(Transform obj,Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
        obj.transform.DOKill();
        obj.transform.DORotateQuaternion(rotation, 0.25f);
    }
    public void Grow(string _preText="",string _keyText="")
    {
        var segment = Instantiate(SegmentPrefab);
        segment.Init();
        if (segment.positionHistory.Count > 0)
        {
            segment.transform.position = segments[segments.Count - 1].positionHistory.Peek();
        }
        else
        {
            segment.transform.position = segments[segments.Count - 1].transform.position;
        }
        segments.Add(segment);
        segment.SetText(_preText,_keyText);
    }
    void SetString(string s)
    {
        currentSegment.SetText("",s);
        KeyString = s;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            var collectible = other.gameObject.GetComponent<CollectibleManager>();
            if (!collectible.KeyString.Equals(KeyString))
            {
                GameOver();
                return;
            }
            Grow(collectible.GetPreText(), collectible.GetKeyText());
            score++;
            SpawnCollectibles.Instance.CollectibleEaten(collectible);
            Destroy(other.gameObject);
        }/*
        if (other.CompareTag("Tail"))
        {
            GameOver();
            return;
        }*/
    }
    public void GameOver()
    {
        Debug.LogError("GAME OVER");
        Debug.Log("Score is : " + score);
    }
}
