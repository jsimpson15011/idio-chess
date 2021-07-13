import * as React from 'react';
import {connect} from 'react-redux';
import {ApplicationState} from '../../store';
import * as BoardStore from '../../store/ChessBoards';
import {useEffect} from "react";
import './ChessBoard.css';
import {ChessPiece, GameState, Player, UpdateBoardModel} from "../../store/ChessBoards";

// At runtime, Redux will merge together...
type ChessBoardProps =
    BoardStore.GameState // ... state we've requested from the Redux store
    & typeof BoardStore.actionCreators // ... plus action creators we've requested

type ChessBoardSquare =
    BoardStore.ChessBoardSquare


const ChessBoard = (props: ChessBoardProps) => {

    useEffect(() => {
        props.requestBoard();
    }, []);

    const handleColorChoice = (color: string) => {
        const humanPlayer: Player = {
            isActive: color == "white",
            isComputer: false,
            color: color
        }
        const computerPlayer: Player = {
            isActive: !humanPlayer.isActive,
            isComputer: true,
            color: color == "white" ? "black" : "white"
        }

        const updatedGameState: UpdateBoardModel = {
            board: {
                board: props.board,
                player1: humanPlayer.color == "white" ? humanPlayer : computerPlayer,
                player2: humanPlayer.color == "white" ? computerPlayer : humanPlayer,
                activePlayer: humanPlayer.color == "white" ? humanPlayer : computerPlayer,
                activeSquare: props.activeSquare,
                pawnToUpgrade: props.pawnToUpgrade
            },
            currentSquare: null
        }

        props.updateBoard(updatedGameState);
    }

    const handleClick = (square: ChessBoardSquare) => {
        const UpdatedGameState: UpdateBoardModel = {
            board: {
                board: props.board,
                activeSquare: props.activeSquare,
                player1: props.player1,
                player2: props.player2,
                activePlayer: props.activePlayer,
                pawnToUpgrade: props.pawnToUpgrade
            },
            currentSquare: square
        }

        props.updateBoard(UpdatedGameState);
    }

    const handlePromotion = (name: string) => {
        if (props.pawnToUpgrade === null || props.activePlayer === null) return;

        const pieceValues: Record<string, number> = {
            queen: 9,
            rook: 5,
            bishop: 3,
            knight: 3
        }

        const promotedPiece: ChessPiece = {
            value: pieceValues[name],
            name: name,
            color: props.activePlayer.color
        }
        console.log(promotedPiece)

        const updatedSquare: ChessBoardSquare = {
            ...props.pawnToUpgrade,
            piece: promotedPiece
        }

        console.log(updatedSquare)

        const UpdatedGameState: UpdateBoardModel = {
            board: {
                board: props.board,
                activeSquare: props.activeSquare,
                player1: props.player1,
                player2: props.player2,
                activePlayer: props.activePlayer,
                pawnToUpgrade: updatedSquare
            },
            currentSquare: null
        }

        props.updateBoard(UpdatedGameState);
    }


    const activeStyles = {
        background: "#5ff0ff",
        border: "2px solid white",
    }

    if (!props.board) {
        return (
            <></>
        );
    }

    if (props.activePlayer == null) {
        return (
            <div>
                Choose your color
                <button onClick={() => handleColorChoice("white")}>White</button>
                <button onClick={() => handleColorChoice("black")}>Black</button>
            </div>
        );
    }

    if (props.pawnToUpgrade != null) {
        return (
            <div className="piece-upgrade-menu">
                <button onClick={() => handlePromotion("queen")}>Queen</button>
                <button onClick={() => handlePromotion("rook")}>Rook</button>
                <button onClick={() => handlePromotion("bishop")}>Bishop</button>
                <button onClick={() => handlePromotion("knight")}>Knight</button>
            </div>
        )
    }

    return (
        <div className="board">
            {props.board.map(row => {
                return (
                    <div className="board__row" key={row[0].key[0]}>
                        {row.map(square => {
                            return (
                                <div style={square.isActive ? activeStyles : {}} onClick={() => handleClick(square)}
                                     className="board__square" key={square.key}>
                                    {square.canMoveTo ?
                                        <div className="board__move-indicator"/> :
                                        ""}
                                    {square.piece ?
                                        <img src={"/img/" + square.piece.name + "-" + square.piece.color + ".png"}
                                             alt=""/> : ""}
                                </div>
                            )
                        })
                        }
                    </div>
                )


            })}
        </div>
    )
}

export default connect(
    (state: ApplicationState) => state.board, // Selects which state properties are merged into the component's props
    BoardStore.actionCreators // Selects which action creators are merged into the component's props
)(ChessBoard as any);