using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication7
{
    //遊戲的資料庫
    class Game_data
    {
        public static Game_data instance = new Game_data();

        public MainWindow myWindow;

        //世界時間
        public float worldTime = 0;


        //如果有重新開始選項可重設地圖
        public int[,] initial_map ={ {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,1},
                                     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                     {1,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1},
                                     {1,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,1},
                                     {1,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,1},
                                     {1,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                     {1,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,1},
                                     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                            };
        //null=0,obstacle=1,brick=2,itemS=3,itemP=4,itemA=5
        public int[,] _map ={   {0,0,2,2,0,0,1,2,2,2,2,2,1,2,2,2,2,2,2},
                                {0,1,2,0,0,2,2,2,2,0,2,2,2,2,0,0,0,1,0}, 
                                {2,2,0,2,9,0,2,2,0,2,0,2,2,0,9,0,0,0,0}, 
                                {2,0,1,0,0,2,0,2,0,2,0,2,0,2,0,0,1,0,2}, 
                                {2,2,0,2,0,0,2,2,0,2,0,2,2,0,0,2,0,2,2}, 
                                {2,7,2,0,2,1,2,2,2,1,2,2,2,1,2,0,2,7,2},
                                {2,2,0,2,0,0,2,2,0,2,0,2,2,0,0,2,0,2,2},
                                {2,0,1,0,0,2,0,2,0,2,0,2,0,2,0,0,1,0,2},
                                {0,0,0,0,9,0,2,2,0,2,0,2,2,0,9,2,0,2,2}, 
                                {0,1,0,0,0,2,2,2,2,0,2,2,2,2,0,0,2,1,0}, 
                                {2,2,2,2,2,2,1,2,2,2,2,2,1,0,0,2,2,0,0}, 
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}, 
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}


                            };


        public List<Player> Player_list = new List<Player>();

        public List<Bomb> Bomb_list = new List<Bomb>();

        public List<Item> Item_list = new List<Item>();

        public List<Laser> Laser_list = new List<Laser>();

        public List<Tower> Tower_list = new List<Tower>();

        public List<MainTower> MainTower_list = new List<MainTower>();
        //紀錄鍵盤事件
        public string[] Move_player = { "", "" };
        public bool[] IsBomb_player = { false , false };
        public bool[] IsSkill_player = { false, false };

        //遊戲進行時間
        public float GameTime = 0.0f;


        public BitmapImage Laser_horizontal;
        public BitmapImage Laser_vertical;
        public BitmapImage Laser_left;
        public BitmapImage Laser_right;
        public BitmapImage Laser_up;
        public BitmapImage Laser_down;
        public BitmapImage Laser_mid;
        public BitmapImage TopImage;
        public BitmapImage DownImage;
        public BitmapImage LeftImage;
        public BitmapImage RightImage;
        public BitmapImage TopImage1;
        public BitmapImage DownImage1;
        public BitmapImage LeftImage1;
        public BitmapImage RightImage1;
        public BitmapImage DieImage;
        public BitmapImage Odstacle;
        public BitmapImage Brick;
        public BitmapImage ItemS;
        public BitmapImage ItemP;
        public BitmapImage ItemA;
        public BitmapImage star1;
        public BitmapImage star2;
        public BitmapImage star3;
        public BitmapImage star4;
        public BitmapImage star5;
        public BitmapImage star6;
        public BitmapImage star7;
        public BitmapImage star8;
        public BitmapImage bomber;

        public void load_pic()
        {
            Laser_horizontal = new BitmapImage(new Uri("Images/ray_horizontal.png", UriKind.Relative));
            Laser_vertical = new BitmapImage(new Uri("Images/ray_vertical.png", UriKind.Relative));
            Laser_left = new BitmapImage(new Uri("Images/ray_left.png", UriKind.Relative));
            Laser_right = new BitmapImage(new Uri("Images/ray_right.png", UriKind.Relative));
            Laser_up = new BitmapImage(new Uri("Images/ray_up.png", UriKind.Relative));
            Laser_down = new BitmapImage(new Uri("Images/ray_down.png", UriKind.Relative));
            Laser_mid = new BitmapImage(new Uri("Images/ray_mid.png", UriKind.Relative));
            
            //移動 人物0
            TopImage = new BitmapImage(new Uri("Images/top.png", UriKind.Relative));
            DownImage = new BitmapImage(new Uri("Images/down.png", UriKind.Relative));
            LeftImage = new BitmapImage(new Uri("Images/left.png", UriKind.Relative));
            RightImage = new BitmapImage(new Uri("Images/right.png", UriKind.Relative));
            //移動 人物1
            TopImage1 = new BitmapImage(new Uri("Images/top1.png", UriKind.Relative));
            DownImage1 = new BitmapImage(new Uri("Images/down1.png", UriKind.Relative));
            LeftImage1 = new BitmapImage(new Uri("Images/left1.png", UriKind.Relative));
            RightImage1 = new BitmapImage(new Uri("Images/right1.png", UriKind.Relative));

            DieImage = new BitmapImage(new Uri("Images/die.png", UriKind.Relative));
            //map
            Odstacle = new BitmapImage(new Uri("Images/Odstacle.jpg", UriKind.Relative));
            Brick = new BitmapImage(new Uri("Images/Brick.png", UriKind.Relative));
            ItemS = new BitmapImage(new Uri("Images/speed.png", UriKind.Relative));
            ItemP = new BitmapImage(new Uri("Images/power.png", UriKind.Relative));
            ItemA = new BitmapImage(new Uri("Images/amout.png", UriKind.Relative));

            star1 = new BitmapImage(new Uri("Images/C_6.png", UriKind.Relative));
            star2 = new BitmapImage(new Uri("Images/C_1.png", UriKind.Relative));
            star3 = new BitmapImage(new Uri("Images/C_2.png", UriKind.Relative));
            star4 = new BitmapImage(new Uri("Images/C_3.png", UriKind.Relative));
            star5 = new BitmapImage(new Uri("Images/C_4.png", UriKind.Relative));
            star6 = new BitmapImage(new Uri("Images/C_5.png", UriKind.Relative));
            star7 = new BitmapImage(new Uri("Images/C_7.png", UriKind.Relative));
            star8 = new BitmapImage(new Uri("Images/C_8.png", UriKind.Relative));

            bomber = new BitmapImage(new Uri("Images/bomber.png", UriKind.Relative));
        }

    }
}
