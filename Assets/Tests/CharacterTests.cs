using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using StarterAssets;

public class CharacterTests : InputTestFixture
{
    GameObject character = Resources.Load<GameObject>("Character");
    Keyboard keyboard;


    public override void Setup()
    {
        SceneManager.LoadScene("Scenes/SimpleTesting");
        base.Setup();
        keyboard = InputSystem.AddDevice<Keyboard>();
        var mouse = InputSystem.AddDevice<Mouse>();
        Press(mouse.rightButton);
        Release(mouse.rightButton);
    }

    [Test]
    public void TestPlayerInstatiation()
    {
        GameObject characterInstance = GameObject.Instantiate(character, Vector3.zero, Quaternion.identity);
        Assert.That(characterInstance, Is.Not.Null);   
    }

    [UnityTest]
    public IEnumerator TestPlayerMoves()
    {
        GameObject characterInstance = GameObject.Instantiate<GameObject>(character, Vector3.zero, Quaternion.identity);
        Press(keyboard.upArrowKey);
        yield return new WaitForSeconds(1f);
        Release(keyboard.upArrowKey);
        yield return new WaitForSeconds(1f);
        Assert.That(characterInstance.transform.GetChild(0).transform.position.z, Is.GreaterThan(1.5f));
    }

    [UnityTest]
    public IEnumerator TestPlayerFallDamage()
    {
        GameObject characterInstance = GameObject.Instantiate(character, new Vector3(0f, 4f, 17.2f), Quaternion.identity);
        var characterHealth = characterInstance.GetComponent<PlayerHealth>();
        Assert.That(characterHealth.Health, Is.EqualTo(1f));
        Press(keyboard.upArrowKey);
        yield return new WaitForSeconds(0.5f);
        Release(keyboard.upArrowKey);
        yield return new WaitForSeconds(2f);
        Assert.That(characterHealth.Health, Is.EqualTo(0.9f));
    }
}
