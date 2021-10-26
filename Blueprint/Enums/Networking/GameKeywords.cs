﻿using System;

namespace Blueprint.Enums.Networking
{
    [Flags]
    public enum GameKeywords : uint
    {
        All = 0,
        Other = 1,
        SpanishLA = 2,
        Korean = 4,
        Russian = 8,
        Portuguese = 16,
        Arabic = 32,
        Filipino = 64,
        Polish = 128,
        English = 256,
        Japanese = 512,
        SpanishEU = 1024,
        Brazilian = 2048,
        Dutch = 4096,
        French = 8192,
        German = 16384,
        Italian = 32768,
        SChinese = 65536,
        TChinese = 131072,
        Irish = 262144
    }
}