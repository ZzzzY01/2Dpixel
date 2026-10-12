using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerDeathTests
{
    private GameObject player;

    [UnityTest]
    public IEnumerator DeathDisablesPlayerMovementBeforeMakingBodyStatic()
    {
        player = new GameObject("PlayerDeathTest");
        Rigidbody2D rigidbody = player.AddComponent<Rigidbody2D>();
        player.AddComponent<Animator>();
        ParticleSystem particles = player.AddComponent<ParticleSystem>();

        Type movementType = Type.GetType("PlayerMovement, Assembly-CSharp", true);
        Type lifeType = Type.GetType("playerLife, Assembly-CSharp", true);
        Behaviour movement = (Behaviour)player.AddComponent(movementType);
        MonoBehaviour life = (MonoBehaviour)player.AddComponent(lifeType);

        lifeType.GetField("playerPs").SetValue(life, particles);

        yield return null;

        MethodInfo death = lifeType.GetMethod("Death", BindingFlags.Instance | BindingFlags.NonPublic);
        death.Invoke(life, null);

        Assert.IsFalse(movement.enabled, "PlayerMovement must stop when the player dies.");
        Assert.AreEqual(RigidbodyType2D.Static, rigidbody.bodyType);
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (player != null)
        {
            UnityEngine.Object.Destroy(player);
            yield return null;
        }
    }
}
