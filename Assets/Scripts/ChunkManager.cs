using UnityEngine;
using System.Collections;

public class ChunkManager : MonoBehaviour
{
    public bool corunning = false;
    public GameObject player;
    public GameObject chunkvoxels;

    private void Start()
    {
        StartCoroutine(Delayexit());
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
    
    public IEnumerator Delayexit()
    {
        corunning = true;
        yield return new WaitForSeconds(0.2f);
        chunkvoxels.SetActive(false);
    }

    void Update()
    {
        chunkvoxels.SetActive(Mathf.Abs(player.transform.position.y - transform.position.y - 5) < 8.0f);
    }
}
