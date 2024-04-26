'use client';
import { useEffect, useState } from 'react';
import styles from './ServicesCarousel.module.css';

const ServicesCarousel = () => {
  const [ids, setIds] = useState([0, 1, 2, 3]);
  const [ids2, setIds2] = useState([3, 2, 1, 0]);
  useEffect(() => {
    const interval = setInterval(() => {
      setIds((prevIds) => {
        return [prevIds[1], prevIds[2], prevIds[3], prevIds[0]];
      });
      setIds2((prevIds) => {
        return [prevIds[1], prevIds[2], prevIds[3], prevIds[0]];
      });
    }, 2000);
    return () => clearInterval(interval);
  }, []);

  return (
    <>
      <div className={styles.items}>
        <img src="../../Components/SquareHboMax.svg" alt="" id={`${ids[0]}L`} />
        <img src="../../Components/SquarePrime.svg" alt="" id={`${ids[1]}L`} />
        <img
          src="../../Components/SquareNetflix.svg"
          alt=""
          id={`${ids[2]}L`}
        />
        <img
          src="../../Components/SquareAppleTV.svg"
          alt=""
          id={`${ids[3]}L`}
        />
      </div>
      <div className={styles.items}>
        <img
          src="../../Components/SquareNetflix.svg"
          alt=""
          id={`${ids2[0]}R`}
        />
        <img src="../../Components/SquarePrime.svg" alt="" id={`${ids2[1]}R`} />
        <img
          src="../../Components/SquareAppleTV.svg"
          alt=""
          id={`${ids2[2]}R`}
        />
        <img
          src="../../Components/SquareHboMax.svg"
          alt=""
          id={`${ids2[3]}R`}
        />
      </div>
    </>
  );
};

export default ServicesCarousel;
