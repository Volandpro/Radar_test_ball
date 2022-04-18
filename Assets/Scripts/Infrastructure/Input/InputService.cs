using UnityEngine;

namespace Infrastructure.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";

    public abstract Vector2 Axis { get; }
    public virtual void StopUsing()
    {
      
    }

    protected static Vector2 SimpleInputAxis()
    {
      return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
  }
}