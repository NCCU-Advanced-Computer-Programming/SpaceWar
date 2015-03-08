using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//圖片相關
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication7
{

    class Player
    {
        private MainWindow _window;
        //第幾個玩家
        public int number;
        //玩家初始位置
        public float Position_Player0_X = 0;
        public float Position_Player0_Y = 0;
        public float Position_Player1_X = 1080;
        public float Position_Player1_Y = 600;

        //是否存活
        public bool _IsLife = false;
        //死亡次數
        public int _DeathCounts = 0;
        //死亡時間
        public float _DeathTime = 0;

        //位置
        public float _PositionX;
        public float _PositionY;
        //角色面對方向
        public string _PlayerView;

        //基本移動速度
        private float BasicSpeed = 4.2f;
        //角色移動速度
        public float _Speed;
        //角色最大速度
        public float MaxSpeed = 10;

        //變換的速度->撞牆用的小速度
        public float _DeltaSpeed;

        //基本炸彈威力
        private int BasicPower = 2;
        //角色炸彈威力
        public int _Power;
        //角色最大炸彈威力
        public int MaxPower = 10;

        //基本炸彈數量
        private int BasicBNumber = 1;
        //角色炸彈數量
        public int _BNumber;
        //角色現在情況下能放炸彈數量
        public int _Can_BNumber = 1;
        //角色總共最大炸彈數量
        public int MaxBNumber = 8;
        //炸彈爆炸時間
        private float bomb_wait_time = 2.5f;

        //角色基本技能數量
        private int BasicSkill = 10;
        //角色現在能放技能數量
        private int _SkillNumber;

        //角色按鍵位置
        private string GoTop;
        private string GoRight;
        private string GoDown;
        private string GoLeft;

        ////圖片路徑
        //public string TopImage = "Images/top.png";
        //public string DownImage = "Images/down.png";
        //public string LeftImage = "Images/left.png";
        //public string RightImage = "Images/right.png";
        //public string DieImage = "Images/die.png";
        //使用圖片法 imageLogo.Source = new BitmapImage(new Uri("logo.jpg", UriKind.Relative));


        public Player(MainWindow window, int player_num)
        {
            _window = window;
            number = player_num;


        }

        public void Start()
        {
            //新玩家起始設定
            _IsLife = true;
            _Speed = BasicSpeed;
            _Power = BasicPower;
            _BNumber = BasicBNumber;
            _Can_BNumber = _BNumber;
            _SkillNumber = BasicSkill;
            _PlayerView = "Down";

            //按鍵設定 座標設定
            switch (number)
            {
                case 0:
                    GoTop = "R";
                    GoDown = "F";
                    GoRight = "G";
                    GoLeft = "D";
                    _PositionX = (float)_window.player0.Margin.Left;
                    _PositionY = (float)_window.player0.Margin.Top;
                    _window.player0.Margin = new Thickness(Position_Player0_X, Position_Player0_Y, 0, 0);
                    _window.player0.Source = Game_data.instance.DownImage;
                    break;
                case 1:
                    GoTop = "Up";
                    GoDown = "Down";
                    GoRight = "Right";
                    GoLeft = "Left";
                    //TopImage = "Images/top1.png";
                    //DownImage = "Images/down1.png";
                    //LeftImage = "Images/left1.png";
                    //RightImage = "Images/right1.png";
                    _PositionX = (float)_window.player1.Margin.Left;
                    _PositionY = (float)_window.player1.Margin.Top;
                    _window.player1.Margin = new Thickness(Position_Player1_X, Position_Player1_Y, 0, 0);
                    _window.player1.Source = Game_data.instance.DownImage1;
                    break;
                default:
                    GoTop = "R";
                    GoDown = "F";
                    GoRight = "G";
                    GoLeft = "D";
                    _PositionX = (float)_window.player0.Margin.Left;
                    _PositionY = (float)_window.player0.Margin.Top;
                    break;

            }


        }

        public void Update()
        {
            //玩家每幀更新，就按鍵的判斷吧!移動、放炸彈、用技能
            if (_IsLife)//如果存活
            {
                string move = Game_data.instance.Move_player[number];
                bool bomb = Game_data.instance.IsBomb_player[number];
                bool skill = Game_data.instance.IsSkill_player[number];

                if (number == 0)
                {
                    _PositionX = (float)_window.player0.Margin.Left;
                    _PositionY = (float)_window.player0.Margin.Top;
                    //移動相關
                    if (GoTop == move)
                    {
                        _window.player0.Source = Game_data.instance.TopImage;
                        //_window.player0.Source = Game_data.instance.Laser_horizontal_left.Source;
                        if (!IsHitWall("Top", _Speed))
                        {
                            _PlayerView = "Top";
                            _window.player0.Margin = new Thickness(_PositionX, _PositionY - _Speed + _DeltaSpeed, 0, 0);
                            _DeltaSpeed = 0;
                        }
                    }
                    else if (GoDown == move)
                    {
                        _window.player0.Source = Game_data.instance.DownImage;
                        if (!IsHitWall("Down", _Speed))
                        {
                            _PlayerView = "Down";
                            _window.player0.Margin = new Thickness(_PositionX, _PositionY + _Speed - _DeltaSpeed, 0, 0);
                            _DeltaSpeed = 0;
                        }
                    }
                    else if (GoLeft == move)
                    {
                        _window.player0.Source = Game_data.instance.LeftImage;
                        if (!IsHitWall("Left", _Speed))
                        {
                            _PlayerView = "Left";
                            _window.player0.Margin = new Thickness(_PositionX - _Speed + _DeltaSpeed, _PositionY, 0, 0);
                            _DeltaSpeed = 0;
                        }
                    }
                    else if (GoRight == move)
                    {
                        _window.player0.Source = Game_data.instance.RightImage;
                        if (!IsHitWall("Right", _Speed))
                        {
                            _PlayerView = "Right";
                            _window.player0.Margin = new Thickness(_PositionX + _Speed - _DeltaSpeed, _PositionY, 0, 0);
                            _DeltaSpeed = 0;
                        }
                    }
                    /*先存一個以前的做法
                    else if (GoRight == move && !Collider.instance.Player_Obstacle(_PositionX + _Speed, _PositionY))
                    {
                        _window.player0.Margin = new Thickness(_PositionX + _Speed, _PositionY, 0, 0);
                    }
                     */
                    //放炸彈
                    if (bomb && _Can_BNumber > 0)
                    {
                        Game_data.instance.IsBomb_player[number] = false; //關掉炸彈的開關
                        int[] pos = Tools.instance.getPos(_PositionX, _PositionY); //算炸彈位置
                        int[] pos_map = Tools.instance.getPos_map(_PositionX, _PositionY);
                        Bomb newBomb = new Bomb(_window, bomb_wait_time, _Power, pos[0], pos[1], number, pos_map, Game_data.instance.worldTime);
                        _Can_BNumber--;//少一炸彈
                        Game_data.instance.Bomb_list.Add(newBomb);
                    }

                    if (skill && _SkillNumber > 0)
                    {
                        Game_data.instance.IsSkill_player[number] = false; //關掉技能的開關
                        int[] player_map = Tools.instance.getPos_map(_PositionX, _PositionY);
                        int[] next_map = GetNext_map(player_map[0], player_map[1]);
                        //MessageBox.Show(player_map[0] + " " + player_map[1]+"  num="+_SkillNumber);
                        //當下個格子是空格和沒有別人
                        if (Game_data.instance._map[next_map[1], next_map[0]] == 0 && !Collider.instance.Player_forward(next_map[0], next_map[1], number))
                        {
                            Game_data.instance._map[next_map[1], next_map[0]] = 2;
                            _window.player0.Margin = new Thickness(player_map[0] * 60, player_map[1] * 60, 0, 0);
                            _SkillNumber--;//少一技能
                        }
                        //放不了技能
                        else
                        {

                        }
                    }
                    //吃物品
                    Collider.instance.Player_item(this);

                }
                else if (number == 1)
                {
                    _PositionX = (float)_window.player1.Margin.Left;
                    _PositionY = (float)_window.player1.Margin.Top;
                    //移動相關
                    if (GoTop == move)
                    {
                        _window.player1.Source = Game_data.instance.TopImage1;
                        if (!IsHitWall("Top", _Speed))
                        {
                            _PlayerView = "Top";
                            _window.player1.Margin = new Thickness(_PositionX, _PositionY - _Speed + _DeltaSpeed, 0, 0);
                            _DeltaSpeed = 0;
                        }
                    }
                    else if (GoDown == move)
                    {
                        _window.player1.Source = Game_data.instance.DownImage1;
                        if (!IsHitWall("Down", _Speed))
                        {
                            _PlayerView = "Down";
                            _window.player1.Margin = new Thickness(_PositionX, _PositionY + _Speed - _DeltaSpeed, 0, 0);
                            _DeltaSpeed = 0;
                        }
                    }
                    else if (GoLeft == move)
                    {
                        _window.player1.Source = Game_data.instance.LeftImage1;
                        if (!IsHitWall("Left", _Speed))
                        {
                            _PlayerView = "Left";
                            _window.player1.Margin = new Thickness(_PositionX - _Speed + _DeltaSpeed, _PositionY, 0, 0);
                            _DeltaSpeed = 0;
                        }
                    }
                    else if (GoRight == move)
                    {
                        _window.player1.Source = Game_data.instance.RightImage1;
                        if (!IsHitWall("Right", _Speed))
                        {
                            _PlayerView = "Right";
                            _window.player1.Margin = new Thickness(_PositionX + _Speed - _DeltaSpeed, _PositionY, 0, 0);
                            _DeltaSpeed = 0;
                        }
                    }
                    /*先存一個以前的做法
                    else if (GoRight == move && !Collider.instance.Player_Obstacle(_PositionX + _Speed, _PositionY))
                    {
                        _window.player0.Margin = new Thickness(_PositionX + _Speed, _PositionY, 0, 0);
                    }
                     */
                    //放炸彈
                    if (bomb && _Can_BNumber > 0)
                    {
                        Game_data.instance.IsBomb_player[number] = false; //關掉炸彈的開關
                        int[] pos = Tools.instance.getPos(_PositionX, _PositionY); //算炸彈位置
                        int[] pos_map = Tools.instance.getPos_map(_PositionX, _PositionY);
                        Bomb newBomb = new Bomb(_window, bomb_wait_time, _Power, pos[0], pos[1], number, pos_map, Game_data.instance.worldTime);
                        _Can_BNumber--;//少一炸彈
                        Game_data.instance.Bomb_list.Add(newBomb);
                    }

                    if (skill && _SkillNumber > 0)
                    {
                        Game_data.instance.IsSkill_player[number] = false; //關掉技能的開關
                        int[] player_map = Tools.instance.getPos_map(_PositionX, _PositionY);
                        int[] next_map = GetNext_map(player_map[0], player_map[1]);
                        //當下個格子是空格和沒有別人
                        if (Game_data.instance._map[next_map[1], next_map[0]] == 0 && !Collider.instance.Player_forward(next_map[0], next_map[1], number))
                        {
                            Game_data.instance._map[next_map[1], next_map[0]] = 2;
                            _window.player1.Margin = new Thickness(player_map[0] * 60, player_map[1] * 60, 0, 0);
                            _SkillNumber--;//少一技能
                        }
                        //放不了技能
                        else
                        {

                        }
                    }

                    //吃物品
                    Collider.instance.Player_item(this);
                }
            }//islife=true

            else
            {
                Relive();
            }

        }

        public void Hit()
        {
            //玩家被衝擊波打到了QQ
            _IsLife = false;
            _DeathCounts++;
            _DeathTime = Game_data.instance.worldTime;

            if (number == 0)
            {
                _window.player0.Source = Game_data.instance.DieImage;
                //殺死敵方 我方主堡生命++
                Game_data.instance.MainTower_list[1].life++;
            }
            else if (number == 1)
            {
                _window.player1.Source = Game_data.instance.DieImage;
                //殺死敵方 我方主堡生命++
                Game_data.instance.MainTower_list[0].life++;
            }

        }

        public void Relive()
        {
            //復活所需時間
            float LiveNeedTime = 0.5f + (_DeathCounts * 0.5f);
            if (Game_data.instance.worldTime - _DeathTime >= LiveNeedTime)
            {
                if (number == 0)
                {
                    _window.player0.Margin = new Thickness(Position_Player0_X, Position_Player0_Y, 0, 0);
                    _window.player0.Source = Game_data.instance.DownImage;
                    _PositionX = (float)_window.player0.Margin.Left;
                    _PositionY = (float)_window.player0.Margin.Top;
                }
                else if (number == 1)
                {
                    _window.player1.Margin = new Thickness(Position_Player1_X, Position_Player1_Y, 0, 0);
                    _window.player1.Source = Game_data.instance.DownImage1;
                    _PositionX = (float)_window.player1.Margin.Left;
                    _PositionY = (float)_window.player1.Margin.Top;
                }
                _IsLife = true;
            }
        }

        //判斷是否撞牆-->會撞牆就丟更小的範圍看會不會撞-->直到離牆只有0.多的距離就回傳true
        public bool IsHitWall(string GoToWhere, float CalSpeed)
        {
            if (CalSpeed > 1.01)
            {
                float TopSpeed = 0;
                float DownSpeed = 0;
                float RightSpeed = 0;
                float LeftSpeed = 0;
                switch (GoToWhere)
                {
                    case "Top":
                        TopSpeed = CalSpeed;
                        break;
                    case "Down":
                        DownSpeed = CalSpeed;
                        break;
                    case "Right":
                        RightSpeed = CalSpeed;
                        break;
                    case "Left":
                        LeftSpeed = CalSpeed;
                        break;
                }

                //人跟障礙物、人跟炸彈、人跟邊界
                if (Collider.instance.Player_Obstacle(_PositionX - LeftSpeed + RightSpeed, _PositionY - TopSpeed + DownSpeed) ||
                    Collider.instance.Player_bomb(_PositionX - LeftSpeed + RightSpeed, _PositionY - TopSpeed + DownSpeed, number) ||
                    (_PositionX - LeftSpeed + RightSpeed) < 0 || (_PositionY - TopSpeed + DownSpeed) < 0 ||
                    (_PositionX - LeftSpeed + RightSpeed) > 1080 || (_PositionY - TopSpeed + DownSpeed) > 600)
                {
                    CalSpeed--;
                    //IsHitWall(GoToWhere, CalSpeed);
                    if (!IsHitWall(GoToWhere, CalSpeed))
                    {
                        return false;
                    }
                }
                else
                {
                    _DeltaSpeed = _Speed - CalSpeed;
                    return false;
                }
                return true;
            }
            _DeltaSpeed = 0;
            return true;
        }

        //得到前方位置
        public int[] GetNext_map(int x, int y)
        {
            switch (_PlayerView)
            {
                case "Top":
                    y--;
                    break;
                case "Down":
                    y++;
                    break;
                case "Left":
                    x--;
                    break;
                case "Right":
                    x++;
                    break;
                default:
                    y++;
                    break;
            }
            int posX = x, posY = y;

            int[] posnext = { posX, posY };
            return posnext;
        }
    }
}