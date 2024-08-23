using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    public CollectibleManager prefab;
    public int NumberOfDesiredKeys;
    public int NumberOfObjects = 10;
    [SerializeField] float spawnPaddingFromCamera;
    [SerializeField] float spawnPaddingFromCollectible;
    [SerializeField] Transform collectibleParent;
    List<CollectibleManager> allCollectibles;
    public void SpawnCollectible()
    {
        allCollectibles = new();
        SpawnObjects();
    }

    void SpawnObjects()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        Vector3 camPosition = cam.transform.position;
        float minX = camPosition.x - camWidth / 2 + spawnPaddingFromCamera;
        float maxX = camPosition.x + camWidth / 2 - spawnPaddingFromCamera;
        float minY = camPosition.y - camHeight / 2 + spawnPaddingFromCamera;
        float maxY = camPosition.y + camHeight / 2 - spawnPaddingFromCamera;
        var selectedNumbers=Level2Manager.GetUniqueRandomNumbers(NumberOfDesiredKeys, 0, NumberOfObjects-1);
        var desiredKeyString = PlayerManager.Instance.KeyString;
        for (int i = 0; i < NumberOfObjects; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                0f
            );
            if (!CheckForSafety(spawnPosition))
            {
                i--;
                continue;
            }
            var newCollectible=Instantiate(prefab, collectibleParent,collectibleParent);
            newCollectible.transform.position = spawnPosition;
            var selectedKey = "nothing";
            if (selectedNumbers.Contains(i))
            {
                selectedKey = desiredKeyString;
            }
            newCollectible.Init(selectedKey);
            allCollectibles.Add(newCollectible);
        }
        Debug.Log("Spawned");
    }
    bool CheckForSafety(Vector3 spawnPosition)
    {
        bool isSafe = true;
        foreach (var collectible in allCollectibles)
        {
            if (Vector3.Distance(collectible.transform.position, spawnPosition) < spawnPaddingFromCollectible)
            {
                isSafe = false; break;
            }
        }
        if (Vector3.Distance(PlayerManager.Instance.transform.position, spawnPosition) < spawnPaddingFromCollectible)
        {
            isSafe = false;
        }
        return isSafe;
    }
}
