import styles from './ModalCatalogo.module.css';
import useFetch from '../../useFetch';
import PropTypes from 'prop-types';
import { useState } from 'react';
import Loading from '../../Loading/Loading';

const ModalCatalogo = ({ tmdbId, onClose }) => {
  const [data, setData] = useState('');
  const [error, setError] = useState('');
  const [imgLoaded, setImgLoaded] = useState(false);
  tmdbId = encodeURIComponent(tmdbId);
  useFetch(
    `https://localhost:7282/Items/${tmdbId}?sizeParams=w780`,
    setData,
    setError,
  );
  return (
    <>
      {!imgLoaded && <Loading />}
      <div
        className={styles.modal}
        style={{
          display: imgLoaded ? 'block' : 'none',
          opacity: imgLoaded ? 1 : 0,
          width: '100%',
        }}
      >
        {error && <h1>{error.message}</h1>}
        {data && (
          <div
            onClick={() => {
              onClose();
            }}
          >
            {
              <img
                src={data.backdropPath}
                alt=""
                className={styles.imagemModal}
                onLoad={() => {
                  setImgLoaded(true);
                }}
              />
            }
            <div className={styles.modalTexts}>
              <h1 className={styles.filmName}>{data.title}</h1>
              <button className={styles.buttonClose}>
                <img src="../../Assets/close.svg" alt="" />
              </button>
              <p>{data.overview}</p>
              <div className={styles.streamingItems}>
                {data.streamings.map((x, indexStreaming) => {
                  return (
                    <div
                      key={indexStreaming}
                      className={styles.singleStreaming}
                    >
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
          </div>
        )}
      </div>
    </>
  );
};

ModalCatalogo.propTypes = {
  tmdbId: PropTypes.string,
  onClose: PropTypes.func,
};

export default ModalCatalogo;
