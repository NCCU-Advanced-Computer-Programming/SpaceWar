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
    class MainTower
    {
        //他在哪一格
        public int _posX;
        public int _posY;
        //哪一座塔(目前0是地圖左邊 1是地圖右邊)
        public int _num;

        public int life = 3;

        //上次炸時間
        public float last_hittime = 0; 

        public bool isOver = false;
        Image image;

        public MainTower(int posX, int posY, int num)
        {
            _posX = posX;
            _posY = posY;
            _num = num;
            drawTower();
        }

        public void Update()
        {
            if (life <= 0 && !isOver)
            {
                image.Source = null;
                if (_num == 0 )
                {
                    isOver = true;

                    image = new Image();
                    image.Stretch = Stretch.Fill;
                    image.Source = new BitmapImage(new Uri("Images/redwin.png", UriKind.Relative));
                    image.Width = 1140;
                    image.Height = 780;
                    image.Margin = new Thickness(0, 0, 0, 0);
                    Game_data.instance.myWindow.mainGrid.Children.Add(image);

                    
                }
                else if(_num == 1)
                {
                    isOver = true;

                    image = new Image();
                    image.Stretch = Stretch.Fill;
                    image.Source = new BitmapImage(new Uri("Images/whitewin.png", UriKind.Relative));
                    image.Width = 1140;
                    image.Height = 780;
                    image.Margin = new Thickness(0, 0, 0, 0);
                    Game_data.instance.myWindow.mainGrid.Children.Add(image);

                    
                    
                }
            }
        }

        void drawTower()
        {
            int[] pos_tower = Tools.instance.getPos_margin(_posX, _posY);
            BitmapImage maintower = new BitmapImage();
            if (_num == 0) //辨別是哪方的塔
            {
                maintower = new BitmapImage(new Uri("Images/mainTower0.png", UriKind.Relative));
            }
            else if (_num == 1)
            {
                maintower = new BitmapImage(new Uri("Images/mainTower1.png", UriKind.Relative));
            } 
            image = new Image();
            image.Stretch = Stretch.Fill;
            image.Source = maintower;
            image.Width = 60;
            image.Height = 60;
            image.Margin = new Thickness(pos_tower[0], pos_tower[1], 0, 0);
            Game_data.instance.myWindow.mainGrid.Children.Add(image);
        }

        public void Hit_MainTower(float HitTime)
        {
            if(HitTime -last_hittime  >0.5)
            {
                life--;
                last_hittime = HitTime;
                
            }

        }
    }
}
