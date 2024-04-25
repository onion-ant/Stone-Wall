'use client';
import React from 'react';
import styles from './ServicesCarousel.module.css';

const ServicesCarousel = () => {
  setInterval(changeClass, 2000);
  function changeClass() {
    let items = document.getElementById('items');
    if (items.children[0].id == 2) {
      items.children[0].id = 0;
    } else {
      items.children[0].id++;
    }
    if (items.children[1].id == 2) {
      items.children[1].id = 0;
    } else {
      items.children[1].id++;
    }
    if (items.children[2].id == 2) {
      items.children[2].id = 0;
    } else {
      items.children[2].id++;
    }
    // for (let j = 0; j < 3; j++) {
    //   let item = items.children[j];
    //   let val = +item.id;
    //   if (val == 2) {
    //     item.id = 0;
    //   } else {
    //     item.id = val + 1;
    //   }
    // }
  }
  return (
    <>
      <div className={styles.items} id="items">
        <img src="../../Components/SquarePrime.svg" alt="" id="0" />
        <img src="../../Components/SquareNetflix.svg" alt="" id="1" />
        <img src="../../Components/SquareAppleTV.svg" alt="" id="2" />
      </div>
    </>
  );
};

export default ServicesCarousel;
