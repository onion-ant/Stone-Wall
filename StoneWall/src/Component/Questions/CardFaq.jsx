import React from 'react';
import styles from './CardFaq.module.css'


const CardFaq = ({ type, tittle, text }) => {
  return (
    <div>
      <h1 className={`${styles.titulo} ${styles[type]}`}>{tittle}</h1>
      <p className={`${styles.texto}`}>{text}</p>
    </div>
  );
};

export default CardFaq;
