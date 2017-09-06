using System;
using System.Text;

namespace Unicode_Editor.Halo2
{
    public static class UnicodeString
    {
        private static readonly Character Partner = new Character("<Partner>", 0xee, 0x80, 0x86);
        private static readonly Character Lock = new Character("<Lock>", 0xee, 0x80, 0x88);
        private static readonly Character WhiteColor = new Character("<WhiteColor>", 0xee, 0x80, 0x90);
        private static readonly Character SteelColor = new Character("<SteelColor>", 0xee, 0x80, 0x91);
        private static readonly Character RedColor = new Character("<RedColor>", 0xee, 0x80, 0x92);
        private static readonly Character OrangeColor = new Character("<OrangeColor>", 0xee, 0x80, 0x93);
        private static readonly Character GoldColor = new Character("<GoldColor>", 0xee, 0x80, 0x94);
        private static readonly Character OliveColor = new Character("<OliveColor>", 0xee, 0x80, 0x95);
        private static readonly Character GreenColor = new Character("<GreenColor>", 0xee, 0x80, 0x96);
        private static readonly Character SageColor = new Character("<SageColor>", 0xee, 0x80, 0x97);
        private static readonly Character CyanColor = new Character("<CyanColor>", 0xee, 0x80, 0x98);
        private static readonly Character CobaltColor = new Character("<CobaltColor>", 0xee, 0x80, 0x99);
        private static readonly Character BlueColor = new Character("<BlueColor>", 0xee, 0x80, 0xa0);
        private static readonly Character VioletColor = new Character("<VioletColor>", 0xee, 0x80, 0xa1);
        private static readonly Character PurpleColor = new Character("<PurpleColor>", 0xee, 0x80, 0xa2);
        private static readonly Character PinkColor = new Character("<PinkColor>", 0xee, 0x80, 0xa3);
        private static readonly Character CrimsonColor = new Character("<CrimsonColor>", 0xee, 0x80, 0xa4);
        private static readonly Character BrownColor = new Character("<BrownColor>", 0xee, 0x80, 0xa5);
        private static readonly Character TanColor = new Character("<TanColor>", 0xee, 0x80, 0xa6);
        private static readonly Character TealColor = new Character("<TealColor>", 0xee, 0x80, 0xa7);

        private static readonly Character AButton = new Character("<A>", 0xee, 0x84, 0x80);
        private static readonly Character BButton = new Character("<B>", 0xee, 0x84, 0x81);
        private static readonly Character XButton = new Character("<X>", 0xee, 0x84, 0x82);
        private static readonly Character YButton = new Character("<Y>", 0xee, 0x84, 0x83);
        private static readonly Character Black = new Character("<Black>", 0xee, 0x84, 0x84);
        private static readonly Character White = new Character("<White>", 0xee, 0x84, 0x85);
        private static readonly Character LTrigger = new Character("<L>", 0xee, 0x84, 0x86);
        private static readonly Character RTrigger = new Character("<R>", 0xee, 0x84, 0x87);
        private static readonly Character Start = new Character("<Start>", 0xee, 0x84, 0x8c);
        private static readonly Character Back = new Character("<Back>", 0xee, 0x84, 0x8d);
        private static readonly Character LStick = new Character("<LStick>", 0xee, 0x84, 0x8e);
        private static readonly Character RStick = new Character("<RStick>", 0xee, 0x84, 0x8f);

        private static readonly Character Player1Name = new Character("<Player1Name>", 0xee, 0x90, 0x8f);
        private static readonly Character Player2Name = new Character("<Player2Name>", 0xee, 0x90, 0x90);
        private static readonly Character Player3Name = new Character("<Player3Name>", 0xee, 0x90, 0x91);
        private static readonly Character Player4Name = new Character("<Player4Name>", 0xee, 0x90, 0x92);
        private static readonly Character Player1Gamertag = new Character("<Player1Gamertag>", 0xee, 0x90, 0xa9);
        private static readonly Character Player2Gamertag = new Character("<Player2Gamertag>", 0xee, 0x90, 0xaa);
        private static readonly Character Player3Gamertag = new Character("<Player3Gamertag>", 0xee, 0x90, 0xab);
        private static readonly Character Player4Gamertag = new Character("<Player4Gamertag>", 0xee, 0x90, 0xac);
        private static readonly Character PlayerScore = new Character("<PlayerScore>", 0xee, 0x90, 0xad);
        private static readonly Character OtherScore = new Character("<OtherScore>", 0xee, 0x90, 0xae);
        private static readonly Character VarientName = new Character("<VarientName>", 0xee, 0x90, 0xaf);
        private static readonly Character TimeLeft = new Character("<TimeLeft>", 0xee, 0x90, 0xb0);
        
        private static readonly Character MoveUDLR = new Character("<MoveUDLR>", 0xee, 0x91, 0x86);
        private static readonly Character Action = new Character("<Action>", 0xee, 0x91, 0x88);
        private static readonly Character Swap = new Character("<Switch>", 0xee, 0x91, 0x89);
        private static readonly Character Melee = new Character("<Melee>", 0xee, 0x91, 0x8a);
        private static readonly Character Flashlight = new Character("<Flashlight>", 0xee, 0x91, 0x8b);
        private static readonly Character Grenade = new Character("<Grenade>", 0xee, 0x91, 0x8c);
        private static readonly Character Fire = new Character("<Fire>", 0xee, 0x91, 0x8d);
        private static readonly Character Crouch = new Character("<Crouch>", 0xee, 0x91, 0x90);
        private static readonly Character Zoom = new Character("<Zoom>", 0xee, 0x91, 0x91);
        private static readonly Character MoveAxis = new Character("<MoveAxis>", 0xee, 0x91, 0x96);
        private static readonly Character LookAxis = new Character("<LookAxis>", 0xee, 0x91, 0x97);
        private static readonly Character OffendingPlayer = new Character("<OffendingPlayer>", 0xee, 0x91, 0x98);
        private static readonly Character Idk = new Character("<Idk>", 0xee, 0x91, 0x9e);

