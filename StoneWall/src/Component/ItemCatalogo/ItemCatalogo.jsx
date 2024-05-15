import { useState } from 'react';
import styles from './ItemCatalogo.module.css';
import PropTypes from 'prop-types';

const ItemCatalogo = ({ json, sendData, open }) => {
  function handleClick(tmdbId) {
    sendData(tmdbId, !open);
  }
  return (
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
  );
};

// ItemCatalogo.propTypes = {
//   json: PropTypes.string,
// };

export default ItemCatalogo;
