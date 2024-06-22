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
        // 找到小女巫对象
        player = GameObject.Find("Player");
        if (player != null)
        {
            player.GetComponent<SpriteRenderer>().sprite = playerSprite; // 分配玩家精灵
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

        // 找到关卡点对象
        levelPoints = new Transform[3];
        for (int i = 0; i < levelPoints.Length; i++)
        {
            var levelPointObj = GameObject.Find("LevelPoint" + (i + 1));
            if (levelPointObj != null)
            {
                levelPoints[i] = levelPointObj.transform;
                levelPointObj.GetComponent<SpriteRenderer>().sprite = levelPointSprite; // 分配关卡点精灵
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

            // 只允许点击当前目标关卡点
            if (Vector3.Distance(mousePos, levelPoints[currentLevelIndex].position) < 1.0f) // 调整检测半径为1.0f
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
            // 点亮当前关卡点（可以通过改变精灵颜色或者其他方式实现）
            levelPoints[currentLevelIndex].GetComponent<SpriteRenderer>().color = Color.green;

            OnLevelReached(currentLevelIndex);
        }
    }

    void OnLevelReached(int levelIndex)
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levelPoints.Length)
        {
            // 当所有关卡点都被访问后，跳转到最终场景（或者重新开始）
            Debug.Log("All levels completed");
            currentLevelIndex = 0; // 重置为第一个关卡点
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

// 这个脚本用于控制关卡选择界面，玩家可以通过点击不同的关卡点来选择并加载相应的关卡。

public class LevelSelector : MonoBehaviour
{
    public Sprite playerSprite; // 在Inspector面板中分配玩家精灵
    public Sprite levelPointSprite; // 在Inspector面板中分配关卡点精灵

    private GameObject player; // 玩家游戏对象
    private Transform[] levelPoints; // 关卡点数组
    private bool isMoving = false; // 玩家是否正在移动到关卡点
    private int currentLevelIndex = 0; // 当前选中的关卡索引

    void Start()
    {
        // 初始化玩家对象，设置其精灵和碰撞器，并放置在起始位置
        GameObject playerObject = new GameObject("Player");
        playerObject.AddComponent<SpriteRenderer>().sprite = playerSprite;
        playerObject.AddComponent<BoxCollider2D>().isTrigger = true;
        playerObject.transform.position = new Vector3(-6, 2, 0);
        player = playerObject;

        // 创建并初始化三个关卡点，每个关卡点都有自己的位置
        levelPoints = new Transform[3];

        // 关卡点1
        GameObject levelPoint1 = CreateLevelPoint(new Vector3(1, 3, 0), "LevelPoint1");
        levelPoints[0] = levelPoint1.transform;

        // 关卡点2
        GameObject levelPoint2 = CreateLevelPoint(new Vector3(-2, 0, 0), "LevelPoint2");
        levelPoints[1] = levelPoint2.transform;

        // 关卡点3
        GameObject levelPoint3 = CreateLevelPoint(new Vector3(1, -3, 0), "LevelPoint3");
        levelPoints[2] = levelPoint3.transform;
    }

    // 辅助函数，用于创建关卡点游戏对象并调整其大小
    private GameObject CreateLevelPoint(Vector3 position, string name)
    {
        GameObject levelPoint = new GameObject(name);
        levelPoint.AddComponent<SpriteRenderer>().sprite = levelPointSprite;
        levelPoint.GetComponent<SpriteRenderer>().transform.localScale *= 0.4f; // 将精灵大小缩小0.5倍
        levelPoint.AddComponent<BoxCollider2D>().isTrigger = true;
        levelPoint.transform.position = position;
        return levelPoint;
    }

    void Update()
    {
        // 如果玩家没有在移动，则检查鼠标输入
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

            // 检查鼠标点击位置是否接近任何一个关卡点
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
        // 移动玩家到选定的关卡点
        player.transform.position = Vector3.MoveTowards(player.transform.position, levelPoints[currentLevelIndex].position, Time.deltaTime * 5);

        // 到达关卡点后，停止移动并加载相应关卡
        if (Vector3.Distance(player.transform.position, levelPoints[currentLevelIndex].position) < 0.1f)
        {
            isMoving = false;
            OnLevelReached(currentLevelIndex);
        }
    }

    void OnLevelReached(int levelIndex)
    {
        // 根据当前关卡索引加载对应的关卡场景
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
