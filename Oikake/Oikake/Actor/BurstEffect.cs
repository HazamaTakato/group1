using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Def;
using Oikake.Device;
using Oikake.Scene;
using Oikake.Util;
using Microsoft.Xna.Framework;

namespace Oikake.Actor
{
    class BurstEffect:Character
    {
        private Timer timer;//切り替え時間
        private int counter;//表示中の画像番号
        private readonly int pictureNum = 7;//画像登録枚数
        ///<summary>
        ///コンストラクタ
        ///エフェクト画像は1枚絵で、７つの絵でアニメーション
        ///</summary>
        ///<param name="position">表示位置</param>
        ///<param name="mediator">仲介者</param>
        public BurstEffect(Vector2 position,IGameMediator mediator) : base("pipo-btleffect", mediator)
        {
            this.position = position;
        }

        ///<summary>
        ///ヒット通知
        ///</summary>
        ///<param name="other"></param>
        public override void Hit(Character other)
        {
        }

        ///<summary>
        ///初期化
        ///</summary>
        public override void Initialize()
        {
            counter = 0;
            isDeadFlag = false;
            timer = new CountDownTimer(0.05f);
        }

        ///<summary>
        ///終了
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
            //タイマー更新
            timer.Update(gameTime);

            //指定時間か？
            if (timer.IsTime())
            {
                //次の画像へ
                counter += 1;
                //時間初期化
                timer.Initialize();
                //アニメーション画像の最後までたどり着いてたら死亡へ
                if (counter >= pictureNum)
                {
                    isDeadFlag = true;
                }
            }
        }

        ///<summary>
        ///描画
        ///</summary>
        ///<param name="renderer">描画オブジェクト</param>
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, new Rectangle(counter * 120, 0, 120, 120));
        }
    }
}
