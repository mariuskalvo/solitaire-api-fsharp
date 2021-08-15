module GameMoveWeb

[<Literal>]
let FoundationWeb = "FOUNDATION"

[<Literal>]
let ActiveStockWeb = "ACTIVE_STOCK"

[<Literal>]
let TableauWeb = "TABLEAU"


type MoveWeb =
    { Source: string
      SourceIndex: Option<int>
      Destination: string
      DestinationIndex: Option<int> }
