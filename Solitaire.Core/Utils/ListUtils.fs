module ListUtils


    

let replaceAt (list: 'a list, index: int, newValue: 'a) =
    
    list
    |> List.indexed
    |> List.map (fun (idx, value) -> if idx = index then newValue else value)
