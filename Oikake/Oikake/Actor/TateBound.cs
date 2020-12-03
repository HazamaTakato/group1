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
    class TateBound:Character
    {
        private Vector2 velocity;//移動量
        private static Random rnd = new Random();
        public TateBound(IGameMediator mediator) : base("black",mediator)
        {
            velocity = Vector2.Zero;
            position = Vector2.Zero;
        }

        public override void Initialize()
        {
            position = new Vector2(rnd.Next(Screen.Width - 64),
                -64);
            //最初は上移動
            velocity = new Vector2(0, 1f);
        }
        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {


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
            mediator.AddActor(new TateBound(mediator));//新規に敵を追加
            mediator.AddActor(new TateBound(mediator));//新規に敵を追加
            mediator.AddActor(new BurstEffect(position, mediator));//爆発エフェクトを追加
        }
    }
}
