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
    class Tower
    {
        //他在哪一格
        public int _posX;
        public int _posY;

        public float ray_time; //雷射光間格
        private int _power = 2; //雷射光威力
        private float _startTime; //起始爆炸時間

        //給雷射光判斷位置的
        public int[] _position;

        public int _num; //判斷是哪一方的塔


        public Tower(int posX, int posY, float startTime, int num)
        {
            _posX = posX;
            _posY = posY;
            _startTime = startTime;
            ray_time = _startTime;
            _num = num;
            _position = new int[] { _posX, _posY }; //初始化雷射光
            drawTower();
        }

        public void Update()
        {
            ray_time -= 0.02f;

            //爆炸後
            if (ray_time <= 0)
            {
                ray_time = 5;
                explosion();
                drawTower();
            }
        }

        void explosion()
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
        }

        void drawTower()
        {
            int[] pos_tower = Tools.instance.getPos_margin(_posX, _posY);
            BitmapImage Tower = new BitmapImage() ;
            if (_num == 0) //辨別是哪方的塔
            {
                Tower = new BitmapImage(new Uri("Images/tower0.png", UriKind.Relative));
            }
            else if (_num == 1)
            {
                Tower = new BitmapImage(new Uri("Images/tower1.png", UriKind.Relative));
            }
            Image image = new Image();
            image.Stretch = Stretch.Fill;
            image.Source = Tower;
            image.Width = 60;
            image.Height = 60;
            image.Margin = new Thickness(pos_tower[0], pos_tower[1], 0, 0);
            Game_data.instance.myWindow.mainGrid.Children.Add(image);
        }

        /// <summary>
        /// 如果撞到障礙物或是磚塊，回傳false
        /// </summary>
        bool check_map(int num_x, int num_y)
        {
            switch (Game_data.instance._map[num_y, num_x])
            {
                case 1:
                    return false;
                case 2:
                    Game_data.instance._map[num_y, num_x] = 0;
                    //對位置Game_data.instance._map[num_x,num_y]的磚塊做爆炸
                    return false;
            }
            return true;
        }

    }
}
