import styles from './ModalCatalogo.module.css';
import useFetch from '../../useFetch';
import PropTypes from 'prop-types';

const ModalCatalogo = ({ tmdbId, onClose }) => {
  const {
    data: json,
    loading,
    error,
  } = useFetch(`https://localhost:7282/Items/${tmdbId}?sizeParams=w780`);
  return (
    <div className={styles.modal}>
      {loading && <h1> loading</h1>}
      {!loading && (
        <>
          <img src={json.backdropPath} alt="" className={styles.imagemModal} />
          <div className={styles.modalTexts}>
            <h1 className={styles.filmName}>{json.title}</h1>
            <button
              className={styles.buttonClose}
              onClick={() => {
                onClose();
              }}
            >
              X
            </button>
            <p>{json.overview}</p>
            <div className={styles.streamingItems}>
              {json.streamings.map((x, indexStreaming) => (
                <div key={indexStreaming} className={styles.singleStreaming}>
                  <a href={x.link}>
                    <img
                      src={`../../Assets/${x.streamingId}Square.svg`}
                      alt=""
                    />
                    <p>{x.type.toUpperCase()}</p>
                  </a>
                </div>
              ))}
            </div>
          </div>
        </>
      )}
    </div>
  );
};

ModalCatalogo.propTypes = {
  tmdbId: PropTypes.number,
  onClose: PropTypes.func,
};

export default ModalCatalogo;
