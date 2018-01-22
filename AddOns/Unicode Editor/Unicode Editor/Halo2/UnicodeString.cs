using System;
using System.Text;

namespace Unicode_Editor.Halo2
{
    internal static class UnicodeString
    {
        private static readonly SpecialCharacter Partner = new SpecialCharacter("<Partner>", 0xee, 0x80, 0x86);
        private static readonly SpecialCharacter Lock = new SpecialCharacter("<Lock>", 0xee, 0x80, 0x88);
        private static readonly SpecialCharacter WhiteColor = new SpecialCharacter("<WhiteColor>", 0xee, 0x80, 0x90);
        private static readonly SpecialCharacter SteelColor = new SpecialCharacter("<SteelColor>", 0xee, 0x80, 0x91);
        private static readonly SpecialCharacter RedColor = new SpecialCharacter("<RedColor>", 0xee, 0x80, 0x92);
        private static readonly SpecialCharacter OrangeColor = new SpecialCharacter("<OrangeColor>", 0xee, 0x80, 0x93);
        private static readonly SpecialCharacter GoldColor = new SpecialCharacter("<GoldColor>", 0xee, 0x80, 0x94);
        private static readonly SpecialCharacter OliveColor = new SpecialCharacter("<OliveColor>", 0xee, 0x80, 0x95);
        private static readonly SpecialCharacter GreenColor = new SpecialCharacter("<GreenColor>", 0xee, 0x80, 0x96);
        private static readonly SpecialCharacter SageColor = new SpecialCharacter("<SageColor>", 0xee, 0x80, 0x97);
        private static readonly SpecialCharacter CyanColor = new SpecialCharacter("<CyanColor>", 0xee, 0x80, 0x98);
        private static readonly SpecialCharacter CobaltColor = new SpecialCharacter("<CobaltColor>", 0xee, 0x80, 0x99);
        private static readonly SpecialCharacter BlueColor = new SpecialCharacter("<BlueColor>", 0xee, 0x80, 0xa0);
        private static readonly SpecialCharacter VioletColor = new SpecialCharacter("<VioletColor>", 0xee, 0x80, 0xa1);
        private static readonly SpecialCharacter PurpleColor = new SpecialCharacter("<PurpleColor>", 0xee, 0x80, 0xa2);
        private static readonly SpecialCharacter PinkColor = new SpecialCharacter("<PinkColor>", 0xee, 0x80, 0xa3);
        private static readonly SpecialCharacter CrimsonColor = new SpecialCharacter("<CrimsonColor>", 0xee, 0x80, 0xa4);
        private static readonly SpecialCharacter BrownColor = new SpecialCharacter("<BrownColor>", 0xee, 0x80, 0xa5);
        private static readonly SpecialCharacter TanColor = new SpecialCharacter("<TanColor>", 0xee, 0x80, 0xa6);
        private static readonly SpecialCharacter TealColor = new SpecialCharacter("<TealColor>", 0xee, 0x80, 0xa7);

        private static readonly SpecialCharacter AButton = new SpecialCharacter("<A>", 0xee, 0x84, 0x80);
        private static readonly SpecialCharacter BButton = new SpecialCharacter("<B>", 0xee, 0x84, 0x81);
        private static readonly SpecialCharacter XButton = new SpecialCharacter("<X>", 0xee, 0x84, 0x82);
        private static readonly SpecialCharacter YButton = new SpecialCharacter("<Y>", 0xee, 0x84, 0x83);
        private static readonly SpecialCharacter Black = new SpecialCharacter("<Black>", 0xee, 0x84, 0x84);
        private static readonly SpecialCharacter White = new SpecialCharacter("<White>", 0xee, 0x84, 0x85);
        private static readonly SpecialCharacter LTrigger = new SpecialCharacter("<L>", 0xee, 0x84, 0x86);
        private static readonly SpecialCharacter RTrigger = new SpecialCharacter("<R>", 0xee, 0x84, 0x87);
        private static readonly SpecialCharacter Start = new SpecialCharacter("<Start>", 0xee, 0x84, 0x8c);
        private static readonly SpecialCharacter Back = new SpecialCharacter("<Back>", 0xee, 0x84, 0x8d);
        private static readonly SpecialCharacter LStick = new SpecialCharacter("<LStick>", 0xee, 0x84, 0x8e);
        private static readonly SpecialCharacter RStick = new SpecialCharacter("<RStick>", 0xee, 0x84, 0x8f);

