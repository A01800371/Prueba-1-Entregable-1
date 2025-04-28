using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class TestPuertas
{
    private GameObject player;
    private MockSceneLoader mockLoader;

    [SetUp]
    public void Setup()
    {
        player = new GameObject("Player");
        player.tag = "Player";

        mockLoader = new MockSceneLoader();
    }

    [UnityTest]
    public IEnumerator PuertaB_CargaSelectorBlockchain()
    {
        var puerta = new GameObject("PuertaB");
        var script = puerta.AddComponent<PuertaB>();
        puerta.AddComponent<BoxCollider2D>().isTrigger = true;

        // Inyectar nuestro Mock
        script.sceneLoader = mockLoader;

        // Simular la colisión
        script.SendMessage("OnTriggerEnter2D", player.AddComponent<BoxCollider2D>());

        yield return null;

        Assert.AreEqual("Selector Blockchain", mockLoader.LastSceneLoaded);
    }

    [UnityTest]
    public IEnumerator PuertaC_CargaSelectorCryptocurrency()
    {
        var puerta = new GameObject("PuertaC");
        var script = puerta.AddComponent<PuertaC>();
        puerta.AddComponent<BoxCollider2D>().isTrigger = true;

        script.sceneLoader = mockLoader;

        script.SendMessage("OnTriggerEnter2D", player.AddComponent<BoxCollider2D>());

        yield return null;

        Assert.AreEqual("Selector Cryptocurrency", mockLoader.LastSceneLoaded);
    }

    [UnityTest]
    public IEnumerator PuertaW_CargaSelectorWeb3()
    {
        var puerta = new GameObject("PuertaW");
        var script = puerta.AddComponent<PuertaW>();
        puerta.AddComponent<BoxCollider2D>().isTrigger = true;

        script.sceneLoader = mockLoader;

        script.SendMessage("OnTriggerEnter2D", player.AddComponent<BoxCollider2D>());

        yield return null;

        Assert.AreEqual("Selector Web 3", mockLoader.LastSceneLoaded);
    }

    // Mock del SceneLoader
    private class MockSceneLoader : ISceneLoader
    {
        public string LastSceneLoaded { get; private set; }

        public void LoadScene(string sceneName)
        {
            LastSceneLoaded = sceneName;
        }
    }
}
