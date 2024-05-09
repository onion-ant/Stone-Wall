import React, { useEffect } from 'react';
import styles from './CarouselMenu.module.css';
import Carousel from '../Carousel/Carousel';

const CarouselMenu = () => {
  const [images, setImages] = React.useState([]);
  useEffect(() => {
    setImages([
      '../../Assets/hboSquare.svg',
      '../../Assets/primeSquareWhite.svg',
      '../../Assets/netflixSquare.svg',
      '../../Assets/appleSquareWhite.svg',
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
            <Carousel direction={false} images={images} type={'Streaming'} />
            <Carousel direction={true} images={images} type={'Streaming'} />
          </div>
        </div>
      </section>
    </div>
  );
};

export default CarouselMenu;
