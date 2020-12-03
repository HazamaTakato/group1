using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Actor;
using Oikake.Device;

namespace Oikake.Scene
{
    /// <summary>
    /// ゲーム仲介者
    /// </summary>
    interface IGameMediator
    {
        void AddActor(Character character);//演技者（キャラクター）を追加
        void AddScore();//得点を追加
        void AddScore(int num);//指定
        void AddHp();//Hpを追加
        void AddHp(int num);//指定
    }
}
