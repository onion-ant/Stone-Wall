import { useState } from 'react';
import styles from './StreamingComparison.module.css';
import SideMenu from '../SideMenu/SideMenu';
import Select from '../StreamingRepresentation/Select/Select';
import useFetch from '../useFetch';
import ItemsCatalogo from '../StreamingRepresentation/ItemsCatalogo/ItemsCatalogo';

const StreamingComparison = () => {
  const [data, setData] = useState('');
  useFetch('https://localhost:7282/Streamings', setData);

  return (
    <>
      <div className={styles.background}>
        <SideMenu />
        <div>
          <Select></Select>
          <ItemsCatalogo></ItemsCatalogo>
        </div>
      </div>
    </>
  );
};

export default StreamingComparison;
