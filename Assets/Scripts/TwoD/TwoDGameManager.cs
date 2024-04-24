using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDGameManager : MonoBehaviour
{
    bool isPaused = false;
    int SHealth = 300;//ship health
    [SerializeField] GameObject wormPrefab;
    [SerializeField] GameObject boostPrefab;
    [SerializeField] GameObject repairPrefab;
    [SerializeField] Transform[] SpawnPts;
    private GameObject[] objects;
    public int numberOfObjects = 3;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.TWOD_PAUSED, PauseObject);
        Messenger.AddListener(GameEvent.TWOD_RESUMED, ResumeObject);
        Messenger.AddListener(GameEvent.SHIP_DAMAGE, shipReactToHit);
        Messenger.AddListener(GameEvent.REPAIR, shipRepair);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TWOD_PAUSED, PauseObject);
        Messenger.RemoveListener(GameEvent.TWOD_RESUMED, ResumeObject);
        Messenger.RemoveListener(GameEvent.SHIP_DAMAGE, shipReactToHit);
    }
    // Start is called before the first frame update
    void Start()
    {
        // instantiate the array
        objects = new GameObject[numberOfObjects];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            List<int> usedSpawnIndices = new List<int>(); // Track used spawn point indices(to stop damn double spawn(why? how? don't know.))
                                                          //(update: still happens no idea how to stop. just makes more luck) letting the double live!)
            for (int i = 0; i < numberOfObjects; i++)
            {
                if (objects[i] == null)
                {
                    int spawnIndex = GetUnusedSpawnIndex(usedSpawnIndices);

                    // If all spawn points have been used(should happen but...
                    if (spawnIndex == -1)
                    {
                        break;
                    }

                    // Mark used
                    usedSpawnIndices.Add(spawnIndex);
                    //random object
                    GameObject prefabToSpawn = ChooseObjectToSpawn();
                    Transform spawnPoint = SpawnPts[Random.Range(0, SpawnPts.Length)];
                    // Instantiate
                    GameObject newObj = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

                    // Store
                    objects[i] = newObj;
                }
            }
        }
    }
    // Choose which object 
    GameObject ChooseObjectToSpawn()
    {
        float rand = Random.value;

        if (rand < 0.6f)
        {
            return wormPrefab; // 60% 
        }
        else if (rand < 0.8f)
        {
            return boostPrefab; // 20%
        }
        else
        {
            return repairPrefab; // 20%
        }
    }
    // Choose spawn point
    int GetUnusedSpawnIndex(List<int> usedSpawnIndices)
    {
        List<int> unusedSpawnIndices = new List<int>();

        // Find unused
        for (int i = 0; i < SpawnPts.Length; i++)
        {
            if (!usedSpawnIndices.Contains(i))
            {
                unusedSpawnIndices.Add(i);
            }
        }

        if (unusedSpawnIndices.Count > 0)
        {
            return unusedSpawnIndices[Random.Range(0, unusedSpawnIndices.Count)];
        }
        else
        {
            return -1;
        }
    }
    //for update ship health when worm becomes mush
    void shipReactToHit()
    {
        SHealth -= 10;
        Messenger<int>.Broadcast(GameEvent.SHIP_HEALTH_CHANGED, SHealth);
    }

    //method to restore ship health when repaired
    void shipRepair()
    {
        SHealth += 30;
        if(SHealth > 300)
        {
            SHealth = 300;
        }
        Messenger<int>.Broadcast(GameEvent.SHIP_HEALTH_CHANGED, SHealth);
    }

    // Method to pause 
    public void PauseObject()
    {
        isPaused = true;
    }

    // Method to resume
    public void ResumeObject()
    {
        isPaused = false;
    }
}
