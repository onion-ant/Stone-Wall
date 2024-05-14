import React from 'react';
import styles from './ItemCatalogo.module.css';

const ItemCatalogo = ({ json }) => {
  return (
    <>
      {json
        ? json.map((item, index) => {
            console.log(item.item.posterPath);
            return (
              <img
                key={index}
                className={styles.itemCatalogo}
                src={item.item.posterPath}
                alt=""
                // onClick={}
              />
            );
          })
        : ''}
        {/* {isOpen &&} */}
    </>
  );
};

export default ItemCatalogo;
