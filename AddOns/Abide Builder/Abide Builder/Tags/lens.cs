using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("lens", 100)]
	internal sealed class _lens
	{
		[Tag("Bitmap", 32)]
		public sealed class _32_Bitmap
		{
		}
		[TagIdentifier("Bitmap", 36)]
		public sealed class _36_Bitmap
		{
		}
		[TagBlock("Reflections", 64, 48, 32, 4)]
		public sealed class _64_Reflections
		{
		}
		[TagBlock("Brightness", 76, 8, 1, 4)]
		public sealed class _76_Brightness
		{
			[TagBlock("Data", 0, 1, 1024, 4)]
			public sealed class _0_Data
			{
			}
		}
		[TagBlock("Colour", 84, 8, 1, 4)]
		public sealed class _84_Colour
		{
			[TagBlock("Data", 0, 1, 1024, 4)]
			public sealed class _0_Data
			{
			}
		}
		[TagBlock("Rotation", 92, 8, 1, 4)]
		public sealed class _92_Rotation
		{
			[TagBlock("Data", 0, 1, 1024, 4)]
			public sealed class _0_Data
			{
			}
		}
	}
}
