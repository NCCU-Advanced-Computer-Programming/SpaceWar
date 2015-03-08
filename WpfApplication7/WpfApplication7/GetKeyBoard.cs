using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication7
{
    //紀錄鍵盤事件
    class GetKeyBoard
    {
        public bool R = false, D = false, F = false, G = false, Up = false, Down = false, Left = false, Right = false;
        public string[,] MoveKey = new string[,] { { "", "", "", "","","","","" }, { "", "", "", "","","","","" } };

        public void GetKeyDown(string key)
        {
            switch (key)
                {
                    //玩家一移動
                    case "R":
                    case "D":
                    case "F":
                    case "G":
                        MoveState(0, key);
                        Game_data.instance.Move_player[0] = key;
                        break;
                    //玩家一炸彈
                    case "Q":
                        Game_data.instance.IsBomb_player[0] = true;
                        break;
                    //玩家一技能
                    case "W":
                        Game_data.instance.IsSkill_player[0] = true;
                        break;
                    //玩家二移動
                    case "Up":
                    case "Down":
                    case "Left":
                    case "Right":
                        MoveState(1, key);
                        Game_data.instance.Move_player[1] = key;
                        break;
                    //玩家二炸彈
                    case "O":
                        Game_data.instance.IsBomb_player[1] = true;
                        break;
                    //玩家二技能
                    case "P":
                        Game_data.instance.IsSkill_player[1] = true;
                        break;
                    default:
                        break;
                }

        }
        public void GetKeyUp(string key)
        {
            switch (key)
            {
                //玩家一移動
                case "R":
                case "D":
                case "F":
                case "G":
                    StopState(0, key);
                    if (Game_data.instance.Move_player[0] == key)
                    {
                        Game_data.instance.Move_player[0] = "";
                        if (CheckTrue(0) > 0) {
                            StillMove(0);
                        }
                    }
                    break;
                //玩家一炸彈
                case "Q":
                    Game_data.instance.IsBomb_player[0] = false;
                    break;
                //玩家一技能
                case "W":
                    Game_data.instance.IsSkill_player[0] = false;
                    break;
                //玩家二移動
                case "Up":
                case "Down":
                case "Left":
                case "Right":
                    StopState(1, key);
                    if (Game_data.instance.Move_player[1] == key)
                    {
                        Game_data.instance.Move_player[1] = "";
                        if (CheckTrue(1) > 0) {
                            StillMove(1);
                        }
                    }
                    break;
                //玩家二炸彈
                case "O":
                    Game_data.instance.IsBomb_player[1] = false;
                    break;
                //玩家二技能
                case "P":
                    Game_data.instance.IsSkill_player[1] = false;
                    break;
                default:
                    break;
            }
        }

        public void MoveState(int number, string key)
        {
            bool AlreadyTrue = false;
            if(number==0)
            {
                switch(key)
                {
                    case "R":
                        if (!R)
                        {
                            R = true;
                            AlreadyTrue = true;
                        }
                        break;
                    case "D":
                        if (!D)
                        {
                            D = true;
                            AlreadyTrue = true;
                        }
                        break;
                    case "F":
                        if (!F)
                        {
                            F = true;
                            AlreadyTrue = true;
                        }
                        break;
                    case "G":
                        if (!G)
                        {
                            G = true;
                            AlreadyTrue = true;
                        }
                        break;
                }
            }
            else if(number==1)
            {
                switch (key)
                {
                    case "Up":
                        if (!Up)
                        {
                            Up = true;
                            AlreadyTrue = true;
                        }
                        break;
                    case "Down":
                        if (!Down)
                        {
                            Down = true;
                            AlreadyTrue = true;
                        }
                        break;
                    case "Right":
                        if (!Right)
                        {
                            Right = true;
                            AlreadyTrue = true;
                        }
                        break;
                    case "Left":
                        if (!Left)
                        {
                            Left = true;
                            AlreadyTrue = true;
                        }
                        break;
                }
            }

            if(AlreadyTrue)
            {
                //紀錄步驟
                for (int i = 7; i > 0; i--)
                {
                    MoveKey[number, i] = MoveKey[number, i - 1];
                }
                MoveKey[number, 0] = key;
            }
        }
        public void StopState(int number, string key)
        {
            if (number == 0)
            {
                switch (key)
                {
                    case "R":
                        R = false;
                        break;
                    case "D":
                        D = false;
                        break;
                    case "F":
                        F = false;
                        break;
                    case "G":
                        G = false;
                        break;
                }
            }
            else if (number == 1)
            {
                switch (key)
                {
                    case "Up":
                        Up = false;
                        break;
                    case "Down":
                        Down = false;
                        break;
                    case "Right":
                        Right = false;
                        break;
                    case "Left":
                        Left = false;
                        break;
                }
            }
        }

        //看還有沒有按著的鍵
        public int CheckTrue(int number)
        {
            int check = 0;
            if (number == 0)
            {
                if (R) check++;
                if (D) check++;
                if (F) check++;
                if (G) check++;
            }
            else if (number == 1)
            {
                if (Up) check++;
                if (Down) check++;
                if (Right) check++;
                if (Left) check++;
            }
            return check;
        }

        //鍵盤上還有按著的要繼續移動
        public void StillMove(int number)
        {
            //看之前按的還是不是TRUE 是的話移動變成這個
            if (number == 0)
            {
                for (int i = 1; i < 8; i++)
                {
                    string TrueKey = "";
                    switch (MoveKey[number, i])
                    {
                        case "R":
                            if (R)
                                TrueKey = "R";
                            break;
                        case "D":
                            if (D)
                                TrueKey = "D";
                            break;
                        case "F":
                            if (F)
                                TrueKey = "F";
                            break;
                        case "G":
                            if (G)
                                TrueKey = "G";
                            break;
                    }
                    if(TrueKey!="")
                    {
                        Game_data.instance.Move_player[0] = TrueKey;
                        break;
                    }
                }
            }
            else if (number == 1)
            {
                for (int i = 1; i < 8; i++)
                {
                    string TrueKey = "";
                    switch (MoveKey[number, i])
                    {
                        case "Up":
                            if (Up)
                                TrueKey = "Up";
                            break;
                        case "Down":
                            if (Down)
                                TrueKey = "Down";
                            break;
                        case "Right":
                            if (Right)
                                TrueKey = "Right";
                            break;
                        case "Left":
                            if (Left)
                                TrueKey = "Left";
                            break;
                    }
                    if(TrueKey!="")
                    {
                        Game_data.instance.Move_player[1] = TrueKey;
                        break;
                    }
                }
            }
            
        }

    }
}
