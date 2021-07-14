module Models

type Mark = X | O
type Position = { X : int; Y : int }
type Board = Mark option [,]