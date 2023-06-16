import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import ChessBoard from "./components/ChessBoard/ChessBoard";

import './custom.css'


export default () => (
    <Layout>
        <Route exact path='/' component={ChessBoard} />
    </Layout>
);
