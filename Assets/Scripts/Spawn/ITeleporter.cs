using Infrastructure.Misc;

namespace Spawn
{
    public interface ITeleporter : IEnabled, IDisabled
    {
        void PieceBecameInvisible(int index);
    }
}