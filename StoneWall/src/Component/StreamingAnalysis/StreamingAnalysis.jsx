import { useEffect, useState, useContext, useRef } from 'react';
import styles from './StreamingAnalysis.module.css';
import SideMenu from '../SideMenu/SideMenu';
import ItemCatalogo from '../ItemCatalogo/ItemCatalogo';
import ModalCatalogo from '../ModalCatalogo/ModalCatalogo';
import UserContext from '../../UserContext';

const StreamingAnalysis = () => {
  const [json, setJson] = useState('');
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(true);
  const [tmdbId, setTmdbId] = useState('');
  const [isOpen, setIsOpen] = useState(false);
  const modalRef = useRef();

  function handleDataFromChild(tmdbId, isOpen) {
    setTmdbId(tmdbId);
    setIsOpen(isOpen);
  }
  function handleOutsideModalClick(event) {
    if (modalRef.current) {
      if (event.target != modalRef.current.firstChild && isOpen == true) {
        setIsOpen(false);
      }
    }
  }
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
  return (
    <div className={styles.background} onClick={handleOutsideModalClick}>
      <SideMenu />
      {isOpen && (
        <div ref={modalRef} className={styles.modal}>
          <ModalCatalogo tmdbId={tmdbId} sendData={handleDataFromChild} />
        </div>
      )}
      <div className={`${styles.catalogo}`}>
        <ItemCatalogo
          json={json}
          sendData={handleDataFromChild}
          open={isOpen}
        />
      </div>
    </div>
  );
};

export default StreamingAnalysis;
