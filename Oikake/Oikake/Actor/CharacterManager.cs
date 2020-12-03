using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Oikake.Device;

namespace Oikake.Actor
{
    /// <summary>
    /// キャラクター管理者
    /// </summary>
    class CharacterManager
    {
        private List<Character> players;//プレイヤーリスト
        private List<Character> enemys;//エネミーリスト
        private List<Character> addNewCharacters;//追加するキャラクターリスト

        ///<summary>
        ///コンストラクタ
        ///</summary>
        public CharacterManager()
        {
            Initialize();//初期化
        }

        ///<summary>
        ///初期化
        ///</summary>
        public void Initialize()
        {
            //各リストの生成とクリア
            if(players != null)
            {
                players.Clear();
            }
            else
            {
                players = new List<Character>();
            }

            if (enemys != null)
            {
                enemys.Clear();
            }
            else
            {
                enemys = new List<Character>();
            }

            if (addNewCharacters != null)
            {
                addNewCharacters.Clear();
            }
            else
            {
                addNewCharacters = new List<Character>();
            }
        }

        ///<summary>
        ///追加
        ///</summary>
        ///<param name="character">追加するキャラクター</param>
        public void Add(Character character)
        {
            //早期リターン：登録するものがなければ何もしない
            if (character == null)
            {
                return;
            }
            //追加リストにキャラを追加
            addNewCharacters.Add(character);
        }

        ///<summary>
        ///プレイヤーたちと敵たちとの当たり判定
        ///</summary>
        private void HitToCharacters()
        {
            //プレイヤーで繰り返し
            foreach(var player in players)
            {
                //敵で繰り返し
                foreach(var enemy in enemys)
                {
                    //どちらか死んでたら次へ
                    if (player.IsDead() || enemy.IsDead())
                    {
                        continue;
                    }
                    //プレイヤーと敵が衝突しているか？
                    if (player.IsCollision(enemy))
                    {
                        //互いにヒット通知
                        player.Hit(enemy);
                        enemy.Hit(player);
                    }
                }
            }
        }

        ///<summary>
        ///死亡キャラの削除
        ///</summary>
        private void RemoveDeadCharacters()
        {
            //死んでいたら、リストから削除
            players.RemoveAll(p => p.IsDead());
            enemys.RemoveAll(e => e.IsDead());
        }

        ///<summary>
        ///更新
        ///</summary>
        ///<param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            //全キャラクター更新
            foreach(var p in players)
            {
                p.Update(gameTime);
            }
            foreach(var e in enemys)
            {
                e.Update(gameTime);
            }

            //追加候補者をリストに追加
            foreach(var newChara in addNewCharacters)
            {
                //キャラがプレイヤーだったらプレイやリストに登録
                if(newChara is Player)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                else if(newChara is PlayerBullet)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                else if(newChara is PlayerBulletEx)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                //それ以外は敵リストに登録
                else
                {
                    newChara.Initialize();
                    enemys.Add(newChara);
                }
            }
            //追加処理後、追加リストはクリア
            addNewCharacters.Clear();

            //当たり判定
            HitToCharacters();

            //死亡フラグが立ってたら削除
            RemoveDeadCharacters();
        }
        
        ///<summary>
        ///描画
        ///</summary>
        ///<param name="renderer">描画オブジェクト</param>
        public void Draw(Renderer renderer)
        {
            //全キャラ描画
            foreach(var e in enemys)
            {
                e.Draw(renderer);
            }
            foreach(var p in players)
            {
                p.Draw(renderer);
            }
        }
    }
}
