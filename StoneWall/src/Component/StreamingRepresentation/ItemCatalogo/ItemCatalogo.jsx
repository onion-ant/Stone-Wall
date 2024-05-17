import styles from './ItemCatalogo.module.css';
import PropTypes from 'prop-types';
import useFetch from '../../useFetch';
import { useEffect, useState } from 'react';

const ItemCatalogo = ({ sendData, open, urlFetch, setInfinity }) => {
  const [data, setData] = useState('');
  const [loading, setLoading] = useState('');
  const [error, setError] = useState('');
  function handleClick(tmdbId) {
    sendData(tmdbId, !open);
  }
  useFetch(urlFetch, setData, setLoading, setError);
  useEffect(() => {
    if (data != null) {
      if (error || data.length < 30) {
        setInfinity(false);
      }
    }
  }, [error, data, setInfinity]);
  console.log(data);
  return (
    <>
      {!error && (
        <div className={styles.containerItemCat}>
          {data
            ? data.map((item, index) => {
                return (
                  <img
                    key={index}
                    className={styles.itemCatalogo}
                    src={item.item.posterPath}
                    alt=""
                    onClick={() => handleClick(item.item.tmdbId)}
                  />
                );
              })
            : ''}
        </div>
      )}
      {loading && <h1>{loading}</h1>}
      {error && <h1>{error.message}</h1>}
    </>
  );
};

ItemCatalogo.propTypes = {
  sendData: PropTypes.func,
  open: PropTypes.bool,
  urlFetch: PropTypes.string,
  setInfinity: PropTypes.func,
};

export default ItemCatalogo;
