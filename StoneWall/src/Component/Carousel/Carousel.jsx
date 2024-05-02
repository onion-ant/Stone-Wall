'use client';
import { useEffect, useState } from 'react';
import styles from './Carousel.module.css';
import PropTypes from 'prop-types';

const Carousel = ({ direction, stop, images, type }) => {
  const [idDirection, setidDirection] = useState('');
  const [ids, setIds] = useState([]);
  useEffect(() => {
    let val = [];
    for (let i = 0; i < images.length; i++) {
      val[i] = i;
    }
    if (direction) {
      setidDirection('U');
      setIds(val);
    } else {
      setidDirection('D');
      setIds(val.reverse());
    }
    const interval = setInterval(() => {
      if (stop == false) {
        setIds((prevIds) => {
          if(images.length === 3){
            return [prevIds[1], prevIds[2], prevIds[0]];
          } else if(images.length === 4){
            return [prevIds[1], prevIds[2], prevIds[3], prevIds[0]];
          }
        });
      }
    }, 2000);
    return () => clearInterval(interval);
  }, [direction, stop, images, type]);
  return (
    <>
      <div className={`${styles.items} ${styles[type]} ${idDirection}`}>
        {images.map((image, index) => (
          <img
            src={image}
            key={index}
            alt=""
            id={`${ids[index]}${idDirection}`}
          />
        ))}
      </div>
    </>
  );
};

Carousel.propTypes = {
  direction: PropTypes.bool,
  stop: PropTypes.bool,
  images: PropTypes.array,
  type: PropTypes.string,
};

export default Carousel;
