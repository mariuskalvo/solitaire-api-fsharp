module MoveHandler

open Game

type DestinationMove = { DestIndex: int }
type SourceDestinationMove = { SrcIndex: int; DestIndex: int }

type MoveCommand =
    | ActiveToTableau of DestinationMove
    | ActiveToFoundations of DestinationMove
    | TableauToFoundation of SourceDestinationMove
    | FoundationsToTableau of SourceDestinationMove

type CardList =
    | CardList2D of Card list list
    | CardList1D of Card list

type CardArea =
    | Tableau of Card list list * int
    | Foundations of Card list list * int
    | ActiveCards of Card list

type MoveAction =
    { Source: CardArea
      Destination: CardArea }

let private pop2dCardList (cards: Card list list, index: int) : Card Option * Card list list =
    let column = List.tryItem (index) cards

    match column with
    | None
    | Some [] -> None, cards
    | Some (x :: xs) -> Some(x), ListUtils.replaceAt (cards, index, xs)

let private pop1dCardList (cards: Card list) : Card Option * Card list =
    match cards with
    | [] -> None, cards
    | x :: xs -> Some(x), xs

let private push2dCardList (card: Card, cards: Card list list, index: int) : Card list list =
    let column = List.tryItem (index) cards

    match column with
    | None
    | Some [] -> cards
    | Some column ->
        let newColumn = card :: column
        ListUtils.replaceAt (cards, index, newColumn)


type IndexedList2D = Card list list * int
type List2D = Card list list


let private moveToIdenticalCardArea ((src, i): IndexedList2D, (dest, j): IndexedList2D) : List2D =
    let (card, newSrc) = pop2dCardList (src, i)

    match card with
    | None -> src
    | Some c -> push2dCardList (c, newSrc, j)


let private moveNestedToNested ((src, i): IndexedList2D, (dest, j): IndexedList2D) : List2D * List2D =
    let (card, newSrc) = pop2dCardList (src, i)

    match card with
    | None -> src, dest
    | Some c ->
        let newDest = push2dCardList (c, dest, j)
        newSrc, newDest


let private moveSingleToNested (src: Card list, (dest, j): Card list list * int) : Card list * List2D =
    match src with
    | [] -> src, dest
    | x :: xs ->
        let newDest = push2dCardList (x, dest, j)
        xs, newDest

let move (game: Game, source: CardArea, destination: CardArea) =
    match (source, destination) with
    | (Tableau (src, i), Tableau (dest, j)) ->
        let newTab =
            moveToIdenticalCardArea ((src, i), (dest, j))

        { game with Tableau = newTab }

    | (Tableau (src, i), Foundations (dest, j)) ->
        let (newTableau, newFoundations) = moveNestedToNested ((src, i), (dest, j))

        { game with
              Tableau = newTableau
              Foundations = newFoundations }

    | (Foundations (src, i), Foundations (dest, j)) ->
        let newFnd =
            moveToIdenticalCardArea ((src, i), (dest, j))

        { game with Tableau = newFnd }

    | (Foundations (src, i), Tableau (dest, j)) ->
        let (newFoundations, newTableau) = moveNestedToNested ((src, i), (dest, j))

        { game with
              Foundations = newFoundations
              Tableau = newTableau }

    | (ActiveCards (src), Tableau (dest, j)) ->
        let (newActive, newTableau) = moveSingleToNested (src, (dest, j))

        { game with
              ActiveStock = newActive
              Tableau = newTableau }

    | (ActiveCards (src), Foundations (dest, j)) ->
        let (newActive, newFoundations) = moveSingleToNested (src, (dest, j))

        { game with
              ActiveStock = newActive
              Foundations = newFoundations }
    | _ -> game



let private moveFromActiveToTableau (game: Game, tableauIndex: int) =
    match game.ActiveStock with
    | [] -> game
    | aHead :: aTail ->
        let newTableauColumn = aHead :: game.Tableau.[tableauIndex]

        let newTableau =
            ListUtils.replaceAt (game.Tableau, tableauIndex, newTableauColumn)

        { game with
              Tableau = newTableau
              ActiveStock = aTail }


let private moveFromActiveToFoundations (game, destIndex) =
    match game.ActiveStock with
    | [] -> game
    | stockHead :: stockTail ->
        let newDestFoundation =
            stockHead :: game.Foundations.[destIndex]

        let newFoundations =
            ListUtils.replaceAt (game.Foundations, destIndex, newDestFoundation)

        { game with
              Foundations = newFoundations
              ActiveStock = stockTail }

let private moveFromTableauToFoundations (game, srcIndex, destIndex) =
    let tableauColumn = game.Tableau.[srcIndex]
    let foundation = game.Foundations.[destIndex]

    match tableauColumn with
    | [] -> game
    | tabHead :: tabTail ->
        let newFoundation = tabHead :: foundation

        let newFoundations =
            ListUtils.replaceAt (game.Foundations, destIndex, newFoundation)

        let newTableau =
            ListUtils.replaceAt (game.Tableau, srcIndex, tabTail)

        { game with
              Foundations = newFoundations
              Tableau = newTableau }


let private moveFromFoundationsToTableau (game, srcIndex, destIndex) = game

let handleMove (game: Game, moveCommand: MoveCommand) : Game =
    match moveCommand with
    | ActiveToTableau s -> moveFromActiveToTableau (game, s.DestIndex)
    | ActiveToFoundations s -> moveFromActiveToFoundations (game, s.DestIndex)
    | TableauToFoundation s -> moveFromTableauToFoundations (game, s.SrcIndex, s.DestIndex)
    | FoundationsToTableau s -> moveFromFoundationsToTableau (game, s.SrcIndex, s.DestIndex)
