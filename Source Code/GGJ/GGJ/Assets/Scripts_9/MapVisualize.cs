using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.Timeline;

public class MapVisualize : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public GameObject nodePrefab;

    public int layerCount = 15;
    public float height = 15;
    public float heightRandom = 1.0f;
    public float layerWidth = 15;
    public float layerWidthRandom = 3.0f;
    public float widthbias = -4.0f;

    public float h_scale = 1.0f;
    public float w_scale = 1.0f;

    public float line_width = 0.1f;
    public float Node_scale = 0.5f;
    public float Over_scale = 1.5f;

    public float scroll_threshold = 0.1f;
    public float scroll_speed = 0.1f;

    public int [] layerCountWeight = new int[] { 0, 3 , 8 , 6 , 3};
    public string[] RoomTypes = new string[] {"Start", "Fight", "Elite", "Shop", "Rest" ,"Event", "Treasure" ,"Boss" };
    public int[] RoomWeight = new int[] { 0, 7, 2, 1, 1, 3, 1, 0 };
    public int[] TreasureLayer = new int[] { 6, 12 };

    public List<List<GameObject>> nodes = new();
    public List<GameObject> lines = new();

    public static Vector2Int PlayerPosition = new(0, 0);
    public static int seed = -1;
    public static bool needinit = true;
    public static float Posx = 0;

    void Start()
    {
        if (needinit) Reset();
        else MapProcess();
        transform.position = new Vector3(Posx, 0, 0);
    }

    public void MapProcess()
    {
        mapGenerator = new MapGenerator();
        if (seed == -1)
            seed = Time.time.GetHashCode();
        mapGenerator.SetSeed(seed);
        mapGenerator.layerCount = layerCount;
        mapGenerator.layerCountWeight = layerCountWeight;
        mapGenerator.RoomTypes = RoomTypes;
        mapGenerator.RoomWeight = RoomWeight;
        mapGenerator.TreasureLayer = TreasureLayer;
        mapGenerator.GenerateMap();
        VisualizeMap();
        StateUpdate();
    }

    void Update()
    {
        if (Input.mousePosition.x < Screen.width * scroll_threshold && transform.position.x < 0)
            {
                transform.position += Math.Clamp((Screen.width*scroll_threshold-Input.mousePosition.x)/(Screen.width*scroll_threshold),0,1.0f) * scroll_speed * new Vector3(1, 0, 0);
            }
        if (Input.mousePosition.x > Screen.width * (1 - scroll_threshold) &&transform.position.x > -layerWidth * layerCount * w_scale )
            {
                transform.position -= Math.Clamp((Input.mousePosition.x-Screen.width*(1-scroll_threshold))/(Screen.width*scroll_threshold),0,1.0f) * scroll_speed * new Vector3(1, 0, 0);
            }
        Posx = transform.position.x;
    }

    public void Reset(){
        seed = -1;
        PlayerPosition = new Vector2Int(0, 0);
        MapProcess();
        needinit = false;
    }

    public void VisualizeMap()
    {
        if (nodes != null)
            foreach (List<GameObject> nodet in nodes)
                foreach (GameObject node in nodet)
                    Destroy(node);
        if (lines != null)
            foreach (GameObject line in lines)
                Destroy(line);
        
        for (int i = 0; i < mapGenerator.map.Count; i++)
        {
            List<GameObject> layer = new();
            for (int j = 0; j < mapGenerator.map[i].Count; j++)
            {
                GameObject node = Instantiate(nodePrefab,GetNodePosition(i,j), Quaternion.identity);
                node.transform.localScale = new Vector3(Node_scale, Node_scale, 1);
                node.GetComponent<MapNode>().OverScale = Over_scale;
                // node.GetComponent<MapNode>().ShowText(mapGenerator.map[i][j].type);
                node.GetComponent<MapNode>().SetType(mapGenerator.map[i][j].type);
                node.transform.SetParent(transform);
                node.GetComponent<MapNode>().layer = i;
                node.GetComponent<MapNode>().index = j;
                foreach(MapRoom nextRoom in mapGenerator.map[i][j].nextRooms)
                {
                    GameObject linet = new();
                    
                    LineRenderer line = linet.AddComponent<LineRenderer>();
                    linet.transform.SetParent(transform);
                    
                    line.SetPosition(0, GetNodePosition(i, j));
                    line.SetPosition(1, GetNodePosition(nextRoom.layer, nextRoom.index));
                    line.startWidth = line_width;
                    line.endWidth = line_width;
                    line.useWorldSpace = false;
                    lines.Add(linet);
                }
                layer.Add(node);
            }
            nodes.Add(layer);
        }
        StateUpdate();

    }

    public void StateUpdate(){
        SetNodeStateTree(0, 0, 0);
        SetNodeStateTree(PlayerPosition.x, PlayerPosition.y, 1);
        SetNodeState(PlayerPosition.x, PlayerPosition.y, 2);
        foreach (MapRoom nextRoom in mapGenerator.map[PlayerPosition.x][PlayerPosition.y].nextRooms)
        {
            SetNodeState(nextRoom.layer, nextRoom.index, 3);
        }
    }

    public Vector3 GetNodePosition(int layer, int index)
    {
        int layerCount = mapGenerator.map[layer].Count;
        UnityEngine.Random.InitState(mapGenerator.seed*16384+layer*256+index);
        float hdiv = height / (layerCount+1);
        if (layerCount % 2 == 1){
            return new Vector3((layer * layerWidth + UnityEngine.Random.Range(-layerWidthRandom, layerWidthRandom))*w_scale + widthbias, ((index - layerCount / 2) * hdiv + UnityEngine.Random.Range(-heightRandom, heightRandom))*h_scale, 0);
        }
        else{
            return new Vector3((layer * layerWidth + UnityEngine.Random.Range(-layerWidthRandom, layerWidthRandom))*w_scale + widthbias, ((index - layerCount / 2 + 0.5f) * hdiv + UnityEngine.Random.Range(-heightRandom, heightRandom))*h_scale, 0);
        }
    }

    public void SetNodeState(int layer, int index, int state)
    {
        nodes[layer][index].GetComponent<MapNode>().SetState(state);
    }

    public void SetNodeStateTree(int layer, int index, int state)
    {
        nodes[layer][index].GetComponent<MapNode>().SetState(state);
        foreach (MapRoom nextRoom in mapGenerator.map[layer][index].nextRooms)
        {
            SetNodeStateTree(nextRoom.layer, nextRoom.index, state);
        }
    }

    public void UpdatePlayerPosition(int layer, int index)
    {
        PlayerPosition = new Vector2Int(layer, index);
        string type = mapGenerator.map[layer][index].type;
        StateUpdate();
        TypeBehaviour(type);
    }

    public void TypeBehaviour(string type){
        int typeid = Array.IndexOf(RoomTypes, type);
        switch(typeid){
            case 0:
                break;
            case 1:
                SceneControllerManager.Instance.FadeAndLoadScene("GameScene1");
                break;
            case 2:
                SceneControllerManager.Instance.FadeAndLoadScene("GameScene2");
                break;
            case 3:
                SceneControllerManager.Instance.FadeAndLoadScene("Shop");
                break;
            case 4:
                PlayerProperties.HP = PlayerProperties.BaseHP;
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                SceneControllerManager.Instance.FadeAndLoadScene("GameScene3");
                break;
        }
    }
}
