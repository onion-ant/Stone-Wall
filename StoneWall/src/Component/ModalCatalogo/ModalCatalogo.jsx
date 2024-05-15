import { useEffect } from 'react';
import styles from './ModalCatalogo.module.css';

const ModalCatalogo = ({ tmdbId }) => {
  // useEffect(()=>{
  //   fetch('')
  // },[])
  return <div className={styles.modal}>{tmdbId}</div>;
};

export default ModalCatalogo;
