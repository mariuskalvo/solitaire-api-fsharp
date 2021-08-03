module GameDboMapper

open Solitaire.Infrastructure.Models
open CardDboMapper
open Game


let mapGameDboToGame(game: GameDbo): Game =
    
    let stock = List.map mapCardDboToCard (List.ofSeq(game.Stock))
    let active = List.map mapCardDboToCard (List.ofSeq(game.Active))
    let wastepile = List.map mapCardDboToCard (List.ofSeq(game.Wastepile))

    let tableau = 
        (List.ofSeq(game.Tableau)
        |> List.map (fun column -> List.map mapCardDboToCard (List.ofSeq(column))))

    let foundations = 
        (List.ofSeq(game.Foundations)
        |> List.map (fun column -> List.map mapCardDboToCard (List.ofSeq(column))))

    {
        Stock = stock
        ActiveStock = active
        Wastepile = wastepile
        Tableau = tableau
        Foundations = foundations
    }

let mapGameToGameDbo(game: Game): GameDbo =
    let stock =
        game.Stock
        |> List.map mapCardToCardDbo
        |> ResizeArray

    let activeStock =
        game.ActiveStock
        |> List.map mapCardToCardDbo
        |> ResizeArray

    let wastepile =
        game.Wastepile
        |> List.map mapCardToCardDbo
        |> ResizeArray

    let tableau =
        game.Tableau
        |> List.map(fun column ->
            column
            |> List.map (mapCardToCardDbo)
            |> ResizeArray
        )
        |> ResizeArray

    let foundations =
        game.Foundations
        |> List.map(fun column ->
            column
            |> List.map (mapCardToCardDbo)
            |> ResizeArray
        )
        |> ResizeArray

    GameDbo(stock, activeStock, wastepile, tableau, foundations)