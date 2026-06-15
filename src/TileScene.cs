using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rogue;

public abstract class TileScene : IDisposable
{
    private TileGame _game;

    protected TileGraphics Tile => _game.Tile;
    protected GameWindow Window => _game.Window;
    protected ContentManager Content => _game.Content;
    protected SpriteBatch SpriteBatch => _game.Batch;
    protected GameTime GameTime => _game.GameTime;
    protected KeyListener Keyboard => _game._keyboard;
    protected MouseListener Mouse => _game._mouse;
    protected Vector2 MousePos => _game.MousePos;

    protected Color BgColor
    {
        get => _game.BgColor;
        set => _game.BgColor = value;
    }

    protected Vector2 ScreenSize => Tile.GetScreenSizeTiles(Window);

    protected SpriteFont Font
    {
        get => _game.Font;
        set => _game.Font = value;
    }

    internal void InitScene(TileGame tileGame)
    {
        _game = tileGame;
        Init();
    }

    public abstract void Init();
    public abstract void Draw(float dt);
    public abstract void Update(float dt);

    public abstract void OnKeyPress(Keys k);
    public abstract void OnKeyRelease(Keys k);

    public abstract void OnMouseDown(int key);
    public abstract void OnMouseUp(int key);
    public abstract void OnMouseMove(Vector2 pos);

    public abstract void Dispose();
}