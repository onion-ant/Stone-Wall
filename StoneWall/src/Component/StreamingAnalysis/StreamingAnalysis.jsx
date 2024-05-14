import { useEffect, useState } from 'react';
import styles from './StreamingAnalysis.module.css';
import SideMenu from '../SideMenu/SideMenu';
import ItemCatalogo from '../ItemCatalogo/ItemCatalogo';

const StreamingAnalysis = () => {
  const [json, setJson] = useState('');
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    try {
      fetch(
        'https://localhost:7282/Streamings/apple?streamingType=subscription&sizeParams=w300_and_h450_bestv2&language=pt-BR&pageNumber=1&offset=10',
      )
        .then((response) => response.json())
        .then((json) => setJson(json));
      setLoading(false);
    } catch (er) {
      setError(true);
      setJson(er);
      throw new Error(er.message);
    }
  }, []);
  console.log(json);
  return (
    <div className={styles.background}>
      <SideMenu />
      <div className={`${styles.catalogo}`}>
        <ItemCatalogo json={json} />
      </div>
    </div>
  );
};

export default StreamingAnalysis;
