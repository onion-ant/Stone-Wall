import styles from './ModalCatalogo.module.css';
import useFetch from '../../useFetch';

const ModalCatalogo = ({ tmdbId }) => {
  const {
    data: json,
    loading,
    error,
  } = useFetch(`https://localhost:7282/Items/${tmdbId}?sizeParams=w780`);
  console.log(json);
  return (
    <div className={styles.modal}>
      {loading && <h1> loading</h1>}
      {!loading && (
        <>
          <img src={json.backdropPath} alt="" className={styles.imagemModal} />
          <div className={styles.modalTexts}>
            <h1 className={styles.filmName}>{json.title}</h1>
            <div className={styles.streamingItems}>
              {json.streamings.map((x, indexStreaming) => (
                <div key={indexStreaming} className={styles.streaming}>
                  <a href={x.link}>
                    <img
                      src={`../../Assets/${x.streamingId}Square.svg`}
                      alt=""
                    />
                  </a>
                  <p>{x.type.toUpperCase()}</p>
                </div>
              ))}
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default ModalCatalogo;
