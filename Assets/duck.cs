using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class duck : MonoBehaviour
{
    [SerializeField] bool isMoving;
    [SerializeField, Range(0,1)]  float durasi;
    [SerializeField, Range(0,2)]  float jumpHeight;
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
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            direction += Vector3.back;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        { 
            direction += Vector3.right;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            direction += Vector3.left;
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
        transform.DOJump(transform.position + direction*6f, jumpHeight,1,durasi);
    }
}
