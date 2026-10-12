using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class PlayerAnimationEventTests
{
    [Test]
    public void DeathClipRestartsLevelAtTheEnd()
    {
        AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Animator/die.anim");

        Assert.IsNotNull(clip);
        AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
        Assert.IsFalse(events.Any(animationEvent => string.IsNullOrEmpty(animationEvent.functionName)));
        Assert.IsTrue(events.Any(animationEvent => animationEvent.functionName == "RestartLevel"));
    }
}
