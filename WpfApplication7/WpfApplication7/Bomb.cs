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
    class Bomb
    {
        private MainWindow _window;

        //放置時是否有玩家踩在炸彈上，對應的陣列位置代表對應的玩家number
        public bool[] player_on;

        public bool _Is_alive = false;
        //幾秒後爆炸
        private float _waitTime;
        //威力
        private int _power;
        //所在格子
        private float _positionX;
        private float _positionY;
        public int[] _position;
        //第幾個玩家的炸彈
        private int _player;
        //炸彈圖片
        private Image myImage;
        //放炸彈當下的世界時間
        float _worldTime;
        float worldDeltaTime; //世界時間與傳入時間的間格

        //上次爆炸時間
        float _LastBoomTime = 0;

        //隨機物品
        private int random_item; 

        public Bomb(MainWindow window, float waitTime, int power, float positionX, float positionY, int owner, int[] position , float worldTime)
        {
            //炸彈存在
            _Is_alive = true;
            player_on = new bool[] { false, false };

            _window = window;
            _waitTime = waitTime;
            _power = power;
            _positionX = positionX;
            _positionY = positionY;
            _position = position;
            _player = owner;
            _worldTime = worldTime;

            //產生炸彈圖片
            myImage = new Image();
            myImage.Stretch = Stretch.Fill;
            myImage.Source = Game_data.instance.bomber;
            myImage.Width = 60;
            myImage.Height = 60;
            //設定位置
            myImage.Margin = new Thickness(_positionX, _positionY, 0, 0);
            _window.mainGrid.Children.Add(myImage);


            //在放置時判斷有哪些玩家踩在上面
            foreach (Player target in Game_data.instance.Player_list)
            {
                if (Tools.instance.AABBtest(target._PositionX + 10, target._PositionY + 10, target._PositionX + 50, target._PositionY + 50, _position[0] * 60 + 10, _position[1] * 60 + 10, _position[0] * 60 + 50, _position[1] * 60 + 50))
                {
                    player_on[target.number] = true;
                }
            }
        }

        public void Update()
        {
            if (_Is_alive)
            {
                //傳入時間與世界時間的相隔
                worldDeltaTime = Game_data.instance.worldTime - _worldTime;
                ////每次-十分之一秒
                //_waitTime = _waitTime - 0.02f;

                //waitTime倒數
                if (_waitTime <= worldDeltaTime)
                {
                    _waitTime = 1000;
                    Explosion();
                }
                else if (Collider.instance.Bomb_hit(_position[0], _position[1]))
                {
                    Explosion();
                }

                //判斷踩在炸彈上的玩家離開沒
                for (int i = 0; i < player_on.Length; i++)
                {
                    if (player_on[i])
                    {
                        float x = Game_data.instance.Player_list[i]._PositionX;
                        float y = Game_data.instance.Player_list[i]._PositionY;
                        if (!Tools.instance.AABBtest(x + 10, y + 10, x + 50, y + 50, _position[0] * 60 + 10, _position[1] * 60 + 10, _position[0] * 60 + 50, _position[1] * 60 + 50))
                        {
                            player_on[i] = false;
                        }
                    }
                }
            }

            //判斷上次爆炸時間有沒有超過0.02秒
            if (Game_data.instance.worldTime - _LastBoomTime > 0.02f)
            {
                //把不能炸的空白變成可以炸彈
                for (int row = 0; row < 13; row++)
                {
                    for (int col = 0; col < 19; col++)
                    {
                        if (Game_data.instance._map[row, col] == 8)
                        {
                            Game_data.instance._map[row, col] = 0;
                        }

                    }
                }
            }
        }

        public void Explosion()
        {
            //爆炸，產生衝擊波
            bool stop = false;
            int lenght_up = _power;
            int lenght_down = _power;
            int lenght_left = _power;
            int lenght_right = _power;
            //修正上衝擊波長度
            for (int i = 1; i <= _power; i++)
            {
                int num_y = _position[1] - i;
                if (num_y >= 0 && !stop)
                {
                    if (!check_map(_position[0], num_y))
                    {
                        lenght_up = i - 1;
                        stop = true;
                    }
                    if (num_y == 0 && !stop)
                    {
                        lenght_up = i;
                        stop = true;
                    }
                }
            }
            //修正下衝擊波長度
            stop = false;
            for (int i = 1; i <= _power; i++)
            {
                int num_y = _position[1] + i;
                if (num_y <= 12 && !stop)
                {
                    if (!check_map(_position[0], num_y))
                    {
                        lenght_down = i - 1;
                        stop = true;
                    }
                    if (num_y == 12 && !stop)
                    {
                        lenght_down = i;
                        stop = true;
                    }
                }
            }
            //修正左衝擊波長度
            stop = false;
            for (int i = 1; i <= _power; i++)
            {
                int num_x = _position[0] - i;
                if (num_x >= 0 && !stop)
                {
                    if (!check_map(num_x, _position[1]))
                    {
                        lenght_left = i - 1;
                        stop = true;
                    }
                    if (num_x == 0 && !stop)
                    {
                        lenght_left = i;
                        stop = true;
                    }
                }
            }
            //修正右衝擊波長度
            stop = false;
            for (int i = 1; i <= _power; i++)
            {
                int num_x = _position[0] + i;
                if (num_x <= 18 && !stop)
                {
                    if (!check_map(num_x, _position[1]))
                    {
                        lenght_right = i - 1;
                        stop = true;
                    }
                    if (num_x == 18 && !stop)
                    {
                        lenght_right = i;
                        stop = true;
                    }
                }
            }
            //傳入長度以算出衝擊波範圍
            Laser horizontal_laser = new Laser(Game_data.instance.worldTime);
            horizontal_laser.set_pos(true, lenght_left, lenght_right, _position);
            Game_data.instance.Laser_list.Add(horizontal_laser);
            Laser vertical_laser = new Laser(Game_data.instance.worldTime);
            vertical_laser.set_pos(false, lenght_up, lenght_down, _position);
            Game_data.instance.Laser_list.Add(vertical_laser);

            //如果沒超過自己能擁有的數量
            /*if (Game_data.instance.Player_list[_player]._Can_BNumber < Game_data.instance.Player_list[_player].MaxBNumber)
            {*/
            //目前可放炸彈數+1
            Game_data.instance.Player_list[_player]._Can_BNumber++;
            //}
            //destroy掉
            _Is_alive = false;
            myImage.Source = null;
            _position = new int[] { 300, 300 };
            //Game_data.instance.Bomb_list.Remove(this);
        }
        /// <summary>
        /// 如果撞到障礙物或是磚塊，回傳false
        /// </summary>
        bool check_map(int num_x, int num_y)
        {
            if (num_x >= 200)
            {
                return false;
            }
            switch (Game_data.instance._map[num_y, num_x])
            {
                case 1:
                    return false;
                case 2:
                    System.Random random = new Random(Guid.NewGuid().GetHashCode());
                    random_item = random.Next(0, 9);
                    if (random_item < 3||random_item >= 6)
                    {
                        random_item = 8;
                        _LastBoomTime = Game_data.instance.worldTime;
                    }
                    Game_data.instance._map[num_y, num_x] = random_item;
                    //對位置Game_data.instance._map[num_x,num_y]的磚塊做爆炸
                    return false;
                case 3:
                case 4:
                case 5:
                    return false;
                case 8:
                case 9:
                    return false;
            }
            return true;
        }
    }
}