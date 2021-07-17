import * as React from 'react';
import {connect} from 'react-redux';
import {ApplicationState} from '../../store';
import * as BoardStore from '../../store/ChessBoards';
import {useEffect, useState} from "react";
import './ChessBoard.css';
import {ChessPiece, ChessBoardState, Player, UpdateBoardModel} from "../../store/ChessBoards";
import {Button, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";

// At runtime, Redux will merge together...
type ChessBoardProps =
    BoardStore.ChessBoardState // ... state we've requested from the Redux store
    & typeof BoardStore.actionCreators // ... plus action creators we've requested

type ChessBoardSquare =
    BoardStore.ChessBoardSquare


const ChessPrompt = (props: ChessBoardProps) => {

    const [modalIsShowing, setModal] = useState(false);

    useEffect(() => {
        setModal(isModalShowing);
    }, [props.player1]);

    const handleColorChoice = async (color: string) => {
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
                pawnToUpgrade: props.pawnToUpgrade,
                gameState: {state: null, player: null}
            },
            currentSquare: null
        }

        props.updateBoard(updatedGameState);
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

        const updatedSquare: ChessBoardSquare = {
            ...props.pawnToUpgrade,
            piece: promotedPiece
        }

        const UpdatedGameState: UpdateBoardModel = {
            board: {
                board: props.board,
                activeSquare: props.activeSquare,
                player1: props.player1,
                player2: props.player2,
                activePlayer: props.activePlayer,
                pawnToUpgrade: updatedSquare,
                gameState: {state: null, player: null}
            },
            currentSquare: null
        }

        props.updateBoard(UpdatedGameState);
    }

    const isModalShowing = () => {
        return (props.player1 == null || props.pawnToUpgrade != null || (props.gameState != null && props.gameState.state != null));
    }

    let gameOverMessage = "<></>";
    
    if(props.gameState.state != null)
    {
        if(props.gameState.state == "Draw")
        {
            gameOverMessage = "Draw"
        }
        if(props.gameState.state == "Lose" && props.gameState.player != null)
        {
            gameOverMessage = props.gameState.player.isComputer ? "Player Victory" : "Player Defeat";
        }
    }
    
    const screens = {
        colorChoice:
            <Modal isOpen={modalIsShowing}>
                <ModalBody>
                    Choose Your Color
                </ModalBody>
                <ModalFooter>
                    <Button size="lg" outline onClick={() => handleColorChoice("white")}>White</Button>
                    <Button size="lg" onClick={() => handleColorChoice("black")}>Black</Button>
                </ModalFooter>
            </Modal>,
        
        promotion:
            <Modal isOpen={modalIsShowing}>
                <ModalBody>Choose Your Piece</ModalBody>
                <ModalFooter>
                    <Button onClick={() => handlePromotion("queen")}>Queen</Button>
                    <Button onClick={() => handlePromotion("rook")}>Rook</Button>
                    <Button onClick={() => handlePromotion("bishop")}>Bishop</Button>
                    <Button onClick={() => handlePromotion("knight")}>Knight</Button>
                </ModalFooter>
            </Modal>,
        
        gameOver:
            <Modal isOpen={modalIsShowing}>
                <ModalHeader>
                    <h2>Game Over</h2>
                </ModalHeader>
                <ModalBody>
                    {gameOverMessage}
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" size="lg" onClick={() => props.requestBoard()}>Play Again</Button>
                </ModalFooter>
            </Modal>
    }
    let currentScreen = <></>;

    if (props.player1 == null) {
        currentScreen = screens.colorChoice
    }

    if (props.pawnToUpgrade != null) {
        currentScreen = screens.promotion
    }

    if (props.gameState.state != null) {
        currentScreen = screens.gameOver
    }


    return (currentScreen)
}

const ChessBoard = (props: ChessBoardProps) => {

    const [modalIsShowing, setModal] = useState(false);

    const toggle = () => setModal(!modalIsShowing);

    useEffect(() => {
        props.requestBoard();
    }, []);

    const handleClick = (square: ChessBoardSquare) => {
        const UpdatedGameState: UpdateBoardModel = {
            board: {
                board: props.board,
                activeSquare: props.activeSquare,
                player1: props.player1,
                player2: props.player2,
                activePlayer: props.activePlayer,
                pawnToUpgrade: props.pawnToUpgrade,
                gameState: {state: null, player: null}
            },
            currentSquare: square
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


    return (
        <>
            <ChessPrompt
                board={props.board}
                activePlayer={props.activePlayer}
                activeSquare={props.activeSquare}
                player1={props.activePlayer}
                player2={props.player2}
                gameState={props.gameState}
                pawnToUpgrade={props.pawnToUpgrade}
                updateBoard={props.updateBoard}
                requestBoard={props.requestBoard}
            />
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
        </>
    )
}

export default connect(
    (state: ApplicationState) => state.board, // Selects which state properties are merged into the component's props
    BoardStore.actionCreators // Selects which action creators are merged into the component's props
)(ChessBoard as any);