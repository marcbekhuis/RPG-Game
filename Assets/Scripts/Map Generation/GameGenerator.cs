using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameGenerator : MonoBehaviour
{
    public static UnityEvent<string> updateLoadText = new UnityEvent<string>();

    [Tooltip("Map size in tiles")] [SerializeField] private Vector2Int mapSize;
    [SerializeField] private GameObject loadscreen;
    [SerializeField] private Text loadscreenText;

    private LandscapeGenerator landscapeGenerator;
    private ResourceGenerator resourceGenerator;

    private void Start()
    {
        TryGetComponent<LandscapeGenerator>(out landscapeGenerator);
        TryGetComponent<ResourceGenerator>(out resourceGenerator);
        updateLoadText.AddListener(UpdateLoadText);
        StartCoroutine(GenerateGame());
    }

    private IEnumerator GenerateGame()
    {
        GameManager.instance.GameLoading = true;
        if (loadscreen) loadscreen.SetActive(true);
        if (landscapeGenerator) yield return StartCoroutine(landscapeGenerator.MakeLandscape(mapSize));
        if (resourceGenerator) yield return StartCoroutine(resourceGenerator.MakeResources(mapSize));
        if (loadscreen) loadscreen.SetActive(false);
        GameManager.instance.GameLoading = false;
    }

    private void UpdateLoadText(string text)
    {
        if (loadscreenText) loadscreenText.text = text;
    }
}
