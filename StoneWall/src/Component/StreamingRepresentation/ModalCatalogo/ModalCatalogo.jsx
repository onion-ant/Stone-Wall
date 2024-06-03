import styles from './ModalCatalogo.module.css';
import useFetch from '../../useFetch';
import PropTypes from 'prop-types';
import { useState } from 'react';
import Loading from '../../Loading/Loading';

const ModalCatalogo = ({ tmdbId, onClose }) => {
  const [data, setData] = useState('');
  const [loading, setLoading] = useState('');
  const [error, setError] = useState('');
  tmdbId = encodeURIComponent(tmdbId);
  useFetch(
    `https://localhost:7282/Items/${tmdbId}?sizeParams=w780`,
    setData,
    setLoading,
    setError,
  );
  if (loading) return <Loading />;
  return (
    <div className={styles.modal}>
      {error && <h1>{error.message}</h1>}
      {!loading && data && (
        <>
          <img src={data.backdropPath} alt="" className={styles.imagemModal} />
          <div className={styles.modalTexts}>
            <h1 className={styles.filmName}>{data.title}</h1>
            <button
              className={styles.buttonClose}
              onClick={() => {
                onClose();
              }}
            >
              X
            </button>
            <p>{data.overview}</p>
            <div className={styles.streamingItems}>
              {data.streamings.map((x, indexStreaming) => {
                return (
                  <div key={indexStreaming} className={styles.singleStreaming}>
                    <a href={x.link}>
                      <img
                        src={`../../Assets/${x.streamingId}Square.svg`}
                        alt=""
                      />
                      <p>{x.type.toUpperCase()}</p>
                    </a>
                  </div>
                );
              })}
            </div>
          </div>
        </>
      )}
    </div>
  );
};

ModalCatalogo.propTypes = {
  tmdbId: PropTypes.string,
  onClose: PropTypes.func,
};

export default ModalCatalogo;
