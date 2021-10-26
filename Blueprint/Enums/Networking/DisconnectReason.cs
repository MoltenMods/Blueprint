namespace Blueprint.Enums.Networking
{
    public enum DisconnectReason
    {
        ExitGame = 0,
        
        // The game you tried to join is full.
        // Check with the host to see if you can join next round.
        GameFull = 1,
        
        // The game you tried to join already started.
        // Check with the host to see if you can join next round.
        GameStarted = 2,
        
        // Could not find the game you're looking for..
        GameNotFound = 3,
        
        // You are running an older version of the game.
        // Please update to play with others.
        IncorrectVersion = 5,
        
        // You were banned from { GameCode ?? "the room" }
        // You cannot rejoin that room.
        Banned = 6,
        
        // You were kicked from { GameCode ?? "the room" }
        // You can rejoin if the room hasn't started.
        Kicked = 7,
        
        // For this, a message can be given; specifying an empty message shows
        // "An unknown error disconnected you from the server."
        Custom = 8,
        
        InvalidName = 9,
        
        // You were banned for hacking.
        // Please stop.
        Hacking = 10,
        
        NotAuthorized = 11,
        Destroy = 16,
        
        // GameModes.LocalGame:
        // You disconnected from the host.
        // If this happens often, check your WiFi strength.
        //
        // GameModes.OnlineGame:
        // You disconnected from the server.
        // If this happens often, check your network strength.
        // This may also be a server issue.
        Error = 17,
        
        // Could not find the game you're looking for..
        IncorrectGame = 18,
        
        // The server stopped this game. Possibly due to inactivity.
        ServerRequest = 19,
        
        // The Among Us servers are overloaded.
        // Sorry! Please try again later!
        ServerFull = 20,
        
        FocusLostBackground = 207,
        
        // You may not join another game for another { BanMinutesLeft } minutes after intentionally disconnecting.
        IntentionalLeaving = 208,
        
        // You were disconnected because Among Us was suspended by another app.
        FocusLost = 209,
        
        NewConnection = 210
    }
}