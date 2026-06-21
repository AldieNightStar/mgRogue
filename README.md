# Monogame Rogue

## Game lib for roguelike games

```cs
using Game;
using Rogue;

using var game = new TileGame("Game", new SampleScene(), 600, 400);
game.Run();
```

* Game

```cs

namespace Game;

public class SampleScene : TileScene
{
    private Rectangle _table = new Rectangle(263, 812, 50, 50);
    private Texture2D _texture;

    public override void Init()
    {
        BgColor = Color.Pink;
        Tile.Size = new Vector2(100, 100);
        Font = Content.Load<SpriteFont>("font");
        _texture = Content.Load<Texture2D>("texture");
    }

    public override void Update(float dt)
    {
    }

    public override void Draw(float dt)
    {
        var pos = ScreenSize - new Vector2(1, 1);
        Tile.DrawTile(_texture, pos, Color.White, _table, center: true, rotate: Rotation.Left);
    }

    public override void OnKeyPress(Keys k)
    {
    }

    public override void OnKeyRelease(Keys k)
    {
    }

    public override void OnMouseDown(int key)
    {
    }

    public override void OnMouseUp(int key)
    {
    }

    public override void OnMouseMove(Vector2 pos)
    {
    }

    public override void Dispose()
    {

    }
}
```
