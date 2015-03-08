using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication7
{
    //所有的碰撞偵測，請針對Game_data中的遊戲資訊做碰撞偵測，使用Tools中的AABBTest
    //現在是把每種碰撞偵測分開，到時可能會是針對Game_data中的Map_data做偵測，寫法如果不一樣在修改
    class Collider
    {
        Tools tools = new Tools();
        public static Collider instance = new Collider();
        int _height = 13; //高度格數(列數)
        int _width = 19; //寬度格數(行數)
        int L_ltposx;
        int L_ltposy;
        int L_rbposx;
        int L_rbposy;
        float P_ltposx;
        float P_ltposy;
        int B_ltposx;
        int B_ltposy;
        int posx;
        int posy;
        int MT_ltposx;
        int MT_ltposy;
        //float _positionX;
        // float _positionY;
        // public Collider (float _PositionX , float _PositionY)
        // {
        // _positionX = _PositionX;
        // _positionY = _PositionY;

        // }

        public void Update()
        {
            //可依照遊戲需求調整順序
            //Player_bomb();
            Player_hit();
            //Player_item();
            //Obstacle_hit();
            Item_hit();
            MainTower_hit();
            //Bomb_hit();
        }

        public bool Player_bomb(float x, float y, int num)
        {
            //玩家與炸彈的碰撞偵測
            foreach (Bomb _bomb in Game_data.instance.Bomb_list)
            {
                posx = _bomb._position[0];
                posy = _bomb._position[1];
                //foreach (Player _player in Game_data.instance.Player_list)
                //{
                //    P_ltposx = _player._PositionX;
                //    P_ltposy = _player._PositionY;
                if (tools.AABBtest(posx * 60 + 10, posy * 60 + 10, posx * 60 + 50, posy * 60 + 50, x + 10, y + 10, x + 50, y + 50)&&
                    !_bomb.player_on[num])
                {
                    return true;
                }
                //}
            }
            return false;
        }

        public bool Player_Obstacle(float x, float y)
        {
            //玩家與障礙物，包括磚塊

            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    if (Game_data.instance._map[row, col] == 1 || Game_data.instance._map[row, col] == 2 || Game_data.instance._map[row, col] == 9||Game_data.instance._map[row, col] == 7)

                        if (tools.AABBtest(x + 5, y + 5, x + 55, y + 55, col * 60 + 10, row * 60 + 10, col * 60 + 50, row * 60 + 50))
                        {
                            return true;
                        }
                }
            }
            return false;
        }

        public void Player_hit()
        {
            //玩家與衝擊波
            foreach (Laser _laser in Game_data.instance.Laser_list)
            {
                L_ltposx = _laser.start_point[0] * 60 + 10;
                L_ltposy = _laser.start_point[1] * 60 + 10;
                L_rbposx = (_laser.end_point[0] + 1) * 60 - 10;
                L_rbposy = (_laser.end_point[1] + 1) * 60 - 10;
                //System.Windows.MessageBox.Show(L_ltposx + " " + L_ltposy);
                foreach (Player _player in Game_data.instance.Player_list)
                {
                    P_ltposx = _player._PositionX;
                    P_ltposy = _player._PositionY;
                    if (tools.AABBtest(L_ltposx, L_ltposy, L_rbposx, L_rbposy, P_ltposx + 15, P_ltposy + 15, P_ltposx + 45, P_ltposy + 45) &&
                    _player._IsLife)
                    {
                        _player.Hit();
                        //System.Windows.MessageBox.Show(L_ltposx + " " + L_ltposy + " " + L_rbposx + " " + L_rbposy);
                    }
                }
            }



        }

        public void Player_item(Player player)
        {   //玩家跟道具
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    if (Game_data.instance._map[row, col] == 3)
                    {
                        if (tools.AABBtest(player._PositionX, player._PositionY, player._PositionX + 55, player._PositionY + 55, col * 60 + 10, row * 60 + 10, col * 60 + 50, row * 60 + 50))
                        {
                            Game_data.instance._map[row, col] = 0;
                            if (player._Speed * 1.1f <= player.MaxSpeed)
                            {
                                player._Speed = player._Speed * 1.1f;
                            }

                        }
                    }
                    else if (Game_data.instance._map[row, col] == 4)
                    {
                        if (tools.AABBtest(player._PositionX, player._PositionY, player._PositionX + 55, player._PositionY + 55, col * 60 + 10, row * 60 + 10, col * 60 + 50, row * 60 + 50))
                        {
                            Game_data.instance._map[row, col] = 0;
                            if (player._Power + 1 <= player.MaxPower)
                            {
                                player._Power++;
                            }

                        }
                    }
                    else if (Game_data.instance._map[row, col] == 5)
                    {
                        if (tools.AABBtest(player._PositionX, player._PositionY, player._PositionX + 55, player._PositionY + 55, col * 60 + 10, row * 60 + 10, col * 60 + 50, row * 60 + 50))
                        {
                            Game_data.instance._map[row, col] = 0;
                            if (player._BNumber + 1 <= player.MaxBNumber)
                            {
                                //player._BNumber++;
                                player._Can_BNumber++;
                            }

                        }
                    }
                }
            }

        }

        //public void Obstacle_hit()
        //{
        //    //障礙物(包括磚塊)與衝擊波
        //    foreach (Laser _laser in Game_data.instance.Laser_list)
        //    {
        //        L_ltposx = _laser.start_point[0] * 60 + 5;
        //        L_ltposy = _laser.start_point[1] * 60 + 5;
        //        L_rbposx = (_laser.end_point[0] + 1) * 60;
        //        L_rbposy = (_laser.end_point[1] + 1) * 60;
        //        for (int row = 0; row < _height; row++)
        //        {
        //            for (int col = 0; col < _width; col++)
        //            {
        //                if (Game_data.instance._map[row, col] == 1 || Game_data.instance._map[row, col] == 2)

        //                    if (tools.AABBtest(L_ltposx, L_ltposy, L_rbposx, L_rbposy, col * 60, row * 60, col * 60 + 60, row * 60 + 60))
        //                    {
        //                        //System.Windows.MessageBox.Show("sss");
        //                    }
        //            }
        //        }
        //    }
        //}

        public void Item_hit()
        {
            //道具與衝擊波
        }

        public bool Bomb_hit(int POS_X, int POS_Y)
        {
            //炸彈與衝擊波
            foreach (Laser _laser in Game_data.instance.Laser_list)
            {
                L_ltposx = _laser.start_point[0] * 60 + 5;
                L_ltposy = _laser.start_point[1] * 60 + 5;
                L_rbposx = (_laser.end_point[0] + 1) * 60;
                L_rbposy = (_laser.end_point[1] + 1) * 60;
                B_ltposx = POS_X * 60 + 5;
                B_ltposy = POS_Y * 60 + 5;
                //System.Windows.MessageBox.Show(B_ltposx.ToString() + " " + B_ltposy.ToString());
                if (tools.AABBtest(L_ltposx, L_ltposy, L_rbposx, L_rbposy, B_ltposx, B_ltposy, B_ltposx + 45, B_ltposy + 45))
                {
                    return true;

                }
            }
            return false;
        }
        public void MainTower_hit()
        {
            //主堡跟衝擊波
            foreach (Laser _laser in Game_data.instance.Laser_list)
            {
                L_ltposx = _laser.start_point[0] * 60 + 10;
                L_ltposy = _laser.start_point[1] * 60 + 10;
                L_rbposx = (_laser.end_point[0] + 1) * 60 - 10;
                L_rbposy = (_laser.end_point[1] + 1) * 60 - 10;
                //System.Windows.MessageBox.Show(L_ltposx + " " + L_ltposy);
                foreach (MainTower _maintower in Game_data.instance.MainTower_list)
                {
                    MT_ltposx = _maintower._posX;
                    MT_ltposy = _maintower._posY;
                    if (tools.AABBtest(L_ltposx, L_ltposy, L_rbposx, L_rbposy, MT_ltposx * 60, MT_ltposy * 60, MT_ltposx * 60 + 60, MT_ltposy * 60 + 60) &&
                    _maintower.life > 0 && !_maintower.isOver)
                    {
                        _maintower.Hit_MainTower(Game_data.instance.worldTime);
                        //System.Windows.MessageBox.Show(L_ltposx + " " + L_ltposy + " " + L_rbposx + " " + L_rbposy);
                    }
                }
            }
        }
        public bool Player_forward(int posx, int posy, int num)
        {
            foreach (Player _player in Game_data.instance.Player_list)
            {
                if (num != _player.number)
                {
                    if (tools.AABBtest(posx * 60 + 5, posy * 60 + 5, posx * 60 + 55, posy * 60 + 55, _player._PositionX + 5, _player._PositionY + 5, _player._PositionX + 55, _player._PositionY + 55))
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        //public void Bomb_hit()
        //{
        // //炸彈與衝擊波
        // foreach (Laser _laser in Game_data.instance.Laser_list)
        // {
        // L_ltposx = _laser.start_point[0] * 60 + 5;
        // L_ltposy = _laser.start_point[1] * 60 + 5;
        // L_rbposx = (_laser.end_point[0] + 1) * 60;
        // L_rbposy = (_laser.end_point[1] + 1) * 60;
        // foreach(Bomb _bomb in Game_data.instance.Bomb_list)
        // {
        // if (_bomb._Is_alive)
        // {
        // B_ltposx = _bomb._position[0] * 60 + 5;
        // B_ltposy = _bomb._position[1] * 60 + 5;
        // //System.Windows.MessageBox.Show(B_ltposx.ToString() + " " + B_ltposy.ToString());
        // if (tools.AABBtest(L_ltposx, L_ltposy, L_rbposx, L_rbposy, B_ltposx, B_ltposy, B_ltposx + 45, B_ltposy + 45))
        // {
        // _bomb.Explosion();

        // }
        // }
        // }
        // }
        //}
    }
}