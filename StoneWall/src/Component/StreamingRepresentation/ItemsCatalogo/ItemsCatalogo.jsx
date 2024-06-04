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
  const [nextCursor, setNextCursor] = useState('');
  const [infinity, setInfinity] = useState(true);
  useFetch(urlFetch, setData, setLoading, setError, setNextCursor);
  // eslint-disable-next-line react-hooks/exhaustive-deps
  function fetchMoreItems() {
    console.log(nextCursor);
    urlFetch = urlFetch + '&cursor=' + encodeURIComponent(nextCursor);
    setLoading(true);
    fetch(urlFetch)
      .then((x) => x.json())
      .then((json) => {
        // debugger;
        setData([...data, ...json.items]);
        setNextCursor(json.nextCursor);
      })
      .finally(setLoading(false));
  }

  useEffect(() => {
    let wait = false;
    function infiniteScroll() {
      if (infinity) {
        const scroll = window.scrollY;
        const heigth = document.body.offsetHeight - window.innerHeight;
        if (scroll > heigth * 0.9 && !wait) {
          fetchMoreItems();
          wait = true;
          setTimeout(() => {
            wait = false;
          }, 5000);
        }
      }
    }
    window.addEventListener('wheel', infiniteScroll);
    window.addEventListener('scroll', infiniteScroll);
    return () => {
      window.removeEventListener('wheel', infiniteScroll);
      window.removeEventListener('scroll', infiniteScroll);
    };
  }, [infinity, fetchMoreItems]);

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
      </div>
      {loading && <Loading />}
    </>
  );
};

ItemsCatalogo.propTypes = {
  urlFetch: PropTypes.string,
};

export default ItemsCatalogo;
