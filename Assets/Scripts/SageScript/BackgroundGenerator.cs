using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundGenerator : MonoBehaviour
{

    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    private Player player;
    private float roomWidth;
    private float screenWidthInPoints;



    // Use this for initialization
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        roomWidth = availableRooms[0].transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        GenerateRoomIfRequred();
    }


    void AddRoom(float farthestRoomEndX)
    {
        int randomRoomIndex = Random.Range(0, availableRooms.Length);

        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);


        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;

        room.transform.position = new Vector3(roomCenter, 0.5f, 11.7f);
        room.transform.parent = transform;
        currentRooms.Add(room);
    }

    void GenerateRoomIfRequred()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = player.transform.position.x;
        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;
        float farthestRoomEndX = player.transform.position.x - 10;

        foreach (var room in currentRooms)
        {
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;

            if (roomStartX > addRoomX)
                addRooms = false;


            if (roomEndX < removeRoomX)
                roomsToRemove.Add(room);

            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);

        }

        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }

        if (addRooms)
            AddRoom(farthestRoomEndX);
    }
}

