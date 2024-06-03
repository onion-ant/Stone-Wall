import styles from './ItemsCatalogo.module.css';
import PropTypes from 'prop-types';
import useFetch from '../../useFetch';
import { useState, useEffect } from 'react';
import SingleItemCatalogo from '../SingleItemCatalogo/SingleItemCatalogo';
import Loading from '../../Loading/Loading';
import Error from '../../Error/Error';

const ItemsCatalogo = ({ urlFetch }) => {
  const [data, setData] = useState('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [NextCursor, setNextCursor] = useState('');
  useFetch(urlFetch, setData, setLoading, setError, setNextCursor);
  function handleClick() {
    console.log(NextCursor);
    urlFetch = urlFetch + '&cursor=' + encodeURIComponent(NextCursor);
    setLoading(true);
    // debugger;
    fetch(urlFetch)
      .then((x) => x.json())
      .then((json) => {
        setData([...data, ...json.items]);
        setNextCursor(json.nextCursor);
      })
      .finally(setLoading(false));
  }
  const handleScroll = () => {
    // console.log('scrollHeight', document.documentElement.scrollHeight);
    // console.log('scrollTop', document.documentElement.scrollTop);
    // console.log('innerHeight', window.innerHeight);
    if (
      window.innerHeight + document.documentElement.scrollTop + 1 >=
      document.documentElement.scrollHeight
    ) {
      setTimeout(() => {
        console.log('aaaa');
      }, 1000);
    }
  };

  useEffect(() => {
    const intersectionObserver = new IntersectionObserver();
    intersectionObserver.observe(document.querySelector('#sentinela'));
    return () => intersectionObserver.disconnect();
  }, []);

  useEffect(() => {
    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  if (loading) return <Loading />;
  if (error) return <Error error={error} />;
  return (
    <>
      {error && <h1>{error.message}</h1>}
      <div className={styles.containerItemCat}>
        {data
          ? data.map((item, index) => {
              return (
                <SingleItemCatalogo
                  key={index}
                  posterPath={
                    item.item ? item.item.posterPath : item.posterPath
                  }
                  tmdbId={item.item ? item.item.tmdbId : item.tmdbId}
                />
              );
            })
          : ''}
        <button onClick={handleClick}>teste</button>
        <p id="sentinela">teste</p>
      </div>
      {loading && <Loading />}
    </>
  );
};

ItemsCatalogo.propTypes = {
  urlFetch: PropTypes.string,
};

export default ItemsCatalogo;
