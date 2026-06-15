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
    internal KeyListener Keys;
    internal Color BgColor = Color.CornflowerBlue;

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

    public TileGame(string title, TileScene scene)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Scene = scene;
        Window.Title = title;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Batch = new SpriteBatch(GraphicsDevice);
        Tile = new TileGraphics(this, new Vector2(32, 32));
        Keys = new KeyListener(Scene.OnKeyPress, Scene.OnKeyRelease);

        Scene.InitScene(this);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            Exit();

        var keyState = Keyboard.GetState();
        Keys.Update(keyState);

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
