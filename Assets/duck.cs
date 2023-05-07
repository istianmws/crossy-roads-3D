using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Duck : MonoBehaviour
{
    // [SerializeField] bool isMoving;
    [SerializeField, Range(0,1)] float durasi;
    [SerializeField, Range(0,6)] float jumpHeight;
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;
    private Vector2 touchStartPosition;
    public UnityEvent<Vector3> OnJumpEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DOTween.IsTweening(transform))
        {
            return;
        }

        Vector3 direction = Vector3.zero;
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction -= Vector3.forward;
        }
        else if(Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction -= Vector3.back;
        }
        else if(Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
        { 
            direction -= Vector3.right;
        }
        else if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction -= Vector3.left;
        }

        foreach (Touch touch in Input.touches)
        {
            
        if (touch.phase == TouchPhase.Began)
        {
            touchStartPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Vector2 touchDelta = touch.position - touchStartPosition;
            float deltaX = Mathf.Abs(touchDelta.x);
            float deltaY = Mathf.Abs(touchDelta.y);

            if (deltaX > deltaY)
            {
                if (touchDelta.x > 0)
                {
                    // Move right
                    direction -= Vector3.right;
                }
                else
                {
                    // Move left
                    direction -= Vector3.left;
                }
            }
            else
            {
                if (touchDelta.y > 0)
                {
                    // Move forward
                    direction -= Vector3.forward;
                }
                else
                {
                    // Move back
                    direction -= Vector3.back;
                }
            }

            // Reset touch start position
            touchStartPosition = touch.position;
        }
    }

        if (direction == Vector3.zero)
        {
            return;
        }
        Move(direction);
    }
    public void Move(Vector3 direction)
    {
        // transform.DOMoveX(transform.position.x + direction.x*6f, durasi);
        // transform.DOMoveZ(transform.position.z + direction.z*6f, durasi);
        // var seq = DOTween.Sequence();
        // seq.Append(transform.DOMoveY(jumpHeight, durasi));
        // seq.Append(transform.DOMoveY(0, durasi));
        
        var targetPosition = transform.position + direction*6;
        if (targetPosition.x < leftMoveLimit || 
            targetPosition.x > rightMoveLimit ||
            targetPosition.z > backMoveLimit ||
            Tree.AllPosition.Contains(targetPosition) )
        {
            targetPosition = transform.position;
        }

        transform
            .DOJump
            (
                targetPosition, 
                jumpHeight,
                1,
                durasi
            )
            .onComplete = BroadCastPositionOnJumpEnd;
        transform.forward = -direction;
    }
    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMoveLimit = -(horizontalSize/2)*6;
        rightMoveLimit = (horizontalSize/2)*6;
        backMoveLimit = -(backLimit*6);
    }
    private void BroadCastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }
}
