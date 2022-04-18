using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Infrastructure.Input
{
      public class AutomaticInputService : InputService
      {
          private readonly ClickChecker clickerChecker;
          private const float Speed = 0.7f;
          private static int sign = 1;
          public override Vector2 Axis => UnityAxis();

          [Inject]
          public AutomaticInputService(ClickChecker clickerChecker)
          {
              this.clickerChecker = clickerChecker;
              clickerChecker.OnClick += SwitchDirection;
          }
          public override void StopUsing() => 
              clickerChecker.OnClick -= SwitchDirection;
          
          private void SwitchDirection() => 
              sign *= -1;

          private static Vector2 UnityAxis()
          {
              return new Vector2(Speed, 0) * sign;
          }
      }
}