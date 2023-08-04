using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager shareInstance;

    public List<LevelBlock> allLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPosition;
    void Awake()
    {
        if(shareInstance == null)
        {
            shareInstance = this;
        }   
    }
    // Start is called before the first frame update
    void Start()
    {
        generateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addLevelBlock() {
        int randomIdx = Random.Range(0, allLevelBlocks.Count);
        LevelBlock block;

        Vector3 spawnPosition = Vector3.zero;

        if(currentLevelBlocks.Count == 0){
            block = Instantiate(allLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count-1].endPoint.position;
        }
        block.transform.SetParent(this.transform, false);
        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x, spawnPosition.y - block.startPoint.position.y,0);
        block.transform.position = correction;
        currentLevelBlocks.Add(block);
    }
    public void removeLevelBlock()
    {
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);  
    }
    public void removeAllLevelBlocks() { 
        while(currentLevelBlocks.Count > 0)
        {
            removeLevelBlock();
        }
    } 
    public void generateInitialBlocks() {
        for(int i = 0; i < 5; i++) { 
            addLevelBlock();
        }
    }
}
