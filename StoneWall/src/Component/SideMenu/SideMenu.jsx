import React from 'react';
import styles from './SideMenu.module.css';
import { Link } from 'react-router-dom';

const SideMenu = () => {
  const ref = React.useRef(false);
  const refButton = React.useRef(false);
  React.useEffect(() => {
    const handleClickOutside = (event) => {
      if (ref.current != null && refButton.current != null) {
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
      }
      return () => {
        document.removeEventListener('mousedown', handleClickOutside);
      };
    };
    document.addEventListener('mousedown', handleClickOutside);
  }, []);
  return (
    <div className={styles.menu}>
      <p className={styles.logo}>
        <a href="/">STONEWALL</a>
      </p>
      <button className={styles.buttonMenu} ref={refButton}></button>
      <div className={styles.itemsMenu} ref={ref}>
        <Link to="/Analysis">ANÁLISE</Link>
        <Link to="/Comparision">COMPARAÇÃO</Link>
        <a href="">ALEATÓRIO</a>
      </div>
    </div>
  );
};

export default SideMenu;
