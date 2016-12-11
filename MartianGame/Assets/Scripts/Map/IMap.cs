using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map
{
    public interface IMap
    {
        bool IsLoaded();
        int GetWidth();
        int GetHeight();
        string GetName();
        Tile GetTileAt(int x, int y);
        ICharacter GetCharacter();
    }
}
