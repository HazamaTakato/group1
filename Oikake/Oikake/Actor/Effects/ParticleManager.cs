using Microsoft.Xna.Framework;
using Oikake.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Scene;

namespace Oikake.Actor.Effects
{
    class ParticleManager
    {
        //パーティクルのリスト
        private List<Particle> particles = new List<Particle>();
        private List<Particle> addPartocles = new List<Particle>();

        ///<summary>
        ///コンストラクタ
        ///</summary>
        public ParticleManager()
        { }
        /// <summary>
        /// 初期か？
        /// </summary>
        public void Initialize()
        {
            particles.Clear();//リストクリア
            addPartocles.Clear();
        }
        ///<summary>
        ///更新
        ///</summary>
        ///<param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //一括更新
            particles.ForEach(particle => particle.Update(gameTime));
            //追加リストからデータを登録
            particles.AddRange(addPartocles);
            addPartocles.Clear();
            //死亡しているものはリストから削除
            particles.RemoveAll(particle => particle.IsDead());
        }
        ///<summary>
        ///終了
        ///</summary>
        public void Shutdown()
        {
        }

        ///<summary>
        ///描画
        ///</summary>
        ///<param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //一括描画
            particles.ForEach(particle => particle.Draw(renderer));
        }
        ///<summary>
        ///パーティクルの追加
        ///</summary>
        ///<param name="particle"></param>
        public void Add(Particle particle)
        {
            //particles.Add(particle);
            addPartocles.Add(particle);
        }
        
    }
}
