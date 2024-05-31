using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInstance : MonoBehaviour
{
    public Texture2D tex; // 방의 텍스처
    public Texture2D startRoomTexture;
    public Texture2D bossRoomTexture;
    public Vector2 gridPos;
    public int type; // 0: normal, 1: enter, 2: boss
    public bool doorTop, doorBot, doorLeft, doorRight;
    public int nextToBossRoom; // 이웃 방중에 보스 방 있는지 확인
    [SerializeField]
    GameObject doorU, doorD, doorL, doorR, doorWall;
    [SerializeField]
    GameObject bossDoorU, bossDoorD, bossDoorL, bossDoorR;
    [SerializeField]
    ColorToGameObject[] mappings;
    [SerializeField]
    float tileSize = 16;
    [SerializeField]
    Vector2 roomSizeInTiles = new Vector2(9, 17);

    public string nextStage;
    private MonsterManager monsterManager;
    public GameObject stageTargetPrefab; // Stage Target Prefab
    private GameObject stageTargetInstance; // 인스턴스 변수


    public void Setup(Texture2D _tex = null, Vector2 _gridPos = default(Vector2), int _type = 0, bool _doorTop = false, bool _doorBot = false, bool _doorLeft = false, bool _doorRight = false, int _nextToBossRoom = 0)
    {
        tex = _tex;
        gridPos = _gridPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        nextToBossRoom = _nextToBossRoom;
        MakeDoors();
        GenerateRoomTiles();

        monsterManager = FindObjectOfType<MonsterManager>();
    }

    void MakeDoors()
    {
        if (type == 2)
        {
            MakeBossRoomDoors();
        }
        else
        {
            // Check if this room is next to a boss room in any direction
            if (nextToBossRoom == 0)
            {
                // Place regular doors if not next to a boss room
                Vector3 spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
                PlaceDoor(spawnPos, doorTop, doorU);
                spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
                PlaceDoor(spawnPos, doorBot, doorD);
                spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
                PlaceDoor(spawnPos, doorRight, doorR);
                spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
                PlaceDoor(spawnPos, doorLeft, doorL);
            }
            // Place boss doors if next to a boss room in the specified direction
            else if (nextToBossRoom == 1)
            {
                Vector3 spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
                PlaceDoor(spawnPos, doorTop, bossDoorU);
                MakeBossNextRoomDoors(1);
            }
            else if (nextToBossRoom == 3)
            {
                Vector3 spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
                PlaceDoor(spawnPos, doorBot, bossDoorD);
                MakeBossNextRoomDoors(3);
            }
            else if (nextToBossRoom == 4)
            {
                Vector3 spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
                PlaceDoor(spawnPos, doorRight, bossDoorR);
                MakeBossNextRoomDoors(4);
            }
            else if (nextToBossRoom == 2)
            {
                Vector3 spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
                PlaceDoor(spawnPos, doorLeft, bossDoorL);
                MakeBossNextRoomDoors(2);
            }
        }
    }

    void MakeBossRoomDoors()
    {
        Vector3 spawnPos;
        // top door, get position then spawn
        spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
        PlaceDoor(spawnPos, doorTop, bossDoorU);
        // bottom door    
        spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
        PlaceDoor(spawnPos, doorBot, bossDoorD);
        // right door
        spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
        PlaceDoor(spawnPos, doorRight, bossDoorR);
        // left door
        spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
        PlaceDoor(spawnPos, doorLeft, bossDoorL);
    }

    void MakeBossNextRoomDoors(int bossRoom)
    {
        Vector3 spawnPos;
        if (bossRoom != 1)
        {
            spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
            PlaceDoor(spawnPos, doorTop, doorU);
        }
        if (bossRoom != 3)
        {
            spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
            PlaceDoor(spawnPos, doorBot, doorD);
        }
        if (bossRoom != 4)
        {
            spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
            PlaceDoor(spawnPos, doorRight, doorR);
        }
        if (bossRoom != 2)
        {
            spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
            PlaceDoor(spawnPos, doorLeft, doorL);
        }
    }
    void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn)
    {
        // x축 방향 -0.5, y축 방향 0.5 처리
        spawnPos.x -= 0.5f;
        spawnPos.y += 0.5f;
        // check whether its a door or wall, then spawn
        if (door)
        {
            Instantiate(doorSpawn, spawnPos, Quaternion.identity).transform.parent = transform;
        }
        else
        {
            Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = transform;
        }
    }

    void GenerateRoomTiles()
    {
        // loop through every pixel of the texture
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = tex.GetPixel(x, y);
        // skip clear spaces in texture
        if (pixelColor.a == 0)
        {
            return;
        }
        // find the color to match the pixel
        foreach (ColorToGameObject mapping in mappings)
        {
            if (mapping.color.Equals(pixelColor))
            {
                Vector3 spawnPos = positionFromTileGrid(x, y);
                Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform;
            }
        }
        if (type == 1)
        {
            tex = startRoomTexture;
        }
        if (type == 2)
        {
            tex = bossRoomTexture;
        }
    }

    Vector3 positionFromTileGrid(int x, int y)
    {
        Vector3 ret;
        // find difference between the corner of the texture and the center of this object
        Vector3 offset = new Vector3((-roomSizeInTiles.x + 1) * tileSize, (roomSizeInTiles.y / 4) * tileSize - (tileSize / 4), 0);
        // find scaled up position at the offset
        // x축 방향 -0.5, y축 방향 0.5 처리
        ret = new Vector3(tileSize * x - 0.5f, -tileSize * y + 0.5f, 0) + offset + transform.position;
        return ret;
    }

    void Update()
    {
        // 방 타입이 2이고 몬스터가 전부 없어진 경우 다음 스테이지로 이동
        if (type == 1 && monsterManager != null && monsterManager.getMonsterCount() == 0)
        {
            if (stageTargetInstance == null)
            {
                // stage_target 프리팹 생성
                stageTargetInstance = Instantiate(stageTargetPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void LoadNextStage()
    {
        StartCoroutine(LoadStageCoroutine());
    }

    private IEnumerator LoadStageCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextStage);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Player.instance.transform.position = GetStartPositionInNextStage();
    }

    private Vector3 GetStartPositionInNextStage()
    {
        RoomInstance[] rooms = FindObjectsOfType<RoomInstance>();
        foreach (RoomInstance room in rooms)
        {
            if (room.type == 1)
            {
                return room.transform.position;
            }
        }
        // 만약 type 1 방을 찾지 못하면 기본 위치 반환 (필요에 따라 조정)
        return new Vector3(0, 0, 0);
    }
}