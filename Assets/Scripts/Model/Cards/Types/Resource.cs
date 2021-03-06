﻿using model.zones;
using System.Collections.Generic;

namespace model.cards.types
{
    public class Resource : IType
    {
        bool IType.Playable => false;
        bool IType.Installable => true;
        bool IType.Rezzable => false;
        List<IInstallDestination> IType.FindInstallDestinations(Game game)
        {
            return new List<IInstallDestination>()
            {
                game.runner.zones.rig
            };
        }
    }
}