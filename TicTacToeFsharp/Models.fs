module Models

/// <summary>
/// Represents the team's in a game of tic-tac-toe
/// </summary>
type Mark = X | O

/// <summary>
/// Record representation of a two-dimensional discrete coordinate system
/// </summary>
type Position = { X : int; Y : int }

/// <summary>
/// A 2D Array representing the state of a game of tic-tac-toe
/// </summary>
type Board = Mark option [,]