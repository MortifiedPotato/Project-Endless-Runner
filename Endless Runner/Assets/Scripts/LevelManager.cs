using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    [Header("Tile Properties")]
    [SerializeField] GameObject tilePrefab;
    [Range(0, 40)] public int tileSpeed = 10;
    public int obstaclePercentage = 15;
    [SerializeField] int respawnThreshold = -6;
    [SerializeField] int maxPooledTileCount = 60;
    [SerializeField, Range(0, 60)] int maxActiveTileCount = 14;
    [Header("Game Properties")]
    public int playerScore;

    private Tile[] pooledTiles;
    private LinkedList<Tile> activeTiles = new LinkedList<Tile>();

    private void Awake()
    {
        instance = this;
        pooledTiles = new Tile[maxPooledTileCount];
    }

    private void Start()
    {
        // Instantiate and pool all tiles
        for (int i = 0; i < pooledTiles.Length; i++)
        {
            GameObject tile = Instantiate(tilePrefab);
            tile.transform.parent = transform;
            tile.SetActive(false);
            pooledTiles[i] = tile.GetComponent<Tile>();
        }

        StartCoroutine(SpawnTile(maxActiveTileCount));
    }

    private void Update()
    {
        if (activeTiles.Count < maxActiveTileCount)
        {
            //int tilesToSpawn = maxActiveTileCount - activeTiles.Count;
            //StartCoroutine(SpawnTile(tilesToSpawn));
            return;
        }

        foreach (Tile tile in activeTiles)
        {
            tile.transform.position += Vector3.back * tileSpeed * Time.deltaTime;
        }

        // ---------------------

        foreach (Tile tile in activeTiles.ToList())
        {
            if (tile.transform.position.z <= respawnThreshold)
            {
                if (activeTiles.Count > maxActiveTileCount)
                {
                    activeTiles.Remove(tile);
                    tile.gameObject.SetActive(false);
                }
                else
                {
                    tile.transform.position += Vector3.forward * (activeTiles.Count * 3);
                }

                for (int i = 0; i < tile.obstacles.Count; i++)
                {
                    tile.obstacles[i].SetActive(false);
                    tile.challengeTrigger.SetActive(false);
                }

                if (CalculateObstacleChance() == true)
                {
                    tile.obstacles[Random.Range(0, tile.obstacles.Count)].SetActive(true);
                    tile.challengeTrigger.SetActive(true);
                }
            }
        }
    }

    IEnumerator SpawnTile(int count = 1)
    {
        if (activeTiles.Count >= maxActiveTileCount)
        {
            yield break;
        }

        for (int i = 0; i < count; i++)
        {
            HandleSpawn();
            yield return new WaitForSeconds(.001f);
        }
    }

    private void HandleSpawn()
    {
        if (!tilePrefab)
        {
            Debug.LogError("Tile Prefab is not assigned in " + this);
            return;
        }

        foreach (Tile tile in pooledTiles)
        {
            if (tile.gameObject.activeSelf == false)
            {
                //tile.road.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                tile.transform.position += Vector3.forward * (activeTiles.Count * 3);
                tile.gameObject.SetActive(true);
                activeTiles.AddFirst(tile);
                break;
            }
        }
    }

    public bool CalculateObstacleChance()
    {
        if (Random.Range(1, 100) <= obstaclePercentage)
        {
            return true;
        }
        return false;
    }
}
