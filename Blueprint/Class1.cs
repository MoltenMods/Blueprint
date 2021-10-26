using System;
using Blueprint.Enums.Networking;
using Blueprint.Messages.GameData.C2C;
using Blueprint.Messages.GameData.Rpcs;
using Blueprint.Messages.InnerNetObjects;
using Blueprint.Messages.Objects;
using Singularity.Hazel;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint
{
    public class Class1
    {
        public void Test()
        {
            using var writer = MessageWriter.Get(MessageType.Reliable);
            GameDataC2C.Serialize(
                writer,
                new GameCode(1),
                new RpcC2C(
                    new SkeldShipStatus(
                        1,
                        Array.Empty<SystemType>()),
                    new PlayAnimationRpc(TaskType.Decontaminate)));
        }
    }
}