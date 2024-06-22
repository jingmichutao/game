/*using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Sprite playerSprite; // Assign this in the Inspector
    public Sprite levelPointSprite; // Assign this in the Inspector
    private GameObject player;
    private Transform[] levelPoints;
    private bool isMoving = false;
    private int currentLevelIndex = 0;

    void Start()
    {
        // �ҵ�СŮ�׶���
        player = GameObject.Find("Player");
        if (player != null)
        {
            player.GetComponent<SpriteRenderer>().sprite = playerSprite; // ������Ҿ���
            var playerCollider = player.GetComponent<BoxCollider2D>();
            if (playerCollider == null)
            {
                playerCollider = player.AddComponent<BoxCollider2D>();
            }
            playerCollider.isTrigger = true;
            Debug.Log("Player found and initialized");
        }
        else
        {
            Debug.LogError("Player object not found!");
        }

        // �ҵ��ؿ������
        levelPoints = new Transform[3];
        for (int i = 0; i < levelPoints.Length; i++)
        {
            var levelPointObj = GameObject.Find("LevelPoint" + (i + 1));
            if (levelPointObj != null)
            {
                levelPoints[i] = levelPointObj.transform;
                levelPointObj.GetComponent<SpriteRenderer>().sprite = levelPointSprite; // ����ؿ��㾫��
                var pointCollider = levelPointObj.GetComponent<BoxCollider2D>();
                if (pointCollider == null)
                {
                    pointCollider = levelPointObj.AddComponent<BoxCollider2D>();
                }
                pointCollider.isTrigger = true;
                Debug.Log("LevelPoint" + (i + 1) + " found and initialized");
            }
            else
            {
                Debug.LogError("LevelPoint" + (i + 1) + " object not found!");
            }
        }
    }

    void Update()
    {
        if (!isMoving)
        {
            CheckMouseInput();
        }
        else
        {
            MovePlayerToPoint();
        }
    }

    void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Debug.Log("Mouse clicked at: " + mousePos);

            // ֻ��������ǰĿ��ؿ���
            if (Vector3.Distance(mousePos, levelPoints[currentLevelIndex].position) < 1.0f) // �������뾶Ϊ1.0f
            {
                isMoving = true;
                Debug.Log("Target LevelPoint" + (currentLevelIndex + 1) + " selected");
            }
        }
    }

    void MovePlayerToPoint()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, levelPoints[currentLevelIndex].position, Time.deltaTime * 5);

        if (Vector3.Distance(player.transform.position, levelPoints[currentLevelIndex].position) < 0.1f)
        {
            isMoving = false;
            Debug.Log("Reached LevelPoint" + (currentLevelIndex + 1));
            // ������ǰ�ؿ��㣨����ͨ���ı侫����ɫ����������ʽʵ�֣�
            levelPoints[currentLevelIndex].GetComponent<SpriteRenderer>().color = Color.green;

            OnLevelReached(currentLevelIndex);
        }
    }

    void OnLevelReached(int levelIndex)
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levelPoints.Length)
        {
            // �����йؿ��㶼�����ʺ���ת�����ճ������������¿�ʼ��
            Debug.Log("All levels completed");
            currentLevelIndex = 0; // ����Ϊ��һ���ؿ���
        }
        else
        {
            switch (levelIndex)
            {
                case 0:
                    SceneManager.LoadScene("Level1");
                    break;
                case 1:
                    SceneManager.LoadScene("Level2");
                    break;
                case 2:
                    SceneManager.LoadScene("Level3");
                    break;
            }
        }
    }
}
*/



using UnityEngine;
using UnityEngine.SceneManagement;

// ����ű����ڿ��ƹؿ�ѡ����棬��ҿ���ͨ�������ͬ�Ĺؿ�����ѡ�񲢼�����Ӧ�Ĺؿ���

public class LevelSelector : MonoBehaviour
{
    public Sprite playerSprite; // ��Inspector����з�����Ҿ���
    public Sprite levelPointSprite; // ��Inspector����з���ؿ��㾫��

    private GameObject player; // �����Ϸ����
    private Transform[] levelPoints; // �ؿ�������
    private bool isMoving = false; // ����Ƿ������ƶ����ؿ���
    private int currentLevelIndex = 0; // ��ǰѡ�еĹؿ�����

    void Start()
    {
        // ��ʼ����Ҷ��������侫�����ײ��������������ʼλ��
        GameObject playerObject = new GameObject("Player");
        playerObject.AddComponent<SpriteRenderer>().sprite = playerSprite;
        playerObject.AddComponent<BoxCollider2D>().isTrigger = true;
        playerObject.transform.position = new Vector3(-6, 2, 0);
        player = playerObject;

        // ��������ʼ�������ؿ��㣬ÿ���ؿ��㶼���Լ���λ��
        levelPoints = new Transform[3];

        // �ؿ���1
        GameObject levelPoint1 = CreateLevelPoint(new Vector3(1, 3, 0), "LevelPoint1");
        levelPoints[0] = levelPoint1.transform;

        // �ؿ���2
        GameObject levelPoint2 = CreateLevelPoint(new Vector3(-2, 0, 0), "LevelPoint2");
        levelPoints[1] = levelPoint2.transform;

        // �ؿ���3
        GameObject levelPoint3 = CreateLevelPoint(new Vector3(1, -3, 0), "LevelPoint3");
        levelPoints[2] = levelPoint3.transform;
    }

    // �������������ڴ����ؿ�����Ϸ���󲢵������С
    private GameObject CreateLevelPoint(Vector3 position, string name)
    {
        GameObject levelPoint = new GameObject(name);
        levelPoint.AddComponent<SpriteRenderer>().sprite = levelPointSprite;
        levelPoint.GetComponent<SpriteRenderer>().transform.localScale *= 0.4f; // �������С��С0.5��
        levelPoint.AddComponent<BoxCollider2D>().isTrigger = true;
        levelPoint.transform.position = position;
        return levelPoint;
    }

    void Update()
    {
        // ������û�����ƶ��������������
        if (!isMoving)
        {
            CheckMouseInput();
        }
        else
        {
            MovePlayerToPoint();
        }
    }

    void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // ��������λ���Ƿ�ӽ��κ�һ���ؿ���
            for (int i = 0; i < levelPoints.Length; i++)
            {
                if (Vector3.Distance(mousePos, levelPoints[i].position) < 0.5f)
                {
                    currentLevelIndex = i;
                    isMoving = true;
                    break;
                }
            }
        }
    }

    void MovePlayerToPoint()
    {
        // �ƶ���ҵ�ѡ���Ĺؿ���
        player.transform.position = Vector3.MoveTowards(player.transform.position, levelPoints[currentLevelIndex].position, Time.deltaTime * 5);

        // ����ؿ����ֹͣ�ƶ���������Ӧ�ؿ�
        if (Vector3.Distance(player.transform.position, levelPoints[currentLevelIndex].position) < 0.1f)
        {
            isMoving = false;
            OnLevelReached(currentLevelIndex);
        }
    }

    void OnLevelReached(int levelIndex)
    {
        // ���ݵ�ǰ�ؿ��������ض�Ӧ�Ĺؿ�����
        switch (levelIndex)
        {
            case 0:
                SceneManager.LoadScene("Level1");
                break;
            case 1:
                SceneManager.LoadScene("Level2");
                break;
            case 2:
                SceneManager.LoadScene("Level3");
                break;
            default:
                Debug.LogWarning("Invalid level index.");
                break;
        }
    }
}