        private static readonly SpecialCharacter EnergySword = new SpecialCharacter("<EnergySword>", 0xee, 0x84, 0x9f);
        private static readonly SpecialCharacter PlasmaRifle = new SpecialCharacter("<PlasmaRifle>", 0xee, 0x84, 0x9e);
        private static readonly SpecialCharacter BrutePlasmaRifle = new SpecialCharacter("<BrutePlasmaRifle>", 0xee, 0x84, 0xaa);
        private static readonly SpecialCharacter PlasmaPistol = new SpecialCharacter("<PlasmaPistol>", 0xee, 0x84, 0x9d);
        private static readonly SpecialCharacter Carbine = new SpecialCharacter("<Carbine>", 0xee, 0x84, 0x95);
        private static readonly SpecialCharacter Disintigrator = new SpecialCharacter("<Disintigrator>", 0xee, 0x84, 0xa5);
        private static readonly SpecialCharacter BeamRifle = new SpecialCharacter("<BeamRifle>", 0xee, 0x84, 0xa4);
        private static readonly SpecialCharacter BruteShot = new SpecialCharacter("<BruteShot>", 0xee, 0x84, 0x94);
        private static readonly SpecialCharacter FuelRodCannon = new SpecialCharacter("<FuelRodCannon>", 0xee, 0x84, 0x97);
        private static readonly SpecialCharacter BattleRifle = new SpecialCharacter("<BattleRifle>", 0xee, 0x84, 0x92);
        private static readonly SpecialCharacter Needler = new SpecialCharacter("<Needler>", 0xee, 0x84, 0x9b);
        private static readonly SpecialCharacter Magnum = new SpecialCharacter("<Magnum>", 0xee, 0x84, 0x9a);
        private static readonly SpecialCharacter RocketLauncher = new SpecialCharacter("<RocketLauncher>", 0xee, 0x84, 0xa0);
        private static readonly SpecialCharacter Shotgun = new SpecialCharacter("<Shotgun>", 0xee, 0x84, 0xa1);
        private static readonly SpecialCharacter Smg = new SpecialCharacter("<SMG>", 0xee, 0x84, 0xa2);
        private static readonly SpecialCharacter SniperRifle = new SpecialCharacter("<SniperRifle>", 0xee, 0x84, 0xa3);
        private static readonly SpecialCharacter SentinelBeam = new SpecialCharacter("<SentinelBeam>", 0xee, 0x84, 0xa6);
        private static readonly SpecialCharacter SentinelGrenadeLauncher = new SpecialCharacter("<SentinelGrenadeLauncher>", 0xee, 0x84, 0xa8);
        private static readonly SpecialCharacter Bomb = new SpecialCharacter("<Bomb>", 0xee, 0x84, 0x9c);
        private static readonly SpecialCharacter Ball = new SpecialCharacter("<Ball>", 0xee, 0x84, 0x93);
        private static readonly SpecialCharacter Flag = new SpecialCharacter("<Flag>", 0xee, 0x84, 0x96);

        private static readonly SpecialCharacter Player1Name = new SpecialCharacter("<Player1Name>", 0xee, 0x90, 0x8f);
        private static readonly SpecialCharacter Player2Name = new SpecialCharacter("<Player2Name>", 0xee, 0x90, 0x90);
        private static readonly SpecialCharacter Player3Name = new SpecialCharacter("<Player3Name>", 0xee, 0x90, 0x91);
        private static readonly SpecialCharacter Player4Name = new SpecialCharacter("<Player4Name>", 0xee, 0x90, 0x92);
        private static readonly SpecialCharacter OnlineName = new SpecialCharacter("<OnlineName>", 0xee, 0x90, 0xa0);
        private static readonly SpecialCharacter ClanName = new SpecialCharacter("<ClanName>", 0xee, 0x90, 0xa1);
        private static readonly SpecialCharacter Player1Gamertag = new SpecialCharacter("<Player1Gamertag>", 0xee, 0x90, 0xa9);
        private static readonly SpecialCharacter Player2Gamertag = new SpecialCharacter("<Player2Gamertag>", 0xee, 0x90, 0xaa);
        private static readonly SpecialCharacter Player3Gamertag = new SpecialCharacter("<Player3Gamertag>", 0xee, 0x90, 0xab);
        private static readonly SpecialCharacter Player4Gamertag = new SpecialCharacter("<Player4Gamertag>", 0xee, 0x90, 0xac);
        private static readonly SpecialCharacter PlayerScore = new SpecialCharacter("<PlayerScore>", 0xee, 0x90, 0xad);
        private static readonly SpecialCharacter OtherScore = new SpecialCharacter("<OtherScore>", 0xee, 0x90, 0xae);
        private static readonly SpecialCharacter VarientName = new SpecialCharacter("<VarientName>", 0xee, 0x90, 0xaf);
        private static readonly SpecialCharacter TimeLeft = new SpecialCharacter("<TimeLeft>", 0xee, 0x90, 0xb0);
        
