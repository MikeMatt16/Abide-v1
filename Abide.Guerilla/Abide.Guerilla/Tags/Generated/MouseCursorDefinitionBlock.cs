using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("mouse_cursor_definition", "mcsr", "����", typeof(MouseCursorDefinitionBlock))]
	[FieldSet(16, 4)]
	public unsafe struct MouseCursorDefinitionBlock
	{
		[Field("mouse cursor bitmaps", null)]
		[Block("Mouse Cursor Bitmap Reference Block", 4, typeof(MouseCursorBitmapReferenceBlock))]
		public TagBlock MouseCursorBitmaps1;
		[Field("animation speed (fps)", null)]
		public float AnimationSpeedFps2;
	}
}
#pragma warning restore CS1591
