// Guids.cs
// MUST match guids.h
using System;

namespace Justin.Justin_Stock_VsAddin
{
    static class GuidList
    {
        public const string guidJustin_Stock_VsAddinPkgString = "5284bdc3-2a89-43a1-b98a-43082fbe2280";
        public const string guidJustin_Stock_VsAddinCmdSetString = "b853f963-a50d-4ca0-8eb5-27aac308746b";
        public const string guidToolWindowPersistanceString = "dc998857-17c2-4ead-aebd-c0b6a218728f";

        public static readonly Guid guidJustin_Stock_VsAddinCmdSet = new Guid(guidJustin_Stock_VsAddinCmdSetString);
    };
}