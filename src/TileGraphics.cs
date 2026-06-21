using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rogue;

public class TileGraphics
{
    public Vector2 Size;

    private const float PI = (float)Math.PI;
    private const float PI_DIV2 = (float)Math.PI / 2;

    private Vector2 _sizeSwapped;
    private TileGame _game;

    public TileGraphics(TileGame game, Vector2 size)
    {
        _game = game;

        Size = size;
        _sizeSwapped = new Vector2(size.Y, size.X);
    }

    public void DrawString(string text, Vector2 pos, Color? color = null)
    {
        var drawColor = color.HasValue ? color.Value : Color.White;
        var scale = Size / _game.FontSize;
        _game.Batch.DrawString(
            _game.Font, text, pos * Size, drawColor, 0,
            Vector2.Zero, scale, SpriteEffects.None, 0
        );
    }

    public void DrawStringMonospaced(string text, Vector2 pos, Color? color = null)
    {
        foreach (var (x, c) in text.Index())
        {
            var newPos = pos + new Vector2(x, 0);
            DrawString(c.ToString(), newPos, color);
        }
    }

    public void DrawTile(Texture2D texture, Vector2 pos, Color? color = null, Rectangle? srcRect = null, Vector2? scale = null, bool center = false, Rotation rotate = Rotation.Up, float rotation = 0)
    {
        var drawColor = color.HasValue ? color.Value : Color.White;
        var newPos = pos * Size;
        var pscale = scale.HasValue ? scale.Value : Vector2.One;

        var x = (int)newPos.X;
        var y = (int)newPos.Y;
        var w = (int)(Size.X * pscale.X);
        var h = (int)(Size.Y * pscale.Y);
        var halfW = w / 2;
        var halfH = h / 2;
        var origin = _getOrigin(center, texture, srcRect);

        if (center)
        {
            x += w / 2;
            y += h / 2;
        }

        var (newW, newH, newRotation) = _getSwappedSizeBasedOnRotation(w, h, rotate);

        var rect = new Rectangle(x, y, newW, newH);
        _game.Batch.Draw(texture, rect, srcRect, drawColor, newRotation + rotation, origin, SpriteEffects.None, 0);
    }

    public Vector2 GetScreenSizeTiles(GameWindow win)
    {
        var rect = win.ClientBounds;
        var size = new Vector2(rect.Width, rect.Height) / Size;
        size.Floor();
        return size;
    }

    public Vector2 GetTileFromGlobal(Vector2 globalPos)
    {
        var pos = globalPos / Size;
        pos.Floor();
        return pos;
    }

    private Vector2 _getOrigin(bool center, Texture2D texture, in Rectangle? srcRect)
    {
        if (!center) return Vector2.Zero;

        if (srcRect == null)
        {
            var bounds = texture.Bounds;
            return new Vector2(bounds.Width / 2, bounds.Height / 2);
        }
        return new Vector2(srcRect.Value.Width / 2, srcRect.Value.Height / 2);
    }

    private (int w, int h, float rotation) _getSwappedSizeBasedOnRotation(int w, int h, Rotation r)
    {
        if (r == Rotation.Up) return (w, h, 0);
        else if (r == Rotation.Right) return (h, w, PI_DIV2);
        else if (r == Rotation.Down) return (w, h, PI);
        else if (r == Rotation.Left) return (h, w, -PI_DIV2);
        return (w, h, 0);
    }
}