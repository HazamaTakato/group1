using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Oikake.Def;
using Oikake.Scene;

namespace Oikake.Actor
{
    ///<summary>
    ///キャラクタークラスを継承した乱数エネミークラス
    ///</summary>
    class RandomEnemy :Character
    {
        //乱数オブジェクトはRandomEnemyクラスで共通になるようにstatic
        private static Random rnd = new Random();
        private int changeTimer;//切り替え時間

        ///<summary>
        ///コンストラクタ
        ///</summary>
        public RandomEnemy(IGameMediator mediator) : base("black",mediator)
        {
            changeTimer = 60;//60fps
        }

        ///<summary>
        ///初期化
        ///</summary>
        public override void Initialize()
        {
            //乱数で位置と切り替え時間を決定
            position = new Vector2(rnd.Next(Screen.Width - 64),
                                 rnd.Next(Screen.Height - 64));
            changeTimer = 60 * rnd.Next(2, 5);//60フレーム*２～５秒
        }

        ///<summary>
        ///終了処理
        ///</summary>
        public override void Shutdown()
        {
        }

        ///<summary>
        ///更新
        ///</summary>
        ///<param name="gameTime"></param>

        public override void Update(GameTime gameTime)
        {
            //切り替え時間を減らす
            changeTimer -= 1;
            //マイナスになったか？
            if (changeTimer < 0)
            {
                //位置と時間を初期化
                Initialize();
            }
        }
        ///<summary>
        ///ヒット通知
        ///</summary>
        ///<param name="other">衝突した相手</param>
        public override void Hit(Character other)
        {
            isDeadFlag = true;//死亡
            mediator.AddScore(100);//得点追加
            //死んだら敵を増やす
            mediator.AddActor(new RandomEnemy(mediator));//新規に敵を追加
            mediator.AddActor(new RandomEnemy(mediator));//新規に敵を追加
            mediator.AddActor(new BurstEffect(position, mediator));//爆発エフェクトを追加
        }
    }
}
