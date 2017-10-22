using Abide.Builder.Tags.TagDefinition;
namespace Abide.Builder.Tags
{
	[TagDefinition("sky ", 172)]
	internal sealed class _sky_
	{
		[Tag("Render Model", 0)]
		public sealed class _0_Render_Model
		{
		}
		[TagIdentifier("Render Model", 4)]
		public sealed class _4_Render_Model
		{
		}
		[Tag("Animation Graph", 8)]
		public sealed class _8_Animation_Graph
		{
		}
		[TagIdentifier("Animation Graph", 12)]
		public sealed class _12_Animation_Graph
		{
		}
		[TagBlock("Cube Map", 28, 12, 1, 4)]
		public sealed class _28_Cube_Map
		{
			[Tag("Cube Map Reference", 0)]
			public sealed class _0_Cube_Map_Reference
			{
			}
			[TagIdentifier("Cube Map Reference", 4)]
			public sealed class _4_Cube_Map_Reference
			{
			}
		}
		[TagBlock("Atmospheric Fog", 72, 24, 1, 4)]
		public sealed class _72_Atmospheric_Fog
		{
		}
		[TagBlock("Secondary Fog", 80, 24, 1, 4)]
		public sealed class _80_Secondary_Fog
		{
		}
		[TagBlock("Sky Fog", 88, 16, 1, 4)]
		public sealed class _88_Sky_Fog
		{
		}
		[TagBlock("Patchy Fog", 96, 80, 1, 4)]
		public sealed class _96_Patchy_Fog
		{
			[Tag("Patchy Fog", 72)]
			public sealed class _72_Patchy_Fog
			{
			}
			[TagIdentifier("Patchy Fog", 76)]
			public sealed class _76_Patchy_Fog
			{
			}
		}
		[TagBlock("Lights", 120, 52, 8, 4)]
		public sealed class _120_Lights
		{
			[Tag("Lens Flare", 20)]
			public sealed class _20_Lens_Flare
			{
			}
			[TagIdentifier("Lens Flare", 24)]
			public sealed class _24_Lens_Flare
			{
			}
			[TagBlock("Fog", 28, 44, 1, 4)]
			public sealed class _28_Fog
			{
			}
			[TagBlock("Fog Opposite", 36, 44, 1, 4)]
			public sealed class _36_Fog_Opposite
			{
			}
			[TagBlock("Radiosity", 44, 40, 1, 4)]
			public sealed class _44_Radiosity
			{
			}
		}
		[TagBlock("Shader Functions", 132, 36, 8, 4)]
		public sealed class _132_Shader_Functions
		{
		}
		[TagBlock("Animations", 140, 36, 8, 4)]
		public sealed class _140_Animations
		{
		}
	}
}
