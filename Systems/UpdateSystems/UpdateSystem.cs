using System;

namespace EgoCS
{
    public abstract class UpdateSystem< TEgoInterface > : System< TEgoInterface >
        where TEgoInterface : EgoCS
    {
        public abstract void Update( TEgoInterface egoInterface );
    }
}