import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import ChessBoard from "./components/ChessBoard/ChessBoard";

import './custom.css'


export default () => (
    <Layout>
        <Route exact path='/' component={ChessBoard} />
    </Layout>
);
