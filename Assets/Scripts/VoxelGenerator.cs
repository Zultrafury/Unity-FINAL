using Unity.VisualScripting;
using UnityEngine;

public class VoxelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject voxel;
    [SerializeField] private GameObject chunkManager;
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private int worldSize;
    
    void Start()
    {
        if (false)
        {
            int worldHalf = worldSize / 2;
            int yits = 0;
            GameObject cm = null;
            for (int ys = -1; ys > -worldSize - 1; ys--)
            {
                if (yits % 5 == 0)
                {
                    cm = Instantiate(chunkManager, transform.position, transform.rotation);
                    cm.transform.position = new Vector3(0, ys, 0);
                }

                yits += 1;
                for (int zs = -worldHalf; zs < worldHalf; zs++)
                {
                    for (int xs = -worldHalf; xs < worldHalf; xs++)
                    {
                        GameObject v = Instantiate(voxel, transform);
                        v.transform.position = new Vector3(xs, ys, zs);
                        v.transform.SetParent(cm.transform, true);
                    }
                }
            }
            Destroy(this);
        }
    }

    private int yit = 0;
    private Vector3 clamp = Vector3.zero;

    Vector3Int RoundVec3(Vector3 originalVector)
    {
        return new Vector3Int(Mathf.RoundToInt(originalVector.x), Mathf.RoundToInt(originalVector.y), Mathf.RoundToInt(originalVector.z));
    }
    void FixedUpdate()
    {
        if (generator == null) return;
        Vector3Int newclamp = RoundVec3(generator.transform.position);
        if (Mathf.RoundToInt(clamp.y) == newclamp.y) return;
        clamp = newclamp;
        GameObject cm = null;
        int worldHalf = worldSize / 2;
        ChunkManager cmcm = null;
        if (yit % 1 == 0)
        {
            cm = Instantiate(chunkManager, transform.position, transform.rotation);
            cm.transform.position = new Vector3(0, newclamp.y, 0);
            cmcm = cm.GetComponent<ChunkManager>();
            cmcm.SetPlayer(player);
        }

        if (yit % 20 == 19)
        {
            GameObject enemys = Instantiate(enemySpawner, transform);
            enemys.transform.position = new Vector3(newclamp.x, newclamp.y, newclamp.z);
            enemys.transform.SetParent(cmcm.chunkvoxels.transform, true);
            enemys.GetComponent<EnemySpawner>().player = cmcm.player;
        }

        yit += 1;
        for (int zs = newclamp.z - worldHalf; zs < newclamp.z + worldHalf; zs++)
        {
            for (int xs = newclamp.x - worldHalf; xs < newclamp.x + worldHalf; xs++)
            {
                GameObject v = Instantiate(voxel, transform);
                v.transform.position = new Vector3(xs, newclamp.y, zs);
                v.transform.SetParent(cmcm.chunkvoxels.transform, true);
            }
        }
    }
}
