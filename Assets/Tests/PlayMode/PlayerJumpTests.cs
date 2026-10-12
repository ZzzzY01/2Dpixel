using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerJumpTests
{
    private GameObject player;
    private GameObject ground;

    [UnityTest]
    public IEnumerator LeavingGroundDoesNotRestoreConsumedAirJump()
    {
        ground = new GameObject("Ground");
        ground.layer = LayerMask.NameToLayer("Ground");
        ground.transform.position = Vector3.zero;
        BoxCollider2D groundCollider = ground.AddComponent<BoxCollider2D>();
        groundCollider.size = new Vector2(10f, 1f);

        player = new GameObject("PlayerJumpTest");
        player.transform.position = new Vector3(0f, 1f, 0f);
        Rigidbody2D rigidbody = player.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0f;
        player.AddComponent<Animator>();
        player.AddComponent<AudioSource>();
        ParticleSystem particles = player.AddComponent<ParticleSystem>();

        Transform groundCheck = new GameObject("groundCheck").transform;
        groundCheck.SetParent(player.transform);
        groundCheck.localPosition = new Vector3(0f, -0.4f, 0f);

        Transform killPoint = new GameObject("killPoint").transform;
        killPoint.SetParent(player.transform);

        Type movementType = Type.GetType("PlayerMovement, Assembly-CSharp", true);
        MonoBehaviour movement = (MonoBehaviour)player.AddComponent(movementType);
        movementType.GetField("playerPs").SetValue(movement, particles);
        movementType.GetField("groundCheck").SetValue(movement, groundCheck);
        movementType.GetField("ground").SetValue(movement, (LayerMask)(1 << ground.layer));

        // Allow Start and one grounded FixedUpdate to establish the current state.
        yield return null;
        yield return new WaitForFixedUpdate();

        movementType.GetField("jumpCount").SetValue(movement, 1);
        rigidbody.velocity = Vector2.up;

        // The ground-check circle still overlaps briefly while the player starts rising.
        yield return new WaitForFixedUpdate();

        int remainingJumps = (int)movementType.GetField("jumpCount").GetValue(movement);
        Assert.AreEqual(
            1,
            remainingJumps,
            "Leaving the ground must not reset the consumed jump and enable a third jump."
        );
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (player != null)
        {
            UnityEngine.Object.Destroy(player);
        }

        if (ground != null)
        {
            UnityEngine.Object.Destroy(ground);
        }

        yield return null;
    }
}
