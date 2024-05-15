import { useEffect, useState, useRef } from 'react';
import styles from './StreamingAnalysis.module.css';
import SideMenu from './SideMenu/SideMenu';
import ItemCatalogo from './ItemCatalogo/ItemCatalogo';
import ModalCatalogo from './ModalCatalogo/ModalCatalogo';

const StreamingAnalysis = () => {

  const [tmdbId, setTmdbId] = useState('');
  const [isOpen, setIsOpen] = useState(false);
  const modalRef = useRef();

  function handleDataFromChild(tmdbId, isOpen) {
    setTmdbId(tmdbId);
    setIsOpen(isOpen);
  }
  function handleOutsideModalClick(event) {
    event.stopPropagation();
    if (modalRef.current) {
      if (
        !modalRef.current.firstChild.contains(event.target) &&
        isOpen == true
      ) {
        setIsOpen(false);
      }
    }
  }
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
            sendData={handleDataFromChild}
            open={isOpen}
          />
        </div>
    </div>
  );
};

export default StreamingAnalysis;
