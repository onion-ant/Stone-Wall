'use client';
import { useEffect, useState } from 'react';
import styles from './Carousel.module.css';
import PropTypes from 'prop-types';

const Carousel = ({ direction, stop, images }) => {
  const [idDirection, setidDirection] = useState('');
  const [ids, setIds] = useState([]);
  useEffect(() => {
    if (direction) {
      setidDirection('U');
      setIds([0, 1, 2, 3]);
    } else {
      setidDirection('D');
      setIds([3, 2, 1, 0]);
    }
    const interval = setInterval(() => {
      if (stop == false) {
        setIds((prevIds) => {
          return [prevIds[1], prevIds[2], prevIds[3], prevIds[0]];
        });
      }
    }, 2000);
    return () => clearInterval(interval);
  }, [direction, stop, images]);
  return (
    <>
      <div className={`${styles.items} ${idDirection}`}>
        {images.map((image, index) => (
          <img src={image} key={index} alt="" id={`${ids[index]}${idDirection}`} />
        ))}
      </div>
    </>
  );
};

Carousel.propTypes = {
  direction: PropTypes.bool,
  stop: PropTypes.bool,
  images: PropTypes.array,
};

export default Carousel;