        private static readonly SpecialCharacter MoveUDLR = new SpecialCharacter("<MoveUDLR>", 0xee, 0x91, 0x86);
        private static readonly SpecialCharacter Action = new SpecialCharacter("<Action>", 0xee, 0x91, 0x88);
        private static readonly SpecialCharacter Swap = new SpecialCharacter("<Switch>", 0xee, 0x91, 0x89);
        private static readonly SpecialCharacter Melee = new SpecialCharacter("<Melee>", 0xee, 0x91, 0x8a);
        private static readonly SpecialCharacter Flashlight = new SpecialCharacter("<Flashlight>", 0xee, 0x91, 0x8b);
        private static readonly SpecialCharacter Grenade = new SpecialCharacter("<Grenade>", 0xee, 0x91, 0x8c);
        private static readonly SpecialCharacter Fire = new SpecialCharacter("<Fire>", 0xee, 0x91, 0x8d);
        private static readonly SpecialCharacter Crouch = new SpecialCharacter("<Crouch>", 0xee, 0x91, 0x90);
        private static readonly SpecialCharacter Zoom = new SpecialCharacter("<Zoom>", 0xee, 0x91, 0x91);
        private static readonly SpecialCharacter MoveAxis = new SpecialCharacter("<MoveAxis>", 0xee, 0x91, 0x96);
        private static readonly SpecialCharacter LookAxis = new SpecialCharacter("<LookAxis>", 0xee, 0x91, 0x97);
        private static readonly SpecialCharacter OffendingPlayer = new SpecialCharacter("<OffendingPlayer>", 0xee, 0x91, 0x98);
        private static readonly SpecialCharacter Idk = new SpecialCharacter("<Idk>", 0xee, 0x91, 0x9e);
        
        private static readonly SpecialCharacter Count = new SpecialCharacter("<Count>", 0xee, 0x90, 0xa2);

        private static readonly SpecialCharacter[] rawCharacters = new SpecialCharacter[]
        {
            Partner, Lock,
            WhiteColor, SteelColor, RedColor, OrangeColor, GoldColor, OliveColor, GreenColor, SageColor, CyanColor,
            CobaltColor, BlueColor, VioletColor, PurpleColor, PinkColor, CrimsonColor, BrownColor, TanColor, TealColor,

            AButton, BButton, XButton, YButton, Black, White, LTrigger, RTrigger, Start, Back, LStick, RStick,
            Player1Name, Player2Name, Player3Name, Player4Name, OnlineName, ClanName, Player1Gamertag, Player2Gamertag, Player3Gamertag, Player4Gamertag,
            PlayerScore, OtherScore, VarientName, TimeLeft,

            MoveUDLR, Action, Swap, Melee, Flashlight, Grenade, Fire, Crouch, Zoom, MoveAxis, LookAxis, OffendingPlayer, Idk,

            EnergySword, PlasmaRifle, BrutePlasmaRifle, PlasmaPistol, Carbine, Disintigrator, BeamRifle, BruteShot, FuelRodCannon,
            BattleRifle, Needler, Magnum, RocketLauncher, Shotgun, Smg, SniperRifle, SentinelBeam, SentinelGrenadeLauncher, Bomb, Ball, Flag,

            Count,
        };

        public static string ConvertToReadable(string str)
        {
            string readable = str;
            foreach (SpecialCharacter @char in rawCharacters) readable = @char.Decode(readable);
            return readable;
        }
        public static string ConvertToUnicode(string str)
        {
            string raw = str;
            foreach (SpecialCharacter @char in rawCharacters) @char.Encode(ref raw);
            return raw;
        }

        /// <summary>
        /// Represents a character converter.
        /// </summary>
        private struct SpecialCharacter
        {
            private readonly string encodedValue;
            private readonly string decodedValue;

            /// <summary>
            /// Initializes a new instance of the <see cref="SpecialCharacter"/> class using the specified readable string and raw character bytes.
            /// </summary>
            /// <param name="readable">The readable string.</param>
            /// <param name="characterBytes">The raw character bytes.</param>
            public SpecialCharacter(string readable, params byte[] characterBytes)
            {
                //Check
                if (characterBytes == null) throw new ArgumentNullException(nameof(characterBytes));

                //Set
                decodedValue = readable ?? throw new ArgumentNullException(nameof(readable));
                encodedValue = Encoding.UTF8.GetString(characterBytes);
            }
            /// <summary>
            /// Decodes a specified string, replacing the raw character(s) with readable character(s).
            /// </summary>
            /// <param name="str">The raw string.</param>
            /// <returns>A readable string.</returns>
            public string Decode(string str)
            {
                return str.Replace(encodedValue, decodedValue);
            }
            /// <summary>
            /// Encodes a specified string, replacing the readable character with the raw character.
            /// </summary>
            /// <param name="str">The string to encode.</param>
            public void Encode(ref string str)
            {
                str = str.Replace(decodedValue, encodedValue);
            }
            /// <summary>
            /// Gets a string representation of this character.
            /// </summary>
            /// <returns>A string.</returns>
            public override string ToString()
            {
                return $"{decodedValue} -> {encodedValue}";
            }
        }
    }
}
