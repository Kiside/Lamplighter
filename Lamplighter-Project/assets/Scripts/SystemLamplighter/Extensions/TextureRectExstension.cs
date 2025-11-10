using Godot;

public static class TextureRectExstension
{
	public static TextureRect
	SettingUp(this TextureRect textureRect,
	Vector2 size, Vector2 minimumSize, Image image,
	TextureRect.ExpandModeEnum expandModeEnum =
	TextureRect.ExpandModeEnum.KeepSize)
	{
		textureRect.Texture = ImageTexture.CreateFromImage(image);
		textureRect.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
		textureRect.Size = new Vector2(14, 14);
		textureRect.CustomMinimumSize = new Vector2(14, 14);
		return textureRect;
	}

	public static TextureRect SetAnchors(this TextureRect textureRect, float AnchorLeft = 0, float AnchorTop = 0, float AnchorRight = 0, float AnchorBottom = 0)
	{
		textureRect.AnchorBottom = AnchorBottom;
		textureRect.AnchorLeft = AnchorLeft;
		textureRect.AnchorTop = AnchorTop;
		textureRect.AnchorRight = AnchorRight;
		return textureRect;
	}
}