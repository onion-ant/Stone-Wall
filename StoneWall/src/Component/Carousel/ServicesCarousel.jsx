'use client';
import React from 'react';
import styles from './ServicesCarousel.module.css';

const ServicesCarousel = () => {
  const items = React.useRef('');
  setInterval(changeClass, 2000);
  function changeClass() {
    let i = document.getElementById('i');
    console.log(i.children);
    for (let j = 0; j < 3; j++) {
      let item = i.children[j];
      let val = +item.id;
      if (val == 2) {
        item.id = 0;
      } else {
        item.id = val + 1;
      }
    }
  }
  return (
    <>
      <div className={styles.items} id="i" ref={items}>
        <img src="../../Components/SquarePrime.svg" alt="" id="0" />
        <img src="../../Components/SquareNetflix.svg" alt="" id="1" />
        <img src="../../Components/SquareAppleTV.svg" alt="" id="2" />
      </div>
    </>
  );
};

export default ServicesCarousel;
