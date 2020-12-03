using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Oikake.Def;
using Oikake.Scene;
using Microsoft.Xna.Framework.Content;//リソースへのアクセス
using Microsoft.Xna.Framework.Media;//MP3
using Microsoft.Xna.Framework.Audio;//WAVデータ


namespace Oikake.Actor
{
    class BoundEnemy:Character
    {
        private Vector2 velocity;//移動量
        private static Random rnd = new Random();
        public BoundEnemy(IGameMediator mediator):base("black",mediator)
        {
        }

        public override void Initialize()
        {
            position = new Vector2(rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));
            //最初は左移動
            velocity = new Vector2(-10f, 0);
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
                velocity = -velocity;
            }
            //右反射
            if (position.X > 736)
            {
                //移動量を反射
                velocity = -velocity;
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
            mediator.AddActor(new BoundEnemy(mediator));//新規に敵を追加
            mediator.AddActor(new BoundEnemy(mediator));//新規に敵を追加
            mediator.AddActor(new BurstEffect(position, mediator));//爆発エフェクトを追加
        }
    }
}
