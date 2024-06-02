import styles from './ItemsCatalogo.module.css';
import PropTypes from 'prop-types';
import useFetch from '../../useFetch';
import { useState } from 'react';
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
    urlFetch = urlFetch + '&cursor=' + encodeURIComponent(NextCursor);
    fetch(urlFetch)
      .then((x) => x.json())
      .then((json) => {
        setData([...data, ...json.items]);
        setNextCursor(json.nextCursor);
      });
  }
  console.log(data);
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
      </div>
    </>
  );
};

ItemsCatalogo.propTypes = {
  urlFetch: PropTypes.string,
};

export default ItemsCatalogo;
