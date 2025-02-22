using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapRoom
    {
        public string type;
        public int layer;
        public int index;
        public List<MapRoom> nextRooms;
    }

public class MapGenerator
{
    public int layerCount = 15;
    public int [] layerCountWeight = new int[] { 0, 3 , 8 , 6 , 3};

    public string[] RoomTypes = new string[] {"Start", "Fight", "Elite", "Shop", "Rest" ,"Event", "Treasure" ,"Boss" };
    public int[] RoomWeight = new int[] { 0, 7, 2, 1, 1, 3, 1, 0 };
    public int[] TreasureLayer = new int[] { 6, 12 };

    public List<List<MapRoom>> map;
    public int seed;

    public void SetSeed(int seed)
    {
        this.seed = seed;
        Random.InitState(seed);
    }

    public void GenerateMap()
    {
        //地图包含起点终点共layerCount+2层
        //每层包含房间数目为RoomCountMin到RoomCountMax之间
        //每个房间链接下一层的房间数目为RoadCountMin到RoadCountMax之间，从最左边开始
        //同层下一个房间链接下一层从上一个房间链接的最后一个下一层房间开始
        //每个房间包含一个房间类型，房间类型包括Start, Fight, Elite, Shop, Rest, Event, Treasure, Boss
    
        Random.InitState(seed);
        map = new List<List<MapRoom>>();
    
        for (int i = 0; i < layerCount + 2; i++)
        {
            int roomCount;
            if (i == 0)
                roomCount = 1;
            else if (i == layerCount + 1)
                roomCount = 1;
            else {
                roomCount = Random.Range(0, layerCountWeight.Sum());
                for (int j = 0; j < layerCountWeight.Length; j++)
                {
                    if (roomCount < layerCountWeight[j])
                    {
                        roomCount = j;
                        break;
                    }
                    else
                        roomCount -= layerCountWeight[j];
                }
            }

            List<MapRoom> layer = new List<MapRoom>();
    
            for (int j = 0; j < roomCount; j++)
            {
                MapRoom room = new()
                {
                    type = RoomTypes[GetRoomType(i)],
                    layer = i,
                    index = j,
                    nextRooms = new List<MapRoom>()
                };
                layer.Add(room);
            }
    
            map.Add(layer);
        }
    
        for (int i = 0; i < map.Count - 1; i++)
        {
            List<MapRoom> currentLayer = map[i];
            List<MapRoom> nextLayer = map[i + 1];

            int divideNum;
            int [] divide;
            if (currentLayer.Count >= nextLayer.Count){
                divideNum = nextLayer.Count-1;
                divide = Enumerable.Range(1, currentLayer.Count-1).OrderBy(x => Random.value).Take(divideNum).ToArray().OrderBy(x => x).ToArray();
                for (int j = 0; j < currentLayer.Count; j++)
                {
                    for(int k = 0; k <= divideNum; k++){
                        if (k==divideNum){
                            currentLayer[j].nextRooms.Add(nextLayer[k]);
                            break;
                        }
                        if (j < divide[k]){
                            currentLayer[j].nextRooms.Add(nextLayer[k]);
                            break;
                        }
                    }
                }
            }else{
                divideNum = currentLayer.Count-1;
                divide = Enumerable.Range(1, nextLayer.Count-1).OrderBy(x => Random.value).Take(divideNum).ToArray().OrderBy(x => x).ToArray();
                for (int j = 0; j < nextLayer.Count; j++)
                {
                    for(int k = 0; k <= divideNum; k++){
                        if (k==divideNum){
                            currentLayer[k].nextRooms.Add(nextLayer[j]);
                            break;
                        }
                        if (j < divide[k]){
                            currentLayer[k].nextRooms.Add(nextLayer[j]);
                            break;
                        }
                    }
                }
            }
        }
    }

    public int GetRoomType(int layer){
        
        if (layer == 0)
            return 0;
        else if (layer == layerCount + 1)
            return 7;
        else if (TreasureLayer.Contains(layer))
            return 6;
        else
        {
            int type = Random.Range(0, RoomWeight.Sum());
            for (int i = 0; i < RoomWeight.Length; i++)
            {
                if (type < RoomWeight[i])
                    return i;
                else
                    type -= RoomWeight[i];
            }
        }
        return -1;
    }
}

