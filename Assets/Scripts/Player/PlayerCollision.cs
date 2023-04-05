using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float pushPower;

    IEnumerator DestroyAfterSeconds(GameObject gameObj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObj);
    }

    // For Character Controller Collision. Responding to characterController.Move() movements
    void OnControllerColliderHit(ControllerColliderHit hit) // hit: the hitting info
    {
        Rigidbody colliderRB = hit.collider.attachedRigidbody;

        if (colliderRB == null || colliderRB.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        // Crate hit case
        if (hit.collider.tag == "Crate")
        {
            Transform PlasmaExplosionEffect = hit.collider.gameObject.transform.GetChild(0);
            ParticleSystem PS = PlasmaExplosionEffect.GetComponent<ParticleSystem>();
            PS.Play();
            StartCoroutine(DestroyAfterSeconds(hit.collider.gameObject, 1.5f));
        }

        // Calculate push direction from move direction,
        // Push objects only to the sides, never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        colliderRB.velocity = pushDir * pushPower;
    }
}
