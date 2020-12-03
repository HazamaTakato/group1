using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;//vector2用
using Microsoft.Xna.Framework.Input;//入力処理用
using Microsoft.Xna.Framework.Graphics;

using Oikake.Actor;
using Oikake.Device;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake.Actor
{
    /// <summary>
    /// 白玉（プレイヤー）
    /// </summary>
    class Player:Character
    {
        //フィールド
        //  private Vector2 position;//位置

        //モーション管理オブジェクト
        private Motion motion;
        private bool ItemGetFlag;

        ///<summary>
        ///向き
        ///</summary>
        private enum Direction
        {
            DOWN,UP,RIGHT,LEFT
        };
        private Direction direction;//現在の向き
        //向きと範囲を管理
        private Dictionary<Direction, Range> directionRange;

        ///<summary>
        ///コンストラクタ
        ///</summary>
        public Player(IGameMediator mediator):base("player_sentouki", mediator)
        {
        }

        ///<summary>
        ///初期化メソッド
        ///</summary>
        public override void Initialize()
        {
            ItemGetFlag = false;
            //位置を（３００,４００）に設定
            position = new Vector2(300, 400);

            motion = new Motion();
            ////下向き
            //for(int i=0; i <= 3; i++)
            //{
            //    motion.Add(i, new Rectangle(64 * i, 64 * 0, 64, 64));
            //}

            //上向き
            for (int i = 0; i <= 15; i++)
            {
                for (int n = 0; n <= 15; n++)
                {
                    motion.Add(i, new Rectangle(64 * n, 64 * 1, 64, 64));
                }
            }
            //右向き
            //for (int i = 8; i <= 11; i++)
            //{
            //    for (int n = 0; n <= 3; n++)
            //    {
            //        motion.Add(i, new Rectangle(64 * n, 64 * 2, 64, 64));
            //    }
            //}
            ////左向き
            //for (int i = 12; i <= 15; i++)
            //{
            //    for (int n = 0; n <= 3; n++)
            //    {
            //        motion.Add(i, new Rectangle(64 * n, 64 * 3, 64, 64));
            //    }
            //}
            //最初はすべてのパーツ表示に設定
            motion.Initialize(new Range(0, 15), new CountDownTimer(0.2f));

            //最初は下向きに
            direction = Direction.UP;
            directionRange = new Dictionary<Direction, Range>()
            {
                //{Direction.DOWN,new Range(0,3) },
                {Direction.UP,new Range(0,15) }
                //{Direction.RIGHT,new Range(8,11) },
                //{Direction.LEFT,new Range(12,15) }
            };
        }

        ///<summary>
        ///更新処理
        ///</summary>
        ///<param name="gametime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
            //キー入力の移動量を取得
            Vector2 velocity = Input.Velocity();

            //移動処理
            float speed = 10.0f;
            position = position + velocity * speed;

            //当たり判定
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);
            position = Vector2.Clamp(position, min, max);

            UpdateMotion();
            motion.Update(gameTime);

            if (Input.GetKeyTrigger(Keys.Z))
            {
                //上下左右キーが押されてなければその向きに移動量を決定
                if (velocity.Length() >= 0)
                {
                    Dictionary<Direction, Vector2> velocityDict = new Dictionary<Direction, Vector2>()
                    {
                        //{Direction.LEFT,new Vector2(-1,0) },
                        //{Direction.RIGHT,new Vector2(1,0) },
                        {Direction.UP,new Vector2(0,-1) }
                        //{Direction.DOWN,new Vector2(0,1) }
                    };
                    velocity = velocityDict[direction];
                }
                //弾を発射
                if (ItemGetFlag == false)
                {
                    mediator.AddActor(
                    new PlayerBullet(
                    position,
                    mediator,
                    velocity));
                }
                else if (ItemGetFlag == true)
                {
                    mediator.AddActor(
                    new PlayerBulletEx(
                    position,
                    mediator,
                    velocity));
                }
                Console.WriteLine(ItemGetFlag);
            }
        }
        
        ///<summary>
        ///描画メソッド
        ///</summary>
        ///<param name="renderer">描画オブジェクト</param>
      //  public void Draw(Renderer renderer)
      //  {
            //レンダラーで白玉画像を描画
      //      renderer.DrawTexture("white", position);
      //  }

        ///<summary>
        ///終了処理
        ///</summary>
        public override void Shutdown()
        {
        }
        ///<summary>
        ///ヒット通知
        ///</summary>
        ///<param name="other">衝突した相手</param>
        public override void Hit(Character other)
        {
            if(other is DropItem)
            {
                ItemGetFlag = true;
            }
            if (other is EnemyBullet)
            {
                Vector2 effectPos=position;
                effectPos = effectPos + new Vector2(0f, -100f);
                mediator.AddActor(new BurstEffect(effectPos
                    , mediator));
            }
        }

        ///<summary>
        ///CharacterクラスのDrawメソッドに代わって描画
        ///</summary>
        ///<param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, motion.DrawingRange());
        }

        ///<summary>
        ///モーションの変更
        ///</summary>
        ///<param name="direction">変更したい向き</param>
        private void ChangeMotion(Direction direction)
        {
            this.direction = direction;
            motion.Initialize(directionRange[direction], new CountDownTimer(0.2f));
        }

        ///<summary>
        ///キー入力から向きを決定
        ///</summary>
        private void UpdateMotion()
        {
            //キーの入力情報を取得
            Vector2 velocity = Input.Velocity();

            //キーの入力がなければ何もしない
            if (velocity.Length() <= 0.0f)
            {
                return;
            }

            //キー入力があった時
            ////下に変更
            //if ((velocity.Y > 0.0f) && (direction != Direction.DOWN))
            //{
            //    ChangeMotion(Direction.DOWN);
            //}
            //上向きに変更
            //else if ((velocity.Y<0.0f) && (direction != Direction.UP))
            //{
            //    ChangeMotion(Direction.UP);
            //}
            ////右向きに変更
            //else if ((velocity.X > 0.0f) && (direction != Direction.RIGHT))
            //{
            //    ChangeMotion(Direction.RIGHT);
            //}
            ////左向きに変更
            //else if ((velocity.X < 0.0f) && (direction != Direction.LEFT))
            //{
            //    ChangeMotion(Direction.LEFT);
            //}
        }
    }
}
