module StockHandler

open Game

let cycleStock (game: Game) =
    match (game.Stock, game.ActiveStock) with
    | ([], active) ->
        { game with
              Stock = List.append game.Wastepile active
              Wastepile = []
              ActiveStock = [] }

    | (stockHead :: stockTail, []) ->
        { game with
              Stock = stockTail
              ActiveStock = [ stockHead ] }

    | (stockHead :: stockTail, [ activeHead; secondActive; thirdActive ]) ->
        { game with
              Stock = stockTail
              ActiveStock = [ secondActive; thirdActive; stockHead ]
              Wastepile = activeHead :: game.Wastepile }

    | (stockHead :: stockTail, active) ->
        { game with
              Stock = stockTail
              ActiveStock = List.append (active) [ stockHead ] }
