module GameState

open System.Collections.Generic
open Game

type GameState private  () =
    let mutable state = Dictionary<string, Game>();
    static let instance = GameState()
    static member Instance = instance

    member _.addGame(game: Game) =
        state.Add(game.Id.ToString(), game)
        ();

    member _.getGames() =
        state.Values;
