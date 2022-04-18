using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SwitchInputButton : MonoBehaviour
    {
        private SwitchInputService switchInputService;
        [Inject]
        public void Construct(SwitchInputService switchInputService) => 
            this.switchInputService = switchInputService;

        public void OnClick() =>
            switchInputService.SwitchInput();
    }
}
