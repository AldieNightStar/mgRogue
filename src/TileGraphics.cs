using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue;

public class TileGraphics
{
    public Vector2 Size;
    private TileGame _game;

    public TileGraphics(TileGame game, Vector2 size)
    {
        _game = game;
        Size = size;
    }

    public void DrawString(string text, Vector2 pos, Color? color = null)
    {
        var drawColor = color.HasValue ? color.Value : Color.White;
        var scale = Size/_game.FontSize;
        _game.Batch.DrawString(
            _game.Font, text, pos * Size, drawColor, 0,
            Vector2.Zero, scale, SpriteEffects.None, 0
        );
    }

    public void DrawStringMonospaced(string text, Vector2 pos, Color? color= null)
    {
        foreach (var (x, c) in text.Index())
        {
            var newPos = pos + new Vector2(x, 0);
            DrawString(c.ToString(), newPos, color);
        }
    }

    public void DrawTile(Texture2D texture, Vector2 pos, Color? color = null, Rectangle? srcRect = null)
    {
        var drawColor = color.HasValue ? color.Value : Color.White;
        var newPos = pos * Size;
        var rect = new Rectangle((int)newPos.X, (int)newPos.Y, (int)Size.X, (int)Size.Y);
        _game.Batch.Draw(
            texture, rect, srcRect, drawColor, 0,
            Vector2.Zero, SpriteEffects.None, 0
        );
    }

    public Vector2 GetScreenSizeTiles(GameWindow win)
    {
        var rect = win.ClientBounds;
        var size = new Vector2(rect.Width, rect.Height) / Size;
        size.Floor();
        return size;
    }
}