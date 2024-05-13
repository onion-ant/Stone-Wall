import React from 'react';
import styles from './SideMenu.module.css';

const SideMenu = () => {
  return (
    <div className={styles.menu}>
      <p className={styles.logo}>STONEWALL</p>
      <div className={styles.itemsMenu}>
        <a href="">FILMES/SÉRIES</a>
        <a href="">AUDIO</a>
        <a href="">JOGO</a>
      </div>
    </div>
  );
};

export default SideMenu;
