using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Scene;

namespace Oikake.Actor
{
   abstract class Character
    {
        //親と子クラスだけの共通部分はprotectedで宣言
        protected Vector2 position;//位置
        protected string name;//画像の名前
        protected bool isDeadFlag;//死亡フラグ
        protected IGameMediator mediator;//仲介者

        protected enum State
        {
            Preparation,//準備
            Alive,//生存
            Dying,//死に際
            Dead//死亡
        };

        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name="name">画像の名前</param>
        public Character(string name,IGameMediator mediator)
        {
            this.name = name;
            position = Vector2.Zero;
            isDeadFlag = false;
            this.mediator = mediator;
        }
        //抽象メソッド(子クラスで必ず再定義しなければならないメソッド)
        public abstract void Initialize();//初期化
        public abstract void Update(GameTime gameTime);//更新
        public abstract void Shutdown();//終了
        public abstract void Hit(Character other);//ヒット通知

        ///<summary>
        ///死んでいるか
        ///</summary>
        ///<returns></returns>
        
        public bool IsDead()
        {
            return isDeadFlag;
        }

        ///<summary>
        ///描画
        ///</summary>
        ///<param name="renderer">描画オブジェクト</param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }
        ///<summary>
        ///衝突判定(2点間の距離と円の半径)
        ///</summary>
        ///<param name="other"></param>
        ///<returns></returns>
        public bool IsCollision(Character other)
        {
            //自分と相手の位置の長さを計算(２点間の距離)
            float length = (position - other.position).Length();
            //白玉画像のサイズは６４なので、半径は３２
            //自分半径と相手の半径の和
            float radiusSum = 32f + 32f;
            //半径の和と距離を比べて、等しいかまたは小さいか（以下か）
            if(length<=radiusSum)
            {
                return true;
            }
            return false;
        }
        ///<summary>
        ///位置の受け渡し
        ///(引数で渡された変数に自分の位置を渡す)
        ///</summary>
        ///<param name="other">位置を送りたい相手</param>
        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }
    }
}
