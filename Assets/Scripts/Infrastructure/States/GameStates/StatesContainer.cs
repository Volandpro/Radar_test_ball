using System;
using System.Collections.Generic;

namespace Infrastructure.States.GameStates
{
    public class StatesContainer
    {
        public Dictionary<Type, IState> allStates;
    }
}