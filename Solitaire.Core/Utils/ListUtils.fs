module ListUtils


let replaceAt index newValue list =
    list
    |> List.indexed
    |> List.map (fun (idx, value) -> if idx = index then newValue else value)

