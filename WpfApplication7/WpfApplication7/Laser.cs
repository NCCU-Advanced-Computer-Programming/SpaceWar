using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication7
{
    class Laser
    {

        public int[] start_point;
        public int[] end_point;
        private int[] _pos;

        public float _worldTime;
        private List<Image> image_list = new List<Image>();


        public Laser(float worldTime)
        {
            _worldTime = worldTime;        
        }


        public void Update()
        {
            if ((Game_data.instance.worldTime - _worldTime) >= 0.3f)
            {
                foreach (Image _image in image_list)
                {
                    _image.Source = null;
                    start_point = new int[] { 300, 300 };
                    end_point = new int[] { 300, 300 };
                }
            }
        }

        //垂直時，傳入上下長度；水平時，傳入左右長度
        public void set_pos(bool horizontal, int lenght_left_top, int lenght_right_bottom, int[] position)
        {
            _pos = position;
            if (horizontal)
            {
                //string test = "left="+lenght_left_top.ToString()+"  right="+lenght_right_bottom.ToString()+"  posX="+position[0]+"  posY="+position[1];
                //MessageBox.Show(test);
                start_point = new int[]{position[0]-lenght_left_top,position[1]};
                end_point = new int[]{position[0]+lenght_right_bottom,position[1]};

            }
            else
            {
                start_point = new int[] { position[0], position[1] - lenght_left_top };
                end_point = new int[] { position[0], position[1] + lenght_right_bottom };
            }
            show_picture(horizontal);
        }

        void show_picture(bool horizontal)
        {
            //在這裡秀出衝擊波圖片
            BitmapImage left_up = new BitmapImage();
            BitmapImage right_down = new BitmapImage();
            if (horizontal)
            {
                left_up = Game_data.instance.Laser_left;
                right_down = Game_data.instance.Laser_right;
                for (int i = 1; i < (end_point[0] - start_point[0]); i++)
                {
                    int[] pos_mid = Tools.instance.getPos_margin(start_point[0] + i, start_point[1]);
                    //BitmapImage Laser_horizontal_mid = new BitmapImage(new Uri("Images/ray_horizontal.png", UriKind.Relative));
                    Image image_mid = new Image();
                    image_mid.Stretch = Stretch.Fill;
                    image_mid.Source = Game_data.instance.Laser_horizontal;
                    image_mid.Width = 60;
                    image_mid.Height = 60;
                    image_mid.Margin = new Thickness(pos_mid[0], pos_mid[1], 0, 0);
                    image_list.Add(image_mid);
                    Game_data.instance.myWindow.mainGrid.Children.Add(image_mid);
                }
            }
            else
            {
                left_up = Game_data.instance.Laser_up;
                right_down = Game_data.instance.Laser_down;
                for (int i = 1; i < (end_point[1] - start_point[1]); i++)
                {
                    int[] pos_mid = Tools.instance.getPos_margin(start_point[0], start_point[1] + i);
                    //BitmapImage Laser_horizontal_mid = new BitmapImage(new Uri("Images/ray_vertical.png", UriKind.Relative));                  
                    Image image_mid = new Image();
                    image_mid.Stretch = Stretch.Fill;
                    image_mid.Source = Game_data.instance.Laser_vertical;
                    image_mid.Width = 60;
                    image_mid.Height = 60;
                    image_mid.Margin = new Thickness(pos_mid[0], pos_mid[1], 0, 0);
                    image_list.Add(image_mid);
                    Game_data.instance.myWindow.mainGrid.Children.Add(image_mid);
                }
            }

            int[] pos_start = Tools.instance.getPos_margin(start_point[0], start_point[1]);
            //BitmapImage Laser_horizontal_left = new BitmapImage(new Uri(left_top, UriKind.Relative));
            Image image_left = new Image();
            image_left.Stretch = Stretch.Fill;
            image_left.Source = left_up;
            image_left.Width = 60;
            image_left.Height = 60;
            image_left.Margin = new Thickness(pos_start[0], pos_start[1], 0, 0);
            image_list.Add(image_left);
            Game_data.instance.myWindow.mainGrid.Children.Add(image_left);

            int[] pos_end = Tools.instance.getPos_margin(end_point[0], end_point[1]);
            //BitmapImage Laser_horizontal_right = new BitmapImage(new Uri(right_bottom, UriKind.Relative));
            Image image_right = new Image();
            image_right.Stretch = Stretch.Fill;
            image_right.Source = right_down;
            image_right.Width = 60;
            image_right.Height = 60;
            image_right.Margin = new Thickness(pos_end[0], pos_end[1], 0, 0);
            image_list.Add(image_right);
            Game_data.instance.myWindow.mainGrid.Children.Add(image_right);

            int[] pos_mid2 = Tools.instance.getPos_margin(_pos[0], _pos[1]);
            //BitmapImage Laser_horizontal_mid2 = new BitmapImage(new Uri("Images/ray_mid.png", UriKind.Relative));
            Image image_mid2 = new Image();
            image_mid2.Stretch = Stretch.Fill;
            image_mid2.Source = Game_data.instance.Laser_mid;
            image_mid2.Width = 60;
            image_mid2.Height = 60;
            image_mid2.Margin = new Thickness(pos_mid2[0], pos_mid2[1], 0, 0);
            image_list.Add(image_mid2);
            Game_data.instance.myWindow.mainGrid.Children.Add(image_mid2);

        }
    }
}
