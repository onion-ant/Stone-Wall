import React, { useEffect } from 'react';
import styles from './CarouselMenu.module.css';
import Carousel from './Carousel';

const CarouselMenu = () => {
  const [images, setImages] = React.useState([]);
  useEffect(() => {
    setImages([
      "../../Assets/SquareHboMax.svg",
      "../../Assets/SquarePrime.svg",
      "../../Assets/SquareNetflix.svg",
      "../../Assets/SquareAppleTV.svg",
    ]);
  }, []);

  return (
    <div className={styles.barra}>
      <section className={`${styles.mainMenu} container margin-top`}>
        <div className={`${styles.itemsCenter}`}>
          <h1 className={styles.texto}>
            DESCUBRA O <br />
            MELHOR PARA VOCÊ
          </h1>
          <div className={styles.carousel}>
            <Carousel direction={false} stop={false} images={images} />
            <Carousel direction={true} stop={false} images={images} />
          </div>
        </div>
      </section>
    </div>
  );
};

export default CarouselMenu;
