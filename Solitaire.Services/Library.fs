namespace Solitaire.Services
open GameState

module Say =
    let hello name =
        let t = GameState.Instance;
        printfn "Hello %s" name
