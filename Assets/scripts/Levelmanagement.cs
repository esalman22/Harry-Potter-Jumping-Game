using UnityEngine;

public class Levelmanagement : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Side Columns")]
    public GameObject leftColumnPrefab;
    public GameObject rightColumnPrefab;
    public float columnHeight = 8f;

    [Header("Platform")]
    public GameObject platformPrefab;
    public float platformMinX = -2.5f;
    public float platformMaxX = 2.5f;

    [Header("Vertical Spacing")]
    public float minPlatformGap = 1.5f;  // أصغر مسافة بين منصتين
    public float maxPlatformGap = 2.5f;  // أكبر مسافة بين منصتين

    [Header("Start Settings")]
    public int startCount = 12;

    private float nextPlatformY = 1f;   // Y لأول منصة
    private float highestY = 0f;

    void Start()
    {
        nextPlatformY = player.position.y + 0.5f;
        highestY = player.position.y;

        // منصات بداية
        for (int i = 0; i < startCount; i++)
        {
            SpawnPlatform();
        }

        // أعمدة بداية
        for (int i = 0; i < 3; i++)
        {
            SpawnColumns();
        }
    }

    void Update()
    {
        // نولد منصات باستمرار فوق اللاعب
        while (player.position.y + 10f > nextPlatformY)
        {
            SpawnPlatform();
        }

        // نولد الأعمدة لما نحتاج
        if (player.position.y + 12f > highestY)
        {
            SpawnColumns();
        }
    }

    void SpawnColumns()
    {
        // ======== توليد الأعمدة ========

        Vector3 leftPos = new Vector3(leftColumnPrefab.transform.position.x, highestY, 0f);
        Vector3 rightPos = new Vector3(rightColumnPrefab.transform.position.x, highestY, 0f);

        Instantiate(leftColumnPrefab, leftPos, Quaternion.identity);
        Instantiate(rightColumnPrefab, rightPos, Quaternion.identity);

        highestY += columnHeight;
    }

    void SpawnPlatform()
    {
        // ======== توليد المنصة ========

        float randomX = Random.Range(platformMinX, platformMaxX);
        float randomGap = Random.Range(minPlatformGap, maxPlatformGap);

        Vector3 platformPos = new Vector3(randomX, nextPlatformY, 0f);
        Instantiate(platformPrefab, platformPos, Quaternion.identity);

        nextPlatformY += randomGap;
    }
}
