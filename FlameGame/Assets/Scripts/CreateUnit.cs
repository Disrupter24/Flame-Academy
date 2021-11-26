using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnit : MonoBehaviour
{
    [SerializeField]
    private GameObject _worker;
    [SerializeField]
    private Transform _spawnLocation; 
    private void OnMouseUpAsButton()
    {
        //if there are enough ashes
        //remove ashes
        Debug.Log("Spawning Unit");
        if (SpawnUnit())
        {

        } else
        {
            //no spots to spawn into
            Debug.Log("Spawn failed no spots left");
        }
        //if not enough ashes for another unit change the sprite
    }


    private bool SpawnUnit()
    {
        Vector2 newPosition = NavigationGrid.GetPositionOfAnEmptySurroundingTile(_spawnLocation.position.x, _spawnLocation.position.y);
        if (NavigationGrid.HasWorkerOntile(newPosition.x, newPosition.y)) return false; 

        GameObject newWorker = Instantiate(_worker);
        newWorker.transform.position = newPosition;
        WorkerStateManager worker = newWorker.GetComponent<WorkerStateManager>();
        NavigationGrid.SetNodeOccupied(newWorker.transform.position.x, newWorker.transform.position.y, worker);
        return true;
    }
}
