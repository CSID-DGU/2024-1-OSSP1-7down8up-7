using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour
{
    public Texture2D tex; // 방의 텍스처
    public Texture2D startRoomTexture;
    public Texture2D bossRoomTexture;
    public Vector2 gridPos;
    public int type; // 0: normal, 1: enter, 2: boss
    public bool doorTop, doorBot, doorLeft, doorRight;
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

    public void Setup(Texture2D _tex = null, Vector2 _gridPos = default(Vector2), int _type = 0, bool _doorTop = false, bool _doorBot = false, bool _doorLeft = false, bool _doorRight = false)
    {
        tex = _tex;
        gridPos = _gridPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        MakeDoors();
        GenerateRoomTiles();
    }

    void MakeDoors()
    {
        if (type == 2 || IsNextToBossRoom() == 1)
        {
            // top door, get position then spawn
            Vector3 spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
            PlaceDoor(spawnPos, doorTop, bossDoorU);
        }
        if (type == 2 || IsNextToBossRoom() == 3)
        {
            // bottom door    
            Vector3 spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
            PlaceDoor(spawnPos, doorBot, bossDoorD);
        }
        if (type == 2 || IsNextToBossRoom() == 4)
        {
            // right door
            Vector3 spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
            PlaceDoor(spawnPos, doorRight, bossDoorR);
        }
        if (type == 2 || IsNextToBossRoom() == 2)
        {
            // left door
            Vector3 spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
            PlaceDoor(spawnPos, doorLeft, bossDoorL);
        }
        else if (type == 0 || type == 1 || IsNextToBossRoom() == 0)
        {
            // top door, get position then spawn
            Vector3 spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
            PlaceDoor(spawnPos, doorTop, doorU);
            // bottom door    
            spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
            PlaceDoor(spawnPos, doorBot, doorD);
            // right door
            spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
            PlaceDoor(spawnPos, doorRight, doorR);
            // left door
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

    int IsNextToBossRoom()
    {
        // check surrounding rooms to see if any of them is a boss room
        RoomInstance topRoom = FindRoomAtPosition(gridPos + Vector2.up);
        RoomInstance bottomRoom = FindRoomAtPosition(gridPos + Vector2.down);
        RoomInstance leftRoom = FindRoomAtPosition(gridPos + Vector2.left);
        RoomInstance rightRoom = FindRoomAtPosition(gridPos + Vector2.right);

        if (doorTop && topRoom != null && topRoom.type == 2)
        {
            return 1;
        }

        else if (doorBot && bottomRoom != null && bottomRoom.type == 2)
        {
            return 3;
        }
        else if (doorLeft && leftRoom != null && leftRoom.type == 2)
        {
            return 2;
        }
        else if (doorRight && rightRoom != null && rightRoom.type == 2)
        {
            return 4;
        }
        return 0;
    }

    RoomInstance FindRoomAtPosition(Vector2 position)
    {
        // find the room at the specified grid position
        RoomInstance[] rooms = FindObjectsOfType<RoomInstance>();
        foreach (RoomInstance room in rooms)
        {
            if (room.gridPos == position)
            {
                return room;
            }
        }
        return null;
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
}