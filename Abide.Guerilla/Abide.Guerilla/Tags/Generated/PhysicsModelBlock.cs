using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("physics_model", "phmo", "����", typeof(PhysicsModelBlock))]
	[FieldSet(396, 4)]
	public unsafe struct PhysicsModelBlock
	{
		public enum Flags0Options
		{
			Unused_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("mass", null)]
		public float Mass1;
		[Field("low freq. deactivation scale#0 is default (1). LESS than 1 deactivates less aggressively. GREATER than 1 is more agressive.", null)]
		public float LowFreqDeactivationScale2;
		[Field("high freq. deactivation scale#0 is default (1). LESS than 1 deactivates less aggressively. GREATER than 1 is more agressive.", null)]
		public float HighFreqDeactivationScale3;
		[Field("", null)]
		public fixed byte _4[24];
		[Field("phantom types", null)]
		[Block("Phantom Types Block", 16, typeof(PhantomTypesBlock))]
		public TagBlock PhantomTypes5;
		[Field("node edges*", null)]
		[Block("Physics Model Node Constraint Edge Block", 4096, typeof(PhysicsModelNodeConstraintEdgeBlock))]
		public TagBlock NodeEdges6;
		[Field("rigid bodies*", null)]
		[Block("Rigid Bodies Block", 64, typeof(RigidBodiesBlock))]
		public TagBlock RigidBodies7;
		[Field("materials*", null)]
		[Block("Materials Block", 64, typeof(MaterialsBlock))]
		public TagBlock Materials8;
		[Field("spheres*", null)]
		[Block("Spheres Block", 1024, typeof(SpheresBlock))]
		public TagBlock Spheres9;
		[Field("multi spheres*", null)]
		[Block("Multi Spheres Block", 1024, typeof(MultiSpheresBlock))]
		public TagBlock MultiSpheres10;
		[Field("pills*", null)]
		[Block("Pills Block", 1024, typeof(PillsBlock))]
		public TagBlock Pills11;
		[Field("boxes*", null)]
		[Block("Boxes Block", 1024, typeof(BoxesBlock))]
		public TagBlock Boxes12;
		[Field("triangles*", null)]
		[Block("Triangles Block", 1024, typeof(TrianglesBlock))]
		public TagBlock Triangles13;
		[Field("polyhedra*", null)]
		[Block("Polyhedra Block", 1024, typeof(PolyhedraBlock))]
		public TagBlock Polyhedra14;
		[Field("polyhedron four vectors*", null)]
		[Block("Polyhedron Four Vectors Block", 4096, typeof(PolyhedronFourVectorsBlock))]
		public TagBlock PolyhedronFourVectors16;
		[Field("polyhedron plane equations*", null)]
		[Block("Polyhedron Plane Equations Block", 1024, typeof(PolyhedronPlaneEquationsBlock))]
		public TagBlock PolyhedronPlaneEquations17;
		[Field("mass distributions*", null)]
		[Block("Mass Distributions Block", 256, typeof(MassDistributionsBlock))]
		public TagBlock MassDistributions18;
		[Field("lists*", null)]
		[Block("Lists Block", 64, typeof(ListsBlock))]
		public TagBlock Lists19;
		[Field("list shapes*", null)]
		[Block("List Shapes Block", 256, typeof(ListShapesBlock))]
		public TagBlock ListShapes20;
		[Field("mopps*", null)]
		[Block("Mopps Block", 64, typeof(MoppsBlock))]
		public TagBlock Mopps21;
		[Field("mopp codes*", null)]
		[Data(1048576)]
		public TagBlock MoppCodes22;
		[Field("hinge constraints*", null)]
		[Block("Hinge Constraints Block", 64, typeof(HingeConstraintsBlock))]
		public TagBlock HingeConstraints23;
		[Field("ragdoll constraints*", null)]
		[Block("Ragdoll Constraints Block", 64, typeof(RagdollConstraintsBlock))]
		public TagBlock RagdollConstraints24;
		[Field("regions*", null)]
		[Block("Regions Block", 16, typeof(RegionsBlock))]
		public TagBlock Regions25;
		[Field("nodes*", null)]
		[Block("Nodes Block", 255, typeof(NodesBlock))]
		public TagBlock Nodes26;
		[Field("import info*", null)]
		[Block("Import Info", 1, typeof(GlobalTagImportInfoBlock))]
		public TagBlock ImportInfo27;
		[Field("errors*", null)]
		[Block("Error Report Category", 64, typeof(GlobalErrorReportCategoriesBlock))]
		public TagBlock Errors28;
		[Field("point to path curves*", null)]
		[Block("Point To Path Curve Block", 64, typeof(PointToPathCurveBlock))]
		public TagBlock PointToPathCurves29;
		[Field("limited hinge constraints*", null)]
		[Block("Limited Hinge Constraints Block", 64, typeof(LimitedHingeConstraintsBlock))]
		public TagBlock LimitedHingeConstraints30;
		[Field("ball and socket constraints*", null)]
		[Block("Ball And Socket Constraints Block", 64, typeof(BallAndSocketConstraintsBlock))]
		public TagBlock BallAndSocketConstraints31;
		[Field("stiff spring constraints*", null)]
		[Block("Stiff Spring Constraints Block", 64, typeof(StiffSpringConstraintsBlock))]
		public TagBlock StiffSpringConstraints32;
		[Field("prismatic constraints*", null)]
		[Block("Prismatic Constraints Block", 64, typeof(PrismaticConstraintsBlock))]
		public TagBlock PrismaticConstraints33;
		[Field("phantoms*", null)]
		[Block("Phantoms Block", 1024, typeof(PhantomsBlock))]
		public TagBlock Phantoms34;
	}
}
#pragma warning restore CS1591
