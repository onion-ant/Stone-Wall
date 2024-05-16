import { useEffect, useState, useRef } from 'react';
import styles from './StreamingAnalysis.module.css';
import SideMenu from './SideMenu/SideMenu';
import ItemCatalogo from './ItemCatalogo/ItemCatalogo';
import ModalCatalogo from './ModalCatalogo/ModalCatalogo';

const StreamingRepresentation = () => {
  const [tmdbId, setTmdbId] = useState('');
  const [isOpen, setIsOpen] = useState(false);
  const [selectedStreaming, setSelectedStreaming] = useState('apple');
  const [streamingType, setStreamingType] = useState('');

  const modalRef = useRef();

  function handleDataFromChild(tmdbId, isOpen) {
    setTmdbId(tmdbId);
    setIsOpen(isOpen);
  }
  function closeModal() {
    setIsOpen(false);
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
  console.log(streamingType);
  return (
    <div className={styles.background} onClick={handleOutsideModalClick}>
      <SideMenu />
      {isOpen && (
        <div ref={modalRef} className={styles.modal}>
          <ModalCatalogo
            tmdbId={tmdbId}
            sendData={handleDataFromChild}
            onClose={closeModal}
          />
        </div>
      )}

      <div className={`${styles.catalogo}`}>
        <select
          name=""
          id=""
          value={selectedStreaming}
          onChange={(e) => setSelectedStreaming(e.target.value)}
        >
          <option value="apple">AppleTV</option>
          <option value="prime">PrimeVideo</option>
          <option value="hbo">HBOMAX</option>
          <option value="disney">Disney+</option>
        </select>
        <select
          name=""
          id=""
          value={streamingType}
          onChange={(e) => setStreamingType(e.target.value)}
        >
          <option value="subscription">Assinatura</option>
          <option value="addon">AddOn</option>
          <option value="buy">Comprar</option>
          <option value="rent">Alugar</option>
          <option value="free">Grátis</option>
        </select>
        <ItemCatalogo
          sendData={handleDataFromChild}
          urlFetch={`https://localhost:7282/Streamings/${selectedStreaming}${
            streamingType ? '?streamingType=' + streamingType + '&' : '?'
          }sizeParams=w300_and_h450_bestv2&language=pt-BR&pageNumber=1&offset=50`}
        />
      </div>
    </div>
  );
};

export default StreamingRepresentation;
