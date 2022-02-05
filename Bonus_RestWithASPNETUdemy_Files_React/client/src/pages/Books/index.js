import React, { useState, useEffect } from 'react';
import './styles.css';
import logoImage from '../../assets/logo.svg';
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import { FiPower, FiEdit, FiTrash2 } from 'react-icons/fi';
import api from '../../services/api';

export default function Books() {
  const [books, setBooks] = useState([]);
  const [page, setPage] = useState(1);
  const userName = localStorage.getItem('userName');
  const accessToken = localStorage.getItem('accessToken');
  const navigate = useNavigate();

  const authorizationToken = {
    headers: {
      Authorization: `Bearer ${accessToken}`
    }
  }

  useEffect(() => {
    fetchMoreBooks();
  }, [accessToken]);

  async function fetchMoreBooks() {
    const response = await api.get(`api/Book/v1/asc/4/${page}`, authorizationToken);
    setBooks([...books, ...response.data.list]);
    setPage(page + 1);
  }

  async function logout() {
    try {
      await api.get(`api/auth/v1/revoke`, authorizationToken);
      localStorage.clear();
      navigate('/');
    } catch (err) {
      alert('Logout failed! Try again')
    }
  }

  async function editBook(id) {
    try {
      navigate(`books/new/${id}`);
    } catch {
      alert('Edit Book failed! Try again')
    }
  }

  async function deleteBook(id) {
    try {
      api.delete(`api/Book/v1/${id}`, authorizationToken);
      setBooks(books.filter(book => book.id !== id));
      alert('Delete book Success');
    } catch {
      alert('Delete failed! Try again')
    }
  }

  return (
    <div className="book-container">
      <header>
        <img src={logoImage} alt="Erudio" />
        <span>Welcome, <strong>{userName.toLowerCase()}</strong>!</span>
        <Link className="button" to="books/new/0">Add New Book</Link>
        <button type="button" onClick={logout}>
          <FiPower size={18} color="#251FC5" />
        </button>
      </header>
      <h1>Registered Books</h1>
      <ul>
        {books.map(book => (
          <li key={book.id}>
            <strong>Title:</strong>
            <p>{book.title}</p>
            <strong>Author:</strong>
            <p>{book.author}</p>
            <strong>Price:</strong>
            <p>{Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(book.price)}</p>
            <strong>Release Date:</strong>
            <p>{Intl.DateTimeFormat('pt-BR').format(new Date(book.launch_Date))}</p>

            <button type="button" onClick={() => editBook(book.id)}>
              <FiEdit size={20} color="#251FC5" />
            </button>
            <button onClick={() => deleteBook(book.id)} type="button">
              <FiTrash2 size={20} color="#251FC5" />
            </button>
          </li>
        ))}
      </ul>
      <button className='button' onClick={()=>fetchMoreBooks()} type="button">Load More</button>
    </div>
  );
}