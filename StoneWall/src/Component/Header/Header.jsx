import React from 'react';
import Button from '../Button/Button';
import { Link } from 'react-router-dom';
import styles from './Header.module.css';


const Header = () => {
  const ref = React.useRef(null);
  const refButton = React.useRef(null);
  React.useEffect(() => {
    const handleClickOutside = (event) => {
      if (
        refButton.current.contains(event.target) &&
        !ref.current.classList.contains(styles.active)
      ) {
        ref.current.classList.add(styles.active);
        refButton.current.classList.add(styles.active);
      } else if (
        (!ref.current.contains(event.target) &&
          !refButton.current.contains(event.target)) ||
        (refButton.current.contains(event.target) &&
          ref.current.classList.contains(styles.active))
      ) {
        ref.current.classList.remove(styles.active);
        refButton.current.classList.remove(styles.active);
      }
      return () => {
        document.removeEventListener('mousedown', handleClickOutside);
      };
    };
    document.addEventListener('mousedown', handleClickOutside);
  }, []);
  return (
    <div className={`${styles.header} container`}>
      <Link to={'/'} className={styles.titulo}>
        STONEWALL
      </Link>
      <div className={`${styles.rightSide}`} ref={ref}>
        <Link to={'/'} className={styles.link}>
          Analise
        </Link>
        <Link to={'/'} className={styles.link}>
          Criar conta
        </Link>
        <Button>ENTRE</Button>
      </div>
      <button className={styles.button} ref={refButton}>
        Menu
      </button>
    </div>
  );
};

export default Header;
