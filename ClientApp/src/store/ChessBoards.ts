import {Action, Reducer} from 'redux';
import {AppThunkAction} from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export type ChessPiece = {
    color: string,
    name: string,
    value: number
}

export type ChessBoardSquare = {
    piece: ChessPiece,
    key: string,
    isActive: boolean,
    canMoveTo: boolean,
    underThreatFromWhite: ChessBoardSquare[] | null,
    underThreatFromBlack: ChessBoardSquare[] | null,
    enPassantPiece: ChessBoardSquare | null,
    rookToCastle: ChessBoardSquare | null
}

export type Player = {
    color: string,
    isComputer: boolean,
    isActive: boolean
}

export type GameState = {
    state: string | null,
    player: Player | null
}

export interface ChessBoardState {
    isLoading?: boolean;
    board: ChessBoardSquare[][];
    activeSquare: ChessBoardSquare | null;
    activePlayer: Player | null;
    player1: Player | null;
    player2: Player | null;
    pawnToUpgrade: ChessBoardSquare | null;
    gameState: GameState;
    previousMoves: {hash: string, value: number}
}

export interface UpdateBoardModel {
    board: ChessBoardState;
    currentSquare: ChessBoardSquare | null;
}


// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestBoardAction {
    type: 'REQUEST_BOARD';
}

interface ReceiveBoardAction {
    type: 'RECEIVE_BOARD';
    gameState: ChessBoardState;
}

interface UpdateBoardAction {
    type: 'UPDATE_BOARD';
    gameState: ChessBoardState;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestBoardAction | ReceiveBoardAction | UpdateBoardAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestBoard: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (true) {
            fetch(`chessboard`)
                .then(response => response.json() as Promise<ChessBoardState>)
                .then(data => {
                    dispatch({type: 'RECEIVE_BOARD', gameState: data});
                });

            dispatch({type: 'REQUEST_BOARD'});
        }
    },
    updateBoard: (body: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
        try {
            fetch(`chessboard`, {
                method: "POST",
                body: JSON.stringify(body),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json() as Promise<ChessBoardState>)
                .then(data => {
                    dispatch({type: 'UPDATE_BOARD', gameState: data});
                });

            dispatch({type: 'REQUEST_BOARD'});
        }
        catch (e)
        {
            console.log(e);
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: ChessBoardState =
    {
        board: [],
        activeSquare: null,
        activePlayer: null,
        player1: null,
        player2: null,
        isLoading: false,
        pawnToUpgrade: null,
        gameState: {player: null, state: null},
        previousMoves: {hash:"x", value:0}
    };

export const reducer: Reducer<ChessBoardState> = (state: ChessBoardState | undefined, incomingAction: Action): ChessBoardState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_BOARD':
            return {
                ...state
            };
        case 'RECEIVE_BOARD':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            return {
                ...action.gameState,
                isLoading: false
            };
        case 'UPDATE_BOARD':
            return {
                ...action.gameState,
                isLoading: false
            };
            break;
    }

    return state;
};