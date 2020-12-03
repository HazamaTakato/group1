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
    class NanameBound:Character
    {
        private Vector2 velocity;//移動量
        private static Random rnd = new Random();

        public NanameBound(IGameMediator mediator) : base("black",mediator)
        {
            velocity = Vector2.Zero;
        }

        public override void Initialize()
        {
            position = new Vector2(rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));
            //最初は斜め左上移動
            velocity = new Vector2(-10f, -10f);
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //左壁で反射
            if (position.X < 0)
            {
                //移動量を反射
                velocity.X = -velocity.X;
            }
            //右反射
            if (position.X > 736)
            {
                //移動量を反射
                velocity.X = -velocity.X;
            }
            //上壁で反射
            if (position.Y < 0)
            {
                //移動量を反射
                velocity.Y = -velocity.Y;
            }
            //下反射
            if (position.Y > 536)
            {
                //移動量を反射
                velocity.Y = -velocity.Y;
            }

            //移動処理（座標に移動量を足す）
            position += velocity;
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
            mediator.AddActor(new NanameBound(mediator));//新規に敵を追加
            mediator.AddActor(new NanameBound(mediator));//新規に敵を追加
            mediator.AddActor(new BurstEffect(position, mediator));//爆発エフェクトを追加
        }
    }
}
