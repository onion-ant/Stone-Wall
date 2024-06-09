import { useState } from 'react';
import styles from './Analise.module.css';
import { Link } from 'react-router-dom';

const Analise = () => {
  const [filmes, setFilmes] = useState(true);
  const [jogos, setJogos] = useState(true);
  const [musicas, setMusicas] = useState(true);
  return (
    <div className={styles.background}>
      <div
        className={`${styles.options} ${styles.filmes} ${
          filmes ? '' : 'active'
        }`}
        onClick={() => {
          setFilmes(!filmes);
          setMusicas(true);
          setJogos(true);
        }}
      >
        FILMES/SÉRIES
        <div className={styles.innerOptions}>
          <Link to="/Analysis">Analisar Streaming</Link>
          <Link to="/">Comparar Streamings</Link>
          <Link to="/">Buscar Filme</Link>
        </div>
      </div>
      <div
        className={`${styles.options} ${styles.musicas} ${
          musicas ? '' : 'active'
        }`}
        onClick={() => {
          setFilmes(true);
          setMusicas(!musicas);
          setJogos(true);
        }}
      >
        MÚSICAS
        <div className={styles.innerOptions}>
          <Link to="/">Analisar Streaming</Link>
          <Link to="/">Comparar Streamings</Link>
        </div>
      </div>
      <div
        className={`${styles.options} ${styles.jogos} ${
          jogos ? '' : 'active'
        } ${musicas ? '' : 'getDown'}`}
        onClick={() => {
          setFilmes(true);
          setMusicas(true);
          setJogos(!jogos);
        }}
      >
        JOGOS
        <div className={styles.innerOptions}>
          <Link to="/">Analisar Streaming</Link>
          <Link to="/">Comparar Streamings</Link>
        </div>
      </div>
    </div>
  );
};

export default Analise;
