import styles from './Carousel.module.css';
import ServicesCarousel from './ServicesCarousel';

const Carousel = () => {
  return (
    <div className={styles.barra}>
      <section className={`${styles.carousel} container `}>
        <div className={`${styles.itemsCenter}`}>
          <h1 className={styles.texto}>
            DESCUBRA O <br />
            MELHOR PARA VOCÊ
          </h1>
          <div className={styles.bosta}>
            <ServicesCarousel />
          </div>
        </div>
      </section>
    </div>
  );
};

export default Carousel;