        private static readonly Character EnergySword = new Character("<EnergySword>", 0xee, 0x84, 0x9f);
        private static readonly Character PlasmaRifle = new Character("<PlasmaRifle>", 0xee, 0x84, 0x9e);
        private static readonly Character BrutePlasmaRifle = new Character("<BrutePlasmaRifle>", 0xee, 0x84, 0xaa);
        private static readonly Character PlasmaPistol = new Character("<PlasmaPistol>", 0xee, 0x84, 0x9d);
        private static readonly Character Carbine = new Character("<Carbine>", 0xee, 0x84, 0x95);
        private static readonly Character Disintigrator = new Character("<Disintigrator>", 0xee, 0x84, 0xa5);
        private static readonly Character BeamRifle = new Character("<BeamRifle>", 0xee, 0x84, 0xa4);
        private static readonly Character BruteShot = new Character("<BruteShot>", 0xee, 0x84, 0x94);
        private static readonly Character FuelRodCannon = new Character("<FuelRodCannon>", 0xee, 0x84, 0x97);
        private static readonly Character BattleRifle = new Character("<BattleRifle>", 0xee, 0x84, 0x92);
        private static readonly Character Needler = new Character("<Needler>", 0xee, 0x84, 0x9b);
        private static readonly Character Magnum = new Character("<Magnum>", 0xee, 0x84, 0x9a);
        private static readonly Character RocketLauncher = new Character("<RocketLauncher>", 0xee, 0x84, 0xa0);
        private static readonly Character Shotgun = new Character("<Shotgun>", 0xee, 0x84, 0xa1);
        private static readonly Character Smg = new Character("<SMG>", 0xee, 0x84, 0xa2);
        private static readonly Character SniperRifle = new Character("<SniperRifle>", 0xee, 0x84, 0xa3);
        private static readonly Character SentinelBeam = new Character("<SentinelBeam>", 0xee, 0x84, 0xa6);
        private static readonly Character SentinelGrenadeLauncher = new Character("<SentinelGrenadeLauncher>", 0xee, 0x84, 0xa8);
        private static readonly Character Bomb = new Character("<Bomb>", 0xee, 0x84, 0x9c);
        private static readonly Character Ball = new Character("<Ball>", 0xee, 0x84, 0x93);
        private static readonly Character Flag = new Character("<Flag>", 0xee, 0x84, 0x96);

        private static readonly Character Count = new Character("<Count>", 0xee, 0x90, 0xa2);

        private static readonly Character[] rawCharacters = new Character[]
        {
            Partner, Lock,
            WhiteColor, SteelColor, RedColor, OrangeColor, GoldColor, OliveColor, GreenColor, SageColor, CyanColor,
            CobaltColor, BlueColor, VioletColor, PurpleColor, PinkColor, CrimsonColor, BrownColor, TanColor, TealColor,

            AButton, BButton, XButton, YButton, Black, White, LTrigger, RTrigger, Start, Back, LStick, RStick,
            Player1Name, Player2Name, Player3Name, Player4Name, Player1Gamertag, Player2Gamertag, Player3Gamertag, Player4Gamertag,
            PlayerScore, OtherScore, VarientName, TimeLeft,

            MoveUDLR, Action, Swap, Melee, Flashlight, Grenade, Fire, Crouch, Zoom, MoveAxis, LookAxis, OffendingPlayer, Idk,

            EnergySword, PlasmaRifle, BrutePlasmaRifle, PlasmaPistol, Carbine, Disintigrator, BeamRifle, BruteShot, FuelRodCannon,
            BattleRifle, Needler, Magnum, RocketLauncher, Shotgun, Smg, SniperRifle, SentinelBeam, SentinelGrenadeLauncher, Bomb, Ball, Flag,

            Count,
        };

        public static string ConvertToReadable(string str)
        {
            string readable = str;
            foreach (Character @char in rawCharacters) readable = @char.Decode(readable);
            return readable;
        }
        public static string ConvertToUnicode(string str)
        {
            string raw = str;
            foreach (Character @char in rawCharacters) @char.Encode(ref raw);
            return raw;
        }

        /// <summary>
        /// Represents a character converter.
        /// </summary>
        private struct Character
        {
            private readonly string encodedValue;
            private readonly string decodedValue;

            /// <summary>
            /// Initializes a new instance of the <see cref="Character"/> class using the specified readable string and raw character bytes.
            /// </summary>
            /// <param name="readable">The readable string.</param>
            /// <param name="characterBytes">The raw character bytes.</param>
            public Character(string readable, params byte[] characterBytes)
            {
                //Check
                if (characterBytes == null) throw new ArgumentNullException(nameof(characterBytes));
                if (readable == null) throw new ArgumentNullException(nameof(readable));

                //Set
                decodedValue = readable;
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
                str.Replace(decodedValue, encodedValue);
            }
        }
    }
}
