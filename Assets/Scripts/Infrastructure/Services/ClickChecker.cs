using System;
using UnityEngine;

namespace Infrastructure.Services
{
    public class ClickChecker : MonoBehaviour
    {
        public Action OnClick;

        void Update()
        {
            if(UnityEngine.Input.GetMouseButtonDown(0))
                OnClick?.Invoke();
        }
    }
}
