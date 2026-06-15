using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Rogue;

public class KeyListener
{
    private Action<Keys> _onPress;
    private Action<Keys> _onRelease;
    private HashSet<Keys> _keys;

    public KeyListener(Action<Keys> onPress, Action<Keys> onRelease=null)
    {
        _onPress = onPress;
        _onRelease = onRelease;
        _keys = new();
    }

    public void Update(KeyboardState state)
    {
        var newKeys = state.GetPressedKeys().ToHashSet();
        foreach (var key in newKeys)
        {
            if (!_keys.Contains(key))
            {
                _onPress(key);
                _keys.Add(key);
            }
        }

        foreach (var pressedKey in _keys.ToList())
        {
            if (!newKeys.Contains(pressedKey))
            {
                _onRelease?.Invoke(pressedKey);
                _keys.Remove(pressedKey);
            }
        }
    }
}