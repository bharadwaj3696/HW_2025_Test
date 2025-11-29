using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 12f, -10f);

    [Header("Smoothing")]
    [SerializeField] private float smoothTime = 0.08f;

    [Header("Rotation")]
    [SerializeField] private bool lookAtTarget = true;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );

        if (lookAtTarget)
            transform.LookAt(target);
    }
}