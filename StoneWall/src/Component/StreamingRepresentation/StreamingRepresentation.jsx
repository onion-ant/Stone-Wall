import { useEffect, useState, useRef } from 'react';
import styles from './StreamingAnalysis.module.css';
import SideMenu from './SideMenu/SideMenu';
import ItemCatalogo from './ItemCatalogo/ItemCatalogo';
import ModalCatalogo from './ModalCatalogo/ModalCatalogo';
import useFetch from '../useFetch';
import SearchAnalysis from './AnalysisForm/SearchAnalysis';

const StreamingRepresentation = () => {
  const [tmdbId, setTmdbId] = useState('');
  const [isOpen, setIsOpen] = useState(false);
  const [selectedStreaming, setSelectedStreaming] = useState('apple');
  const [streamingType, setStreamingType] = useState('subscription');
  const [data, setData] = useState('');
  const [loading, setLoading] = useState('');
  const [error, setError] = useState('');
  const [search, setSearch] = useState('');

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

  const [pages, setPages] = useState([1]);
  const [infinity, setInfinity] = useState(true);
  useEffect(() => {
    let wait = false;
    function infiniteScroll() {
      if (infinity) {
        const scroll = window.scrollY;
        const heigth = document.body.offsetHeight - window.innerHeight;
        if (scroll > heigth * 0.9 && !wait) {
          setPages((pages) => [...pages, pages.length + 1]);
          wait = true;
          setTimeout(() => {
            wait = false;
          }, 500);
        }
      }
    }
    window.addEventListener('wheel', infiniteScroll);
    window.addEventListener('scroll', infiniteScroll);
    return () => {
      window.removeEventListener('wheel', infiniteScroll);
      window.removeEventListener('scroll', infiniteScroll);
    };
  }, [infinity]);
  useFetch('https://localhost:7282/Streamings', setData, setLoading, setError);
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
        {data && (
          <div className={styles.selects}>
            <select
              name=""
              id=""
              value={selectedStreaming}
              onChange={(e) => setSelectedStreaming(e.target.value)}
            >
              {data.map((option, index) => (
                <option key={index} value={option.id}>
                  {option.name}
                </option>
              ))}
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
            </select>
            <SearchAnalysis setSearch={setSearch} />
          </div>
        )}
        {error && <h1>{error.message}</h1>}
        {/* {loading && <h1>{loading}</h1>} */}
        <ItemCatalogo
          sendData={handleDataFromChild}
          urlFetch={`https://localhost:7282/Streamings/${selectedStreaming}${
            streamingType ? '?streamingType=' + streamingType + '&' : '?'
          }sizeParams=w300_and_h450_bestv2&language=pt-BR&pageNumber=1&offset=30&${
            search ? 'name=' + search : ''
          }`}
          setInfinity={setInfinity}
        />
      </div>
    </div>
  );
};

export default StreamingRepresentation;
