import styles from './ItemCatalogo.module.css';
import PropTypes from 'prop-types';
import useFetch from '../../useFetch';

const ItemCatalogo = ({ sendData, open }) => {
  function handleClick(tmdbId) {
    sendData(tmdbId, !open);
  }
  const {
    data: json,
    loading,
    error,
  } = useFetch(
    'https://localhost:7282/Streamings/apple?sizeParams=w300_and_h450_bestv2&language=pt-BR&pageNumber=1&offset=50',
  );
  return (
    <>
      {!error && (
        <div className={styles.containerItemCat}>
          {json
            ? json.map((item, index) => {
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
};

export default ItemCatalogo;
