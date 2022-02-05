import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './styles.css';
import logoImage from '../../assets/logo.svg';
import { Link, useParams } from 'react-router-dom';
import { FiArrowLeft } from 'react-icons/fi';
import api from '../../services/api';

export default function NewBook() {
  const [id, setId] = useState(null);
  const [author, setAuthor] = useState('');
  const [title, setTitle] = useState('');
  const [launch_Date, setlaunch_Date] = useState('');
  const [price, setPrice] = useState('');
  const navigate = useNavigate();
  const { bookId } = useParams();

  const accessToken = localStorage.getItem('accessToken');

  const authorizationToken = {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    }
  }

  useEffect(() => {
    if (bookId === '0') return;
    else loadBook();
  }, bookId);

  async function loadBook(e) {
    try {
      const response = await api.get(`api/book/v1/${bookId}`, authorizationToken);
      let adjustedDate = response.data.launch_Date.split("T", 10)[0];
      setId(response.data.id);
      setTitle(response.data.title);
      setAuthor(response.data.author);
      setPrice(response.data.price);
      setlaunch_Date(adjustedDate);

    } catch (error) {
      alert('Error recovering')
    }
  }


  async function saveOrUpdate(e) {
    e.preventDefault();
    const data = {
      title,
      author,
      launch_Date,
      price,
    }


    try {
      if (bookId === '0') {
        await api.post('api/Book/v1', data, authorizationToken);
        alert('Book Included with Success');
      }
      else {
        data.id = id;
        await api.put('api/Book/v1', data, authorizationToken);
        alert('Book updated with Success');
      }
    } catch (error) {
      alert('Error while recording Book! Try again!');
    }
    navigate('/Books')
  }

  return (
    <div className='new-book-container'>
      <div className='content'>
        <section className='form'>
          <img src={logoImage} alt="Erudio" />
          <h1>{bookId === '0' ? 'Add New Book' : 'Update Book'}</h1>
          <p>Enter the book information and click on {bookId === '0' ? `'Add'` : `'Update'`}! #### ${bookId}</p>
          <Link className='back-link' to="/books">
            <FiArrowLeft size={16} color='#251FC5' />
            Back to Books
          </Link>
        </section>
        <form onSubmit={saveOrUpdate}>
          <input
            placeholder='Title'
            value={title}
            onChange={e => setTitle(e.target.value)}
          />
          <input
            placeholder='Author'
            value={author}
            onChange={e => setAuthor(e.target.value)}
          />
          <input
            type='date'
            value={launch_Date}
            onChange={e => setlaunch_Date(e.target.value)}
          />
          <input
            placeholder='Price'
            value={price}
            onChange={e => setPrice(e.target.value)}
          />
          <button className='button' type='submit'> {bookId === '0' ? 'Add' : 'Update'}</button>
        </form>
      </div>
    </div>

  );
}