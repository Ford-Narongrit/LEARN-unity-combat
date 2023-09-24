using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerMovementController mov;
    private SpriteRenderer spriteRend;

    [Header("Movement Tilt")]
    [SerializeField] private float maxTilt;
    [SerializeField][Range(0, 1)] private float tiltSpeed;
    [Header("Component")]
    [SerializeField] private GameObject parent;

    [Header("Particle FX")]
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject landFX;
    [SerializeField] private GameObject dashFX;

    [Header("Particle FX")]
    private ParticleSystem _jumpParticle;
    private ParticleSystem _landParticle;
    private ParticleSystem _dashParticle;

    public bool startedJumping { private get; set; }
    public bool justLanded { private get; set; }
    public bool startedDashing { private get; set; }

    public float currentVelY;

    private void Start()
    {
        mov = GetComponent<PlayerMovementController>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();

        _jumpParticle = jumpFX.GetComponent<ParticleSystem>();
        _landParticle = landFX.GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        #region Tilt
        float tiltProgress;

        int mult = -1;

        if (mov.IsSliding)
        {
            tiltProgress = 0.25f;
        }
        else
        {
            tiltProgress = Mathf.InverseLerp(-mov.Data.runMaxSpeed, mov.Data.runMaxSpeed, mov.RB.velocity.x);
            mult = (mov.IsFacingRight) ? 1 : -1;
        }

        float newRot = ((tiltProgress * maxTilt * 2) - maxTilt);
        float rot = Mathf.LerpAngle(spriteRend.transform.localRotation.eulerAngles.z * mult, newRot, tiltSpeed);
        spriteRend.transform.localRotation = Quaternion.Euler(0, 0, rot * mult);
        #endregion

        CheckAnimationState();
    }

    private void CheckAnimationState()
    {
        if (startedJumping)
        {
            GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
            obj.transform.parent = parent.transform;
            Destroy(obj, 1);
            startedJumping = false;
            return;
        }

        if (justLanded)
        {
            GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * transform.localScale.y / 1.5f), Quaternion.Euler(-90, 0, 0));
            obj.transform.parent = parent.transform;

            Destroy(obj, 1);
            justLanded = false;
            return;
        }

        if (startedDashing)
        {
            GameObject obj = Instantiate(dashFX, transform.position - (Vector3.up * transform.localScale.y / 2f), Quaternion.Euler(-90, 0, 0));
            obj.transform.parent = parent.transform;
            Destroy(obj, mov.Data.dashDuration);
            startedDashing = false;
            return;
        }

    }
}
