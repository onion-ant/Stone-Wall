import styles from './Carousel.module.css';

const Carousel = () => {
  return (
    <section className={styles.carousel}>
      <div >
        <div className={`container ${styles.barra}`}>
          <h1 className={styles.texto}>
            DESCUBRA O <br />
            MELHOR PARA VOCÊ
          </h1>
        </div>

        <div></div>
      </div>
    </section>
  );
};

export default Carousel;
