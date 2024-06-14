import React, { useState } from 'react';
import styles from './SelectStreamings.module.css';
import SideMenu from '../SideMenu/SideMenu';
import SingleStreaming from './SingleStreaming/SingleStreaming';

const SelectStreamings = () => {
  return (
    <>
      <SideMenu />
      <div
        className={styles.select}
        style={{
          display: 'flex',
        }}
      >
        <SingleStreaming />
        <p>COMPARAR CATÁLOGO</p>
        <SingleStreaming />
      </div>
    </>
  );
};

export default SelectStreamings;
