using System;
using UnityEngine;

public class RopeCylinder : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    public Character PullingCharacter;
    public float textureTilePerUnit = 1f;

    [SerializeField] private Renderer rend;
    [SerializeField] private Transform rope;

    public ParticleSystem eIsPulled;

    public float ropeLength = 0f;
    public float tensionStrength = 10f;
    public float maxPullSpeed = 15f;


    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPos;

    void FixedUpdate()
    {
        if (PullingCharacter != null)
        {
            pointB.position = PullingCharacter.transform.position;
            targetPos = new Vector3(pointB.position.x, pointA.position.y, pointB.position.z);
            Vector3 dir = targetPos - pointA.position;
            Vector3 mid = (pointA.position + targetPos) / 2f;
            float length = dir.magnitude;

            rope.position = mid;
            rope.up = dir.normalized;
            rope.localScale = new Vector3(0.05f, length / 2f, 0.05f); // scale Y = half-length

            if (rend.sharedMaterial.HasProperty("_MainTex"))
            {
                Vector2 tiling = new Vector2(1, length * textureTilePerUnit);
                rend.sharedMaterial.mainTextureScale = tiling;
            }


            Vector3 offset = pointA.position - targetPos;
            float distance = offset.magnitude;

            if (distance > ropeLength)
            {
                float excessLength = distance - ropeLength;
                Vector3 direction = offset.normalized;
                float pullSpeed = Mathf.Min(excessLength * tensionStrength, maxPullSpeed);

                Vector3 movement = direction * pullSpeed * Time.deltaTime;

                Vector3 flatVelocity = new Vector3(movement.x, 0f, movement.z);
                if (flatVelocity.sqrMagnitude > Constants.EPSILON)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(flatVelocity, Vector3.up);
                    PullingCharacter.transform.rotation = Quaternion.Slerp(PullingCharacter.transform.rotation, targetRotation, 10f * Time.deltaTime);
                }

                PullingCharacter.CurrentVelocity = flatVelocity;

                if (eIsPulled != null && !eIsPulled.isPlaying)
                {
                    Vector3 v = PullingCharacter.transform.position;
                    eIsPulled.transform.position = new Vector3(v.x, v.y + 0.2f, v.z);
                    eIsPulled.Play();
                }
            }
            else
            {
                if (eIsPulled != null && eIsPulled.isPlaying)
                {
                    eIsPulled.Stop();
                }
            }
        }

    }

    internal void Reset()
    {
        rope.localScale = Vector3.zero;
        PullingCharacter.CurrentVelocity = Vector3.zero;
        PullingCharacter = null;
        gameObject.SetActive(false);
    }
}
