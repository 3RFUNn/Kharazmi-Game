using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    [SerializeField] TailBehaviour currentSegment;
    [SerializeField] TailBehaviour SegmentPrefab;
    public float MoveSpeed = 5f;
    public int InitialSize = 4;
    Vector2 direction;
    Vector2 prevDirection;
    List<TailBehaviour> segments;
    bool canMove;
    [HideInInspector]
    public bool IsMoving;
    public string KeyString;

    public void Init(string key)
    {
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
        for (int i = segments.Count - 1; i > 0; i--)
        {
            if (segments[i-1].positionHistory.Count>0)
                segments[i].transform.position = segments[i - 1].positionHistory.Peek();
        }
        transform.position = new Vector3(
            transform.position.x + direction.x * MoveSpeed,
            transform.position.y + direction.y * MoveSpeed,
            0.0f
        );
    }

    public void Grow(string text="")
    {
        var segment = Instantiate(SegmentPrefab);
        segment.Init();
        if (segment.positionHistory.Count > 0)
            segment.transform.position = segments[segments.Count - 1].positionHistory.Peek();
        else
            segment.transform.position = segments[segments.Count - 1].transform.position;
        segments.Add(segment);
        segment.SetText(text);
    }
    void SetString(string s)
    {
        currentSegment.SetText(s);
        KeyString = s;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            var collectible = other.gameObject.GetComponent<CollectibleManager>();
            if (!collectible.KeyString.Equals(KeyString))
            {
                Debug.Log("GAME OVER");
                return;
            }
            Grow(collectible.GetText());
            Destroy(other.gameObject);
        }
    }
}
