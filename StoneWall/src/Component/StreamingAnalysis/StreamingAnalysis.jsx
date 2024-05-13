import React from 'react';
import styles from './StreamingAnalysis.module.css';
import SideMenu from '../SideMenu/SideMenu';

const StreamingAnalysis = () => {
  return (
    <div className={styles.background}>
      <SideMenu />
      <div className={`${styles.streaming} ${styles.header}`}></div>
    </div>
  );
};

export default StreamingAnalysis;
