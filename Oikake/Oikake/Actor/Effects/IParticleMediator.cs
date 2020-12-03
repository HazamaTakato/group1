using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Actor.Effects
{
    /// <summary>
    /// パーティクル仲介者
    /// </summary>
    interface IParticleMediator
    {
        ///<summary>
        ///生成
        ///</summary>
        ///<param name="name">パーティクル名</param>
        ///<returns>実体生成されたパーティクル</returns>
        Particle generate(string name);
    }
}
