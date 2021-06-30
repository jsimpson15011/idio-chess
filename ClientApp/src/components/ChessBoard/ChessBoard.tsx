import * as React from 'react';
import {connect} from 'react-redux';
import {RouteComponentProps} from 'react-router';
import {Link} from 'react-router-dom';
import {ApplicationState} from '../../store';
import * as BoardStore from '../../store/ChessBoards';
import {useEffect} from "react";
import './ChessBoard.css';

// At runtime, Redux will merge together...
type ChessBoardProps =
    BoardStore.ChessBoardState // ... state we've requested from the Redux store
    & typeof BoardStore.actionCreators // ... plus action creators we've requested


const ChessBoard = (props: ChessBoardProps) => {

    useEffect(() => {
        props.requestBoard();
    }, []);


    return (
        <div>
            <h1>Future Chess Board</h1>
            {props.board.map(row => {
                return (
                    <div className="board__row" key={row[0].key[0]}>
                        {row.map(square => {
                            return (
                                <div className="board__square" key={square.key}>
                                    {square.piece ?
                                        <img src={"/img/" + square.piece.name + "-" + square.piece.color + ".png"}
                                             alt=""/> : ""}
                                </div>
                            )
                        })}
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