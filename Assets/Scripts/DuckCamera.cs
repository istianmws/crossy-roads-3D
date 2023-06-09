using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DuckCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(0,1)] float moveDuration = 0.2f;
    private void Start() {
        offset=this.transform.position;
    }
    public void updatePosition(Vector3 targetPosition)
    {
        DOTween.Kill(this.transform);
        transform.DOMove(offset + targetPosition, moveDuration);
    }
}
