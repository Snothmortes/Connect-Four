﻿// <auto-generated />
namespace Connect_Four
{
    // MIT License
    // Copyright (c) 2020 Snothvalp
    // Thought it would be fun to implement the VM part of this game
    // as an educational exercise since I'm new to MVVM.
    // I would have wanted to write unit tests for HasWinner, 
    // but I can't unit test a method called from a static method
    // without mocking/DI, and I'm not going into that at this juncture.
    // Code review would be REALLY appreciated.
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System.Linq;

    public class ConnectFour
    {
        public static string WhoIsWinner(List<string> piecesPositionList)
        {
            var game = new MoveViewModel(piecesPositionList);
            game.Winner = "Draw";
            foreach (var item in piecesPositionList)
            {
                Console.WriteLine(item);
                game.MoveCommand.Execute(item);
                if (game.HasWinner()) break;
            }
            Console.WriteLine(game.Winner);
            return game.Winner;
        }
    }

    internal class Move
    {
        public char Column { get; set; }
        public string Player { get; set; }
    }

    public class MoveViewModel
    {
        private Dictionary<char, string[]> _tiles;
        private List<string> _game;
        private List<Move> _story;
        private Move _lastMove;
        private Move _currentMove;
        public MoveViewModel(List<string> game)
        {
            SetupBoard();
            _story = new List<Move>();
            this._game = game;
        }

        private void SetupBoard()
        {
            _tiles = new Dictionary<char, string[]>();
            for (char c = 'A'; c <= 'G'; c++)
                _tiles.Add(c, new string[6]);
        }

        private ICommand _moveCommand;
        public ICommand MoveCommand
        {
            get
            {
                if (_moveCommand == null) _moveCommand = new RelayCommand(new Action<object>(Move));
                return _moveCommand;
            }
            set { _moveCommand = value; }
        }

        public string Winner { get; internal set; }

        private void Move(object round)
        {
            if (_currentMove == null)
                _currentMove = new Move();
            else
            {
                _lastMove = _currentMove;
                _currentMove = new Move();
            }
            var currentRound = (string)round;
            _currentMove.Column = currentRound[0];
            _currentMove.Player = currentRound.Split('_')[1];

            int i = 0;
            while (true)
            {
                if (_tiles[_currentMove.Column].GetValue(i) != null)
                    i++;
                else
                {
                    _tiles[_currentMove.Column].SetValue(_currentMove.Player, i);
                    break;
                }
            }
            _story.Add(_currentMove);
        }
        public bool HasWinner()
        {
            var result = false;
            if (!(_story.Count < 4))
            {
                List<Func<char, int, List<string>>> funcList = new List<Func<char, int, List<string>>>();
                funcList.Add((x, y) => ColumnExtractor(x, y).ToList());
                funcList.Add((x, y) => RowExtractor(x, y).ToList());
                funcList.Add((x, y) => ForwardDiagExtractor(x, y).ToList());
                funcList.Add((x, y) => BackwardDiagExtractor(x, y).ToList());
                // horizontal and diagonal
                foreach (var item in funcList)
                    for (char c = 'A'; c <= 'G'; c++)
                        for (int i = 0; i < 6; i++)
                        {
                            var func = item.Invoke(c, i);
                            if (func.Count < 4) break;
                            if (func.Count(x => x != null) < 4) continue;
                            result = func.All(x => x == func.First());
                            Winner = result ? func.First() : "Draw";
                            if (result) return result;
                        }
            }
            return result;
        }

        private IEnumerable<string> ColumnExtractor(char column, int row)
        {
            for (int r = row; row <= 2 && r - row < 4; r++)
                yield return _tiles[column][r];
        }

        private IEnumerable<string> RowExtractor(char column, int row)
        {
            for (char c = column; column <= 'D' && c-column < 4; c++)
                yield return _tiles[c][row];
        }

        private IEnumerable<string> ForwardDiagExtractor(char column, int row)
        {
            char c = column;
            int r = row;
            for (; column <= 'D' && c-column < 4 
                && row < 3       && r-row < 4; c++, r++)
                yield return _tiles[c][r];
        }

        private IEnumerable<string> BackwardDiagExtractor(char column, int row)
        {
            char c = column;
            int r = row;
            for (; column >= 'D' && column-c < 4
                && row < 3       && r-row < 4 ; c--, r++)
                yield return _tiles[c][r];
        }
    }
    public class RelayCommand : ICommand
    {
        #region Fields
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        #endregion
        #region Constructors
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new Exception("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion
        #region ICommand interface
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        #endregion
    }
}
