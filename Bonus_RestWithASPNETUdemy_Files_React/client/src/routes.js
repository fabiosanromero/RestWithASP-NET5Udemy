import React from 'react';
import { Routes as Routing, Route } from 'react-router-dom';
import Login from './pages/Login';
import Books from './pages/Books';
import NewBook from './pages/NewBook';

export default function Routes() {
  return (
    <Routing>
      <Route path="/" exact element={<Login />} />
      <Route path="/books" element={<Books />} />
      <Route path="/books/books/new/:bookId" element={<NewBook />} />
    </Routing>
  );
}