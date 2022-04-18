using UnityEngine;

namespace Infrastructure.Input
{
  public interface IInputService
  {
    Vector2 Axis { get; }
    void StopUsing();
  }
}