import Button from '../Button/Button';
import { Link } from 'react-router-dom';
import styles from './Header.module.css';

const Header = () => {
  return (
    <div className={`${styles.header} container`}>
      <Link to={'/'} className={styles.titulo}>
        STONEWALL
      </Link>
      <div className={styles.rightSide}>
        <Link to={'/'} className={styles.link}>
          Analise
        </Link>
        <Link to={'/'} className={styles.link}>
          Criar conta
        </Link>
        <Button>ENTRE</Button>
      </div>
    </div>
  );
};

export default Header;
