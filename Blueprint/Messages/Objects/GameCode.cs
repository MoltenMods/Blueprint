using System;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.Objects
{
    public readonly struct GameCode : IEquatable<GameCode>
    {
        public GameCode(int value)
        {
            this.Value = value;
            this.Code = GameCodeParser.IntToGameName(value);
        }
        
        public GameCode(string code)
        {
            this.Code = code.ToUpperInvariant();
            this.Value = GameCodeParser.GameNameToInt(code);
        }

        public string Code { get; }
        
        public int Value { get; }

        public bool IsInvalid => this.Value == -1;
        
        public static implicit operator string(GameCode code) => code.Code;

        public static implicit operator int(GameCode code) => code.Value;

        public static implicit operator GameCode(string code) => CreateFrom(code);

        public static implicit operator GameCode(int value) => CreateFrom(value);

        public static GameCode Create()
        {
            return new GameCode(GameCodeParser.GenerateCodeV2());
        }
        
        public static GameCode CreateFrom(int value) => new GameCode(value);

        public static GameCode CreateFrom(string code) => new GameCode(code);

        public static GameCode CreateFrom(IMessageReader reader) => new GameCode(reader.ReadInt32());

        public bool Equals(GameCode other)
        {
            throw new NotImplementedException();
        }
    }
}