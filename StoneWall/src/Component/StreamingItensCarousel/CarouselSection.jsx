import React from 'react';
import Carousel from '../Carousel/Carousel';
import styles from './CarouselSection.module.css';

const Movies = () => {
  const [images, setImages] = React.useState([]);
  React.useEffect(() => {
    setImages([
      '../../Assets/TestAssets/2uY8aYmc86UL4N86D2spkWzYKOd.jpg',
      '../../Assets/TestAssets/e8pI4XkYgUMuSJ8cEFbJE18wc4e.jpg',
      '../../Assets/TestAssets/fZJSBHJKNR7Xz3CiEsAawd7bbDh.jpg',
    ]);
  }, []);
  return (
    <div className={styles.background}>
      <div className={`${styles.movies} container'`}>
        <Carousel
          direction={true}
          stop={false}
          images={images}
          type={'Cartaz'}
        />
      </div>
    </div>
  );
};

export default Movies;
