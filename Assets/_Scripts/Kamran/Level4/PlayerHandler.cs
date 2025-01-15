using DG.Tweening;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 initScale; //make sure the player starts right-wards
    Tween moveTween;
    Tween scaleTween;
    private void Start()
    {
        initScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // For mouse click
        {
            Vector3 worldPosition = GetWorldPosition(Input.mousePosition);
            MoveToPoint(worldPosition);
        }
        else
        {
            if (Input.touchCount > 0) // For touch
            {
                Touch touch = Input.GetTouch(0);
                Vector3 worldPosition = GetWorldPosition(touch.position);
                MoveToPoint(worldPosition);
            }
        }
    }
    void MoveToPoint(Vector3 point)
    {
        moveTween?.Kill();
        if(point.x < transform.position.x)
        {
            transform.localScale=new Vector3(-initScale.x, initScale.y, initScale.z);
        }
        else
        {
            transform.localScale = new Vector3(initScale.x, initScale.y, initScale.z);
        }
        moveTween=transform.DOMoveX(point.x, speed).SetSpeedBased(true);
    }
    Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        Camera mainCamera = Camera.main;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, mainCamera.nearClipPlane)
        );
        return worldPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rain"))
        {
            var rc = collision.gameObject.GetComponent<RainingCollectible>();
            RainingCollectibleManager.Instance.ClickedRain(rc);
            scaleTween?.Kill(true);
            scaleTween = transform.DOPunchScale(transform.localScale * 0.1f, 0.1f);
        }
    }
}
