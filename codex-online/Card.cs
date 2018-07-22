using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameDragAndDrop.DragonDrop;
using System;

public class Card : IDragonDropItem
{
    SpriteBatch spriteBatch;

    public Vector2 Position { get; set; }
    public bool IsSelected { get; set; }
    public bool IsMouseOver { get; set; }
    public Rectangle Border => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
    public bool IsDraggable { get; set; }
    public int ZIndex { get; set; }
    public Texture2D Texture { get; }

    public Card(Texture2D texture, SpriteBatch spriteBatch)
	{
        this.spriteBatch = spriteBatch;
        this.Texture = texture;
        Position = new Vector2(500, 250);
        IsSelected = false;
        IsMouseOver = false;
        IsDraggable = true;
        ZIndex = 1;

    }

    public bool Contains(Vector2 pointToCheck)
    {
        var mouse = new Point((int)pointToCheck.X, (int)pointToCheck.Y);
        return Border.Contains(mouse);
    }

    public void Draw(GameTime gameTime)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }

    public void OnCollusion(IDragonDropItem item)
    {
        
    }

    public void OnDeselected()
    {
        IsSelected = false;
    }

    public void OnSelected()
    {
        IsSelected = true;
    }

    public void Update(GameTime gameTime)
    {
    }
}
