'use client';
import { useEffect, useState } from 'react';
import styles from './ServicesCarousel.module.css';

const ServicesCarousel = ({ direction }) => {
  const [idDirection,setidDirection] = useState('');
  const [ids, setIds] = useState([]);
  useEffect(() => {
    if (direction) {
      setidDirection('U')
      setIds([0, 1, 2, 3]);
    } else {
      setidDirection('D')
      setIds([3, 2, 1, 0]);
    }
    const interval = setInterval(() => {
      setIds((prevIds) => {
        return [prevIds[1], prevIds[2], prevIds[3], prevIds[0]];
      });
    }, 2000);
    return () => clearInterval(interval);
  }, [direction]);
  return (
    <>
      <div className={`${styles.items} ${idDirection}`}>
        <img
          src="../../Components/SquareHboMax.svg"
          alt=""
          id={`${ids[0]}${idDirection}`}
        />
        <img
          src="../../Components/SquarePrime.svg"
          alt=""
          id={`${ids[1]}${idDirection}`}
        />
        <img
          src="../../Components/SquareNetflix.svg"
          alt=""
          id={`${ids[2]}${idDirection}`}
        />
        <img
          src="../../Components/SquareAppleTV.svg"
          alt=""
          id={`${ids[3]}${idDirection}`}
        />
      </div>
    </>
  );
};

export default ServicesCarousel;
