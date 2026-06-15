using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rogue;

public class MouseListener
{
    private ButtonState[] _buttons = [ButtonState.Released, ButtonState.Released, ButtonState.Released];
    private TileGame _game;
    private Action<int> _mouseDown;
    private Action<int> _mouseUp;
    private Action<Vector2> _mouseMove;

    private Vector2 _lastMousePos;

    public MouseListener(TileGame game, Action<int> mouseDown, Action<int> mouseUp, Action<Vector2> mouseMove)
    {
        _game = game;
        _mouseDown = mouseDown;
        _mouseUp = mouseUp;
        _mouseMove = mouseMove;
    }

    public void Update(MouseState state)
    {
        _handleButton(0, state.LeftButton);
        _handleButton(1, state.RightButton);
        _handleButton(2, state.MiddleButton);
        _handleMove(state);
    }

    private void _handleButton(int id, in ButtonState state)
    {
        var oldState = _buttons[id];
        if (oldState != state)
        {
            if (state == ButtonState.Pressed)
            {
                _mouseDown(id);
            } else if (state == ButtonState.Released)
            {
                _mouseUp(id);
            }
            _buttons[id] = state;
        }
    }

    private void _handleMove(in MouseState state)
    {
        var newPos = _game.Tile.GetTileFromGlobal(new Vector2(state.X, state.Y));
        if (_lastMousePos != newPos)
        {
            _mouseMove(newPos);
            _lastMousePos = newPos;
        }
    }

    public bool IsLeftPressed => _buttons[0] == ButtonState.Pressed;
    public bool IsRightPressed => _buttons[1] == ButtonState.Pressed;
    public bool IsMiddlePressed => _buttons[2] == ButtonState.Pressed;
}