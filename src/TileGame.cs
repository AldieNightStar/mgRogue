using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rogue;

public class TileGame : Game
{
    internal GraphicsDeviceManager _graphics;
    internal SpriteBatch Batch;
    internal TileScene Scene;
    internal TileGraphics Tile;
    internal KeyListener _keyboard;
    internal MouseListener _mouse;
    internal Color BgColor = Color.CornflowerBlue;
    internal GameTime GameTime;
    internal Vector2 MousePos;

    internal Vector2 FontSize;
    private SpriteFont _font;
    
    public SpriteFont Font
    {
        get => _font;
        set
        {
            _font = value;
            FontSize = value.MeasureString("#");
        }
    }

    public TileGame(string title, TileScene scene, int width=800, int height=600)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Scene = scene;
        Window.Title = title;
        Window.AllowUserResizing = true;
        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Batch = new SpriteBatch(GraphicsDevice);
        Tile = new TileGraphics(this, new Vector2(32, 32));
        
        // Listeners
        _keyboard = new KeyListener(Scene.OnKeyPress, Scene.OnKeyRelease);
        _mouse = new MouseListener(this, Scene.OnMouseDown, Scene.OnMouseUp, (pos) =>
        {
            MousePos = pos;
            Scene.OnMouseMove(pos);
        });

        Scene.InitScene(this);
    }

    protected override void Update(GameTime gameTime)
    {
        // Update Time
        GameTime = gameTime;

        // Input
        var keyState = Keyboard.GetState();
        var mouseState = Mouse.GetState();
        _keyboard.Update(keyState);
        _mouse.Update(mouseState);

        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Scene.Update(dt);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        GraphicsDevice.Clear(BgColor);
        Batch.Begin(samplerState: SamplerState.PointClamp);
        Scene.Draw(dt);
        Batch.End();

        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        Batch.Dispose();
        base.Dispose(disposing);
    }

}
