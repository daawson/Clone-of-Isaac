using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Dungeon
    {
        #region #MAPTYPES
        // 60 = kamien

        int[,] room00 =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };

        int[,] room0 =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3},
            {8,60,0,0,0,0,0,0,0,0,0,0,0,0,60,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,60,0,0,0,0,0,0,0,0,0,0,0,0,60,4},
            {7,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5}

        };

        int[,] room1 =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,60,60,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,60,60,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {7,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5}

        };

        int[,] room2 =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,60,60,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,60,60,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {7,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5}

        };

        int[,] room3 =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,60,60,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,60,60,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {7,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5}

        };
        #endregion

        #region FIELDS
        bool jestemBogiem = false;
        bool jestemUbogiem = false;
        int maxRooms = 5;
        int roomID = 0;
        public Room[] room { get; set; } = new Room[5];

        #endregion

        #region METHODS
        /// <summary>
        /// Tworzy nowy dungeon
        /// </summary>
        public Dungeon()
        {
            //room = new Room[maxRooms];

            StartGen();
            //room[1].GenerateDoors(emptyRoom, emptyRoom, room[0], emptyRoom);
            //room[2].GenerateDoors(emptyRoom, emptyRoom, emptyRoom, room[0]);
            //room[3].GenerateDoors(emptyRoom, room[0], emptyRoom, emptyRoom);          
            
        }
        /// <summary>
        /// Generuje pokoje
        /// </summary>
        void StartGen()
        {
            for (int i = 0; i < maxRooms; i++)
            {
                if (!jestemUbogiem) // pusty pokój reprezentujący brak drzwi
                {
                    room[i] = new Room();
                    room[i].isCleared = true;
                    room[i].isEmpty = true;
                    room[i].GenerateLevel(room00);
                    jestemUbogiem = true;
                }
                else if (!jestemBogiem) // pokój startowy
                {
                    room[i] = new Room();
                    room[i].isStarted = true;
                    room[i].isCleared = true;
                    room[i].GenerateLevel(room0);
                    jestemBogiem = true;
                }
                else // inne rodzaje
                {
                    int rand = Rand.Int(1, 3);
                    if (rand == 1)
                    {
                        room[i] = new Room();
                        room[i].isStarted = false;
                        room[i].GenerateLevel(room1);
                    }
                    else if (rand == 2)
                    {
                        room[i] = new Room();
                        room[i].isStarted = false;
                        room[i].GenerateLevel(room2);
                        
                    }
                    else if (rand == 3)
                    {
                        room[i] = new Room();
                        room[i].isStarted = false;
                        room[i].GenerateLevel(room3);
                    }
                }
                room[i].roomID = roomID;
                roomID++;
            }

            // tworzy wszystkie drzwi
            room[1].GenerateDoors(room[2], room[3], room[0], room[4]);
            room[2].GenerateDoors(room[0], room[0], room[1], room[0]);
            room[3].GenerateDoors(room[0], room[0], room[0], room[1]);
            room[4].GenerateDoors(room[0], room[1], room[0], room[0]);

            for(int r = 0; r < room.Length; r++) // dodaje pokoje do gry
            {
                GameHandler.gameScene.Add(room[r]);
            }

            GameHandler.FirstStart = false; // pierwszy start = false
           
        }
        #endregion
    }
}
