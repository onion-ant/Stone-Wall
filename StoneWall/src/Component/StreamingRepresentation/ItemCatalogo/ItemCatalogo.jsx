import styles from './ItemCatalogo.module.css';
import PropTypes from 'prop-types';
import useFetch from '../../useFetch';

const ItemCatalogo = ({ sendData, open, urlFetch }) => {
  function handleClick(tmdbId) {
    sendData(tmdbId, !open);
  }
  const { data, loading, error } = useFetch(urlFetch);
  console.log(loading);
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
};

export default ItemCatalogo;
