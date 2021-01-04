namespace Abide.DebugXbox
{
    /// <summary>
    /// 
    /// </summary>
    public enum BootType
    {
        /// <summary>
        /// 
        /// </summary>
        Cold,
        /// <summary>
        /// 
        /// </summary>
        Warm,
        /// <summary>
        /// 
        /// </summary>
        NoDebug,
        /// <summary>
        /// 
        /// </summary>
        Wait,
        /// <summary>
        /// 
        /// </summary>
        Stop,
    };

    /// <summary>
    /// Specifies the status of a debug Xbox response.
    /// </summary>
    public enum Status : int
    {
        /// <summary>
        /// Standard response for successful execution of a command.
        /// </summary>
        OK = 200,
        /// <summary>
        /// Initial response sent after a connection is established.
        /// </summary>
        Connected = 201,
        /// <summary>
        /// The response line is followed by one or more additional lines of data terminated by a line containing only a "." (period).
        /// The client must read all available lines before sending another command.
        /// </summary>
        MultilineResponseFollows = 202,
        /// <summary>
        /// The response line is followed by raw binary data, the length of which is indicated in some command-specific way.
        /// The client must read all available data before sending another command.
        /// </summary>
        BinaryResponseFollows = 203,
        /// <summary>
        /// The command is expecting additional binary data from the client. 
        /// After the client sends the required number of bytes, XBDM will send another response line with the final result of the command.
        /// </summary>
        SendBinaryData = 204,
        /// <summary>
        /// The connection has been moved to a dedicated handler thread.
        /// </summary>
        ConnectionDedicated = 205,

        /// <summary>
        /// An internal error occurred that could not be translated to a standard error code.
        /// </summary>
        UnexpectedError = 400,
    }

    /// <summary>
    /// Specifies the drive letter of a debug Xbox drive.
    /// </summary>
    public enum Drive
    {
        /// <summary>
        /// DVD-ROM drive
        /// </summary>
        A,
        /// <summary>
        /// Volume
        /// </summary>
        B,
        /// <summary>
        /// Main Volume
        /// </summary>
        C,
        /// <summary>
        /// Active Title Media
        /// </summary>
        D,
        /// <summary>
        /// Game Development Volume
        /// </summary>
        E,
        /// <summary>
        /// Memory Unit 1A
        /// </summary>
        F,
        /// <summary>
        /// Memory Unit 1B
        /// </summary>
        G,
        /// <summary>
        /// Memory Unit 2A
        /// </summary>
        H,
        /// <summary>
        /// Memory Unit 2B
        /// </summary>
        I,
        /// <summary>
        /// Memory Unit 3A
        /// </summary>
        J,
        /// <summary>
        /// Memory Unit 3B
        /// </summary>
        K,
        /// <summary>
        /// Memory Unit 4A
        /// </summary>
        L,
        /// <summary>
        /// Memory Unit 4B
        /// </summary>
        M,
        /// <summary>
        /// Secondary Active Utility Drive
        /// </summary>
        N,
        /// <summary>
        /// Volume
        /// </summary>
        O,
        /// <summary>
        /// Utility Drive
        /// </summary>
        P,
        /// <summary>
        /// Utility Drive
        /// </summary>
        Q,
        /// <summary>
        /// Utility Drive
        /// </summary>
        R,
        /// <summary>
        /// Persistent Data for all Titles
        /// </summary>
        S,
        /// <summary>
        /// Persistent Data for Active Title
        /// </summary>
        T,
        /// <summary>
        /// Saved Games for Active Title
        /// </summary>
        U,
        /// <summary>
        /// Saved Games for all Titles
        /// </summary>
        V,
        /// <summary>
        /// Persistent Data for Alternative Title
        /// </summary>
        W,
        /// <summary>
        /// Saved Games for Alternate Title
        /// </summary>
        X,
        /// <summary>
        /// Xbox Dashboard Volume
        /// </summary>
        Y,
        /// <summary>
        /// Active Utility Drive
        /// </summary>
        Z
    };
}
