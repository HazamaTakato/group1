using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Oikake.Device;
using Oikake.Util;
using Oikake.Def;

namespace Oikake.Scene
{
    class SceneFader:IScene
    {
        private enum SceneFaderState
        {
            In,
            Out,
            None
        };
        private Timer timer;
        private readonly float FADE_TIME = 2.0f;
        private SceneFaderState state;
        private IScene scene;
        private bool isEndFlag = false;

        public SceneFader(IScene scene)
        {
            this.scene = scene;
        }

        public void Draw(Renderer renderer)
        {
            switch (state)
            {
                case SceneFaderState.In:
                    DrawFadeIn(renderer);
                    break;
                case SceneFaderState.Out:
                    DrawFadeOut(renderer);
                    break;
                case SceneFaderState.None:
                    DrawFadeNone(renderer);
                    break;
            }
        }
        public void Initialize()
        {
            scene.Initialize();
            state = SceneFaderState.In;
            timer = new CountDownTimer(FADE_TIME);
            isEndFlag = false;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return scene.Next();
        }

        public void Shutdown()
        {
            scene.Shutdown();
        }

        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case SceneFaderState.In:
                    UpdateFadeIn(gameTime);
                    break;
                case SceneFaderState.Out:
                    UpdateFadeOut(gameTime);
                    break;
                case SceneFaderState.None:
                    UpdateFadeNone(gameTime);
                    break;
            }
        }

        private void UpdateFadeIn(GameTime gameTime)
        {
            scene.Update(gameTime);
            if (scene.IsEnd())
            {
                state = SceneFaderState.Out;
            }
            timer.Update(gameTime);
            if (timer.IsTime())
            {
                state = SceneFaderState.None;
            }
        }

        private void DrawFadeIn(Renderer renderer)
        {
            scene.Draw(renderer);
            DrawEffect(renderer, 1 - timer.Rate());
        }

        private void UpdateFadeOut(GameTime gameTime)
        {
            scene.Update(gameTime);
            if (scene.IsEnd())
            {
                state = SceneFaderState.Out;
            }
            timer.Update(gameTime);
            if (timer.IsTime())
            {
                isEndFlag = true;
            }
        }

        private void DrawFadeOut(Renderer renderer)
        {
            scene.Draw(renderer);
            DrawEffect(renderer, timer.Rate());
        }

        private void UpdateFadeNone(GameTime gameTime)
        {
            scene.Update(gameTime);
            if (scene.IsEnd())
            {
                state = SceneFaderState.Out;
                timer.Initialize();
            }
        }
        private void DrawFadeNone(Renderer renderer)
        {
            scene.Draw(renderer);
        }
        private void DrawEffect(Renderer renderer,float alpha)
        {
            renderer.Begin();
            renderer.DrawTexture(
                "fade",
                Vector2.Zero,
                null,
                0.0f,
                Vector2.Zero,
                new Vector2(Screen.Width, Screen.Height),
                SpriteEffects.None,
                0.0f,
                alpha);
            renderer.End();
        }
    }
